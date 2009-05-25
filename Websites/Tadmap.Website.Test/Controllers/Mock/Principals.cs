using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Tadmap.Security;
using Tadmap.Tadmap.Security;
using Tadmap.Model;

namespace TadmapTests.Mocks.Security
{
   public class Principals
   {
      private static readonly string[] NoRoles = { };
      private static readonly string[] CollectorRole = {TadmapRoles.Collector};
      private static readonly string[] GuestRole = { TadmapRoles.Guest };
      private static readonly string[] AdminRole = { TadmapRoles.Administrator };

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
            return new System.Security.Principal.GenericPrincipal(new TadmapIdentity("the owner", "the owner display name", new Random().Next(), "Mock"), CollectorRole);
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
