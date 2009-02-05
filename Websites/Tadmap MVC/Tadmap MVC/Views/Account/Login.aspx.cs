using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using DotNetOpenId;
using DotNetOpenId.RelyingParty;
using System.Security.Principal;
using System.Collections.Generic;
using Tadmap.Configuration;
using System.Web.Mvc;

public partial class Login : ViewPage
{
   private void AttachId(Guid userId, string openIdUrl)
   {
      using (SqlConnection cn = new SqlConnection(Database.TadmapConnection))
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
      using (SqlConnection cn = new SqlConnection(Database.TadmapConnection))
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
