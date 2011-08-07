using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Security;
using System;
using System.Data;
using TadMap.Configuration;

public class TadMapRoleProvider : RoleProvider
{
   public override void AddUsersToRoles(string[] usernames, string[] roleNames)
   {
      throw new NotImplementedException();
   }

   public override string ApplicationName
   {
      get
      {
         return "tadmap.com";
      }
      set
      {
         throw new NotImplementedException();
      }
   }

   public override void CreateRole(string roleName)
   {
      throw new NotImplementedException();
   }

   public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
   {
      throw new NotImplementedException();
   }

   public override string[] FindUsersInRole(string roleName, string usernameToMatch)
   {
      throw new NotImplementedException();
   }

   public override string[] GetAllRoles()
   {
      throw new NotImplementedException();
   }

   public override string[] GetRolesForUser(string username)
   {
      using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
      {
         cn.Open();

         using (SqlCommand cm = cn.CreateCommand())
         {
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "GetUserRoles";
            cm.Parameters.AddWithValue("@OpenIdUrl", username);
            SqlDataReader dr = cm.ExecuteReader();

            List<string> roles = new List<string>();
            while (dr.Read())
            {
               roles.Add((string)dr["RoleName"]);
            }

            return roles.ToArray();
         }
      }
   }

   public override string[] GetUsersInRole(string roleName)
   {
      throw new NotImplementedException();
   }

   public override bool IsUserInRole(string username, string roleName)
   {
      throw new NotImplementedException();
   }

   public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
   {
      throw new NotImplementedException();
   }

   public override bool RoleExists(string roleName)
   {
      throw new NotImplementedException();
   }
}
