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
using TadMap.Security;
using System.Web.Mvc;

public partial class Login : ViewPage
{
   protected void Page_Load(object sender, EventArgs e)
   {
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
}
