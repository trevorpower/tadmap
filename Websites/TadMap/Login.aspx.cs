using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using DotNetOpenId;
using DotNetOpenId.RelyingParty;
using System.Security.Principal;
using System.Collections.Generic;
using TadMap.Configuration;
using System.Transactions;

public partial class Login : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      OpenIdRelyingParty rp = new OpenIdRelyingParty();

      if (rp.Response != null)
      {
         switch (rp.Response.Status)
         {
            case AuthenticationStatus.Authenticated:
               {
                  AuthenticateUser(rp.Response);
                  break;
               }
            case AuthenticationStatus.Canceled:
               {
                  lblError.Text = "Authentication canceled.";
                  break;
               }
            case AuthenticationStatus.Failed:
               {
                  lblError.Text = "Authentication failed.";
                  if (rp.Response.Exception != null)
                     lblError.Text += " (" + rp.Response.Exception.Message + ")";

                  break;
               }
            case AuthenticationStatus.SetupRequired:
               {
                  lblError.Text = "Authentication failed. (Setup required?)";
                  break;
               }
         }
      }
   }

   /// <summary>
   /// This authenticates a user based on a response from an openid provider.
   /// </summary>
   /// <param name="response"></param>
   private void AuthenticateUser(IAuthenticationResponse response)
   {
      if (response.Status != AuthenticationStatus.Authenticated)
         throw new ArgumentException("The response status must be 'Authenticated'. (" + response.Status.ToString() + ")", "response");

      Identifier identifier = response.ClaimedIdentifier;

      if (identifier == null)
      {
         lblError.Text = "Oops! We lost the ID you tried to log in with. Can you try again?";
      }
      else
      {
         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();
            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandType = CommandType.StoredProcedure;
               cm.CommandText = "GetUserId";
               cm.Parameters.AddWithValue("@OpenIdUrl", identifier.ToString());

               using (SqlDataReader dr = cm.ExecuteReader())
               {
                  if (dr.Read())
                  {
                     // a record was found so the user already exists
                     // in the future we can use this to populate the identity
                     // with display name and image...
                  }
                  else
                  {
                     // no user was found so we create it now.
                     CreateNewUser(identifier.ToString());
                  }
               }
            }
         }

         // somehow we need to put the response.FriendlyIdentifierForDisplay
         // into the identity/principal

         if (Request.QueryString["ReturnUrl"] != null)
         {
            FormsAuthentication.RedirectFromLoginPage(identifier.ToString(), false);
         }
         else
         {
            FormsAuthentication.SetAuthCookie(identifier.ToString(), false);
            Response.Redirect("Default.aspx", false);
         }
      }
   }

   private void CreateNewUser(string openIdUrl)
   {
      Guid identity = Guid.NewGuid();

      using (TransactionScope transaction = new TransactionScope())
      {
         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandText = "AddUser";
               cm.CommandType = CommandType.StoredProcedure;

               cm.Parameters.AddWithValue("@id", identity);

               int count = cm.ExecuteNonQuery();

               if (count == 0)
               {
                  throw new Exception("Unable to create user");
               }
            }
         }
         AttachId(identity, openIdUrl);

         transaction.Complete();
      }
   }

   private void AttachId(Guid userId, string openIdUrl)
   {
      using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
      {
         cn.Open();

         using (SqlCommand cm = cn.CreateCommand())
         {
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "AttachOpenId";
            cm.Parameters.AddWithValue("@OpenIdUrl", openIdUrl);
            cm.Parameters.AddWithValue("@UserId", userId);
            cm.ExecuteNonQuery();
         }
      }
   }

   private List<string> GetRoles(string openIdUrl)
   {
      using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
      {
         cn.Open();

         using (SqlCommand cm = cn.CreateCommand())
         {
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "GetUserRoles";
            cm.Parameters.AddWithValue("@OpenIdUrl", openIdUrl);
            SqlDataReader dr = cm.ExecuteReader();

            List<string> roles = new List<string>();
            while (dr.Read())
            {
               roles.Add((string)dr["RoleName"]);
            }

            return roles;
         }
      }
   }

   protected void btnLogin_Click(object sender, EventArgs e)
   {
      string openIdUrl = Request["openid_url"];

      if (Identifier.IsValid(openIdUrl))
      {
         try
         {
            Identifier openId = Identifier.Parse(openIdUrl);

            OpenIdRelyingParty rp = new OpenIdRelyingParty();
            IAuthenticationRequest request = rp.CreateRequest(openId, new Realm(OpenId.Realm), new Uri(OpenId.LoginUrl));
            request.RedirectToProvider();
         }
         catch (DotNetOpenId.OpenIdException exception)
         {
            lblError.Text = exception.Message;
         }
         catch (Exception exception)
         {
            lblError.Text = "An unexpected error occured during authentication. (" + exception.Message + ")";
         }
      }
      else
      {
         lblError.Text = "The OpenID you provided is not in the correct format.";
      }
   }
}
