using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Controllers;
using System.Web.Mvc;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class UpdateDescription
   {
      [Test]
      public void WithEmptyGuidAndNullString()
      {
         AssertArgumentException(Guid.Empty, null);
      }

      [Test]
      public void WithEmptyGuidAndEmptyString()
      {
         AssertArgumentException(Guid.Empty, string.Empty);
      }

      [Test]
      public void WithEmptyGuidAndSimpleString()
      {
         AssertArgumentException(Guid.Empty, "Simple");
      }

      private static void AssertArgumentException(Guid id, string description)
      {
         ImageController imageController = new ImageController();

         try
         {
            ActionResult result = imageController.UpdateDescription(Guid.Empty, description);
            Assert.Fail("Execption expected.");
         }
         catch (ArgumentException)
         {
            // this is expected
         }
      }
   }
}
