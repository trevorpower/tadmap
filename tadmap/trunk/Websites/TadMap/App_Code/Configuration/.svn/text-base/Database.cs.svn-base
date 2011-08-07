using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace TadMap.Configuration
{
   public static class Database
   {
      public static string TadMapConnection
      {
         get
         {
            return ConfigurationManager.ConnectionStrings["TadMap"].ConnectionString;
         }
      }

      public static string SecurityConnection
      {
         get { return System.Configuration.ConfigurationManager.ConnectionStrings["Security"].ConnectionString; }
      }
   }
}
