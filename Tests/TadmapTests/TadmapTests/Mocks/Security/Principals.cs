using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using TadMap.Security;

namespace TadmapTests.Mocks.Security
{
   internal class Principals
   {
      private static readonly string[] NoRoles = { };
      private static readonly string[] CollectorRole = {TadMapRoles.Collector};
      private static readonly string[] GuestRole = { TadMapRoles.Guest };
      private static readonly string[] AdminRole = { TadMapRoles.Administrator };

      static public IPrincipal Guest
      {
         get
         {
            return new System.Security.Principal.GenericPrincipal(new Identity("", false), NoRoles);
         }
      }

      static public IPrincipal Collector
      {
         get
         {
            return new System.Security.Principal.GenericPrincipal(new Identity("", true), CollectorRole);
         }
      }

      static public IPrincipal Administrator
      {
         get
         {
            return new System.Security.Principal.GenericPrincipal(new Identity("Admin", true), AdminRole);
         }
      }
   }
}
