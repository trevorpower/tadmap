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
   public class MakePublic
   {
      [Test]
      public void WithEmptyGuid()
      {
         ImageController imageController = new ImageController();

         try
         {
            ActionResult result = imageController.MakePublic(Guid.Empty);
            Assert.Fail("Execption expected.");
         }
         catch (ArgumentException)
         {
            // this is expected
         }
      }
   }
}
