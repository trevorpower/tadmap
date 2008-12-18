using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TadmapTests.Controllers.Image
{
   using NUnit.Framework;
   using System.Web.Mvc;
   using Tadmap_MVC.Controllers;

   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void WithEmptyGuid()
      {
         ImageController home = new ImageController();

         try
         {
            ActionResult result = home.Index(Guid.Empty);
            Assert.Fail("Execption expected.");
         }
         catch (ArgumentException)
         {
            // this is expected
         }
      }
   }

}
