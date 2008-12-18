using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TadmapTests.Controllers.Home
{
   using NUnit.Framework;
   using System.Web.Mvc;

   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void Simple()
      {
         Tadmap_MVC.Controllers.HomeController home = new Tadmap_MVC.Controllers.HomeController();

         ActionResult result = home.Index();

         Assert.IsInstanceOfType(typeof(ViewResult), result);

         ViewResult viewResult = result as ViewResult;

         Assert.AreEqual("", viewResult.ViewName);
         Assert.IsEmpty(home.ViewData);
         Assert.IsEmpty(home.ModelState);
      }

      [Test]
      public void SimpleAboutAction()
      {
         Tadmap_MVC.Controllers.HomeController home = new Tadmap_MVC.Controllers.HomeController();

         try
         {
            ActionResult result = home.About();
            Assert.Fail("Execption expected.");
         }
         catch (NotImplementedException)
         {
            // this is expected
         }
      }
   }

}
