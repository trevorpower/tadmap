using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Tadmap.Security;

namespace TadmapTests.Tadmap.Security
{
   [TestFixture]
   public class TadmapIdentity_Tests
   {
      [Test]
      public void HasNameAndId()
      {
         TadmapIdentity identity = new TadmapIdentity("Name", "Display Name", Guid.Empty, string.Empty);

         Assert.AreEqual("Name", identity.Name);
         Assert.AreEqual("Display Name", identity.DisplayName);
         Assert.AreEqual(Guid.Empty, identity.Id);
      }
   }
}
