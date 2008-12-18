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
   public class UpdateTitle
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

      [Test]
      public void WithEmptyGuidAndComplexString()
      {
         AssertArgumentException(Guid.Empty, "*654 Complex 1 string__");
      }

      private static void AssertArgumentException(Guid id, string title)
      {
         ImageController imageController = new ImageController();

         try
         {
            ActionResult result = imageController.UpdateTitle(Guid.Empty, title);
            Assert.Fail("Execption expected.");
         }
         catch (ArgumentException)
         {
            // this is expected
         }
      }
   }
}
