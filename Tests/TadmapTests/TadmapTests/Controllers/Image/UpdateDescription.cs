using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using System.Web.Mvc;
using System.Security.Principal;
using TadmapTests.Mocks.Security;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class UpdateDescription
   {
      [Test]
      public void WithEmptyGuidAndNullString()
      {
         AssertArgumentException(Guid.Empty, null, Principals.Guest);
      }

      [Test]
      public void WithEmptyGuidAndEmptyString()
      {
         AssertArgumentException(Guid.Empty, string.Empty, Principals.Guest);
      }

      [Test]
      public void WithEmptyGuidAndSimpleString()
      {
         AssertArgumentException(Guid.Empty, "Simple", Principals.Guest);
      }

      private static void AssertArgumentException(Guid id, string description, IPrincipal principal)
      {
         ImageController imageController = new ImageController();

         try
         {
            ActionResult result = imageController.UpdateDescription(id, description);
            Assert.Fail("Execption expected.");
         }
         catch (ArgumentException)
         {
            // this is expected
         }
      }
   }
}
