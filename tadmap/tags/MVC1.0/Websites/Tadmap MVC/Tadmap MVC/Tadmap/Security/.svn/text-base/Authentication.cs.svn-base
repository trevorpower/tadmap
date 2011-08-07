using System.Security.Principal;

namespace TadMap.Security
{
   public class TadMapAuthentication
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
            string[] roles = { TadMapRoles.Guest };
            return new GenericPrincipal(UnauthenticatedIdentity, roles);
         }
      }
   }
}
