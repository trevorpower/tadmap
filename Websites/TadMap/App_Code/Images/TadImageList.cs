using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Web;
using TadMap.Configuration;
using TadMap.Security;

namespace TadMap
{
   public class TadImageList : Collection<TadImage>
   {
      #region Factory Methods

      public static TadImageList GetTadImageList(IIdentity identity)
      {
         if (!identity.IsAuthenticated)
            throw new System.Security.SecurityException("User not authorized to view a map list.");

         TadImageList imageList = new TadImageList();

         using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
         {
            cn.Open();

            using (SqlCommand cm = cn.CreateCommand())
            {
               cm.CommandType = CommandType.StoredProcedure;
               cm.CommandText = "GetUserImages";
               cm.Parameters.AddWithValue("@OpenIdUrl", identity.Name);
               using (SqlDataReader dr = cm.ExecuteReader())
               {
                  while (dr.Read())
                  {
                     TadImage info = new TadImage(dr);
                     imageList.Add(info);
                  }
               }
            }
         }

         return imageList;
      }

      private TadImageList()
      { /* require use of factory methods */ }

      #endregion

      #region Data Access

      [Serializable()]
      private class Criteria
      { /* no criteria - retrieve all projects */ }

      [Serializable()]
      private class FilteredCriteria
      {
         private string m_strUsername;
         public string Username
         {
            get { return m_strUsername; }
         }

         public FilteredCriteria(string strUsername)
         {
            m_strUsername = strUsername;
         }
      }

      private void DataPortal_Fetch(Criteria criteria)
      {
         // fetch with no filter
         //Fetch();
      }

      #endregion

      public static bool CanGetObject()
      {
         return HttpContext.Current.User.IsInRole(TadMapRoles.Collector);
      }
   }
}
