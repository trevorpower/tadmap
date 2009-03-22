using System.Configuration;

namespace Tadmap.Website
{
   public class OpenId
   {
      public static string Realm
      {
         get
         {
            return ConfigurationSettings.AppSettings["OpenIdRealm"];
         }
      }

      public static string LoginUrl
      {
         get
         {
            return ConfigurationSettings.AppSettings["OpenIdLoginUrl"];
         }
      }
   }
}