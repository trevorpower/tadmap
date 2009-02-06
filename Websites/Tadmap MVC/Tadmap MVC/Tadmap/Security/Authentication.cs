using System.Security.Principal;
using Tadmap.Security;

namespace Tadmap.Security
{
   public static class TadmapAuthentication
   {
      static public IIdentity UnauthenticatedIdentity
      {
         get
         {
            return new GenericIdentity("Guest", "None");
         }
      }

      static public IPrincipal UnauthenticatedPrincipal
      {
         get
         {
            string[] roles = { TadmapRoles.Guest };
            return new GenericPrincipal(UnauthenticatedIdentity, roles);
         }
      }
   }
}
