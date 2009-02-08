using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using DotNetOpenId.RelyingParty;
using DotNetOpenId;
using System.Data.SqlClient;
using System.Data;
using Tadmap.Tadmap.Security;
using Tadmap.Configuration;
using Tadmap.Security;

namespace Tadmap.Controllers
{

   [HandleError]
   [OutputCache(Location = OutputCacheLocation.None)]
   public class AccountController : Controller
   {


      // This constructor is used by the MVC framework to instantiate the controller using
      // the default forms authentication and membership providers.

      public AccountController()
      {
      }

      public ActionResult Login()
      {
         return View();
      }

      public ActionResult OpenIdReturn()
      {
         OpenIdRelyingParty rp = new OpenIdRelyingParty();

         string error = string.Empty;

         if (rp.Response != null)
         {
            switch (rp.Response.Status)
            {
               case AuthenticationStatus.Authenticated:
                  {
                     return AuthenticateUser(rp.Response);
                  }
               case AuthenticationStatus.Canceled:
                  {
                     error = "Authentication canceled.";
                     break;
                  }
               case AuthenticationStatus.Failed:
                  {
                     error = "Authentication failed.";

                     if (rp.Response.Exception != null)
                        error += " (" + rp.Response.Exception.Message + ")";

                     break;
                  }
               case AuthenticationStatus.SetupRequired:
                  {
                     error = "Authentication failed. (Setup required?)";
                     break;
                  }
            }
         }

         ViewData["LoginErrorMessage"] = error;

         return View("Login");
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Login(string openid_url)
      {
         //FormsAuth.SetAuthCookie(openid_url, false /* createPersistentCookie */);
         //return RedirectToAction("Index", "Home");

         if (Identifier.IsValid(openid_url))
         {
            try
            {
               Identifier openId = Identifier.Parse(openid_url);

               OpenIdRelyingParty rp = new OpenIdRelyingParty();
               IAuthenticationRequest request = rp.CreateRequest(openId, new Realm(OpenId.Realm), new Uri(OpenId.LoginUrl));
               request.RedirectToProvider();
            }
            catch (DotNetOpenId.OpenIdException exception)
            {
               ModelState.AddModelError("OpenIdException", exception);
            }
         }
         else
         {
            ModelState.AddModelError("openid_url", "The OpenID you provided is not in the correct format.");
         }

         // if we got here then something went wrong so we go back to the login view.
         return View();
      }

      public ActionResult Logout()
      {
         FormsAuthentication.SignOut();
    
         return RedirectToAction("Index", "Home");
      }

      protected override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (filterContext.HttpContext.User.Identity is WindowsIdentity)
         {
            throw new InvalidOperationException("Windows authentication is not supported.");
         }
      }

      /// <summary>
      /// This authenticates a user based on a response from an openid provider.
      /// </summary>
      private ActionResult AuthenticateUser(IAuthenticationResponse response)
      {
         if (response.Status != AuthenticationStatus.Authenticated)
            throw new ArgumentException("The response status must be 'Authenticated'. (" + response.Status.ToString() + ")", "response");

         TadmapDb db = new TadmapDb();

         var user = db.UserOpenIds.Where(u => u.OpenIdUrl == response.ClaimedIdentifier.ToString()).SingleOrDefault();
         Guid userId;

         if (user != null)
         {
            userId = user.UserId;
         }
         else
         {
            userId = CreateNewUser(response.ClaimedIdentifier.ToString());
         }

         var roles = from role in db.UserRoles
                     where role.UserId == userId
                     select role.Role;

         CookieUserData userData = new CookieUserData(
            userId,
            response.FriendlyIdentifierForDisplay.ToString(),
            roles.ToArray()
         );
         
         FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
           1,
           response.ClaimedIdentifier.ToString(),
           DateTime.Now,
           DateTime.Now.AddMinutes(30),
           false,
           userData.ToString()
         );

         // Encrypt the ticket.
         string encTicket = FormsAuthentication.Encrypt(ticket);

         // Create the cookie.
         Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

         return RedirectToAction("Index", new { controller = "Home" });
      }

      private Guid CreateNewUser(string openIdUrl)
      {
         TadmapDb db = new TadmapDb(Database.TadmapConnection);

         User newUser = new User();
         newUser.Id = Guid.NewGuid();
         newUser.Name = string.Empty;

         UserRole newUserRole = new UserRole();
         newUserRole.UserId = newUser.Id;
         newUserRole.Role = TadmapRoles.Collector;

         UserOpenId newOpenId = new UserOpenId();
         newOpenId.UserId = newUser.Id;
         newOpenId.OpenIdUrl = openIdUrl;

         db.Users.InsertOnSubmit(newUser);
         db.UserRoles.InsertOnSubmit(newUserRole);
         db.UserOpenIds.InsertOnSubmit(newOpenId);

         db.SubmitChanges();

         return newUser.Id;
      }
   }
}
