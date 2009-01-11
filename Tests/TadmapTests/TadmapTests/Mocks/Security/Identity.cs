using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace TadmapTests.Mocks.Security
{
   internal class Identity : IIdentity
   {
      public Identity(string name, bool isAuthenticated)
      {
         IsAuthenticated = isAuthenticated;
         Name = name;
      }

      string IIdentity.AuthenticationType
      {
         get { return "Mocked"; }
      }

      public bool IsAuthenticated { get; set; }
      public string Name { get; set; }
   }
}
