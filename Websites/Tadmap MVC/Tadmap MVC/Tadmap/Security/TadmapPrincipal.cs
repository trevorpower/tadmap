using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace Tadmap.Tadmap.Security
{
   public class TadmapPrincipal : IPrincipal
   {
      TadmapPrincipal(IIdentity identity)
      {
         Identity = identity;
      }

      #region IPrincipal Members

      public IIdentity Identity { get; private set; }

      public bool IsInRole(string role)
      {
         return Roles.IsUserInRole(role);
      }

      
      #endregion
   }
}
