using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Tad;
using TadMap.Library.Security;

namespace TadMap.Library
{
    [Serializable()]
    public class MapList : ReadOnlyListBase<MapList, MapInfo>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all projects.
        /// </summary>
        public static MapList GetMapList()
        {
            return Tad.DataPortal.Client.DataPortal.Fetch<MapList>(new Criteria());
        }

        /// <summary>
        /// Return a list of projects filtered
        /// by project name.
        /// </summary>
        public static MapList GetMapList(string strUsername)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException("User not authorized to view a map list.");
          
            return Tad.DataPortal.Client.DataPortal.Fetch<MapList>(new FilteredCriteria(strUsername));
        }

        private MapList()
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

        private void DataPortal_Fetch(FilteredCriteria criteria)
        {
            Fetch(criteria.Username);
        }

        private void Fetch(string strUsername)
        {
            using (SqlConnection cn = new SqlConnection(Database.TadMapConnection))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetMaps";
                    cm.Parameters.AddWithValue("@Username", strUsername);
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            MapInfo info = new MapInfo(
                              dr.GetGuid(0),
                              dr.GetString(1),
                              dr.GetString(2)
                            );
                            Add(info);
                        }
                        IsReadOnly = true;
                    }
                }
            }
        }

        #endregion

        public static bool CanGetObject()
        {
           return false;
        }
    }
}
