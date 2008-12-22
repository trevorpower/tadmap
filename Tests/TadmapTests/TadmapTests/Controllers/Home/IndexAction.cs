using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TadmapTests.Controllers.Home
{
   using NUnit.Framework;
   using System.Web.Mvc;
   using Tadmap_MVC.Models.Images;
   using Tadmap_MVC.Controllers;
   using TadmapTests.DataAccess;

   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void Returns_4_Images()
      {
         HomeController home = new HomeController(new TestImageRepository());

         ActionResult result = home.Index();

         Assert.IsInstanceOfType(typeof(ViewResult), result);

         ViewResult viewResult = result as ViewResult;

         Assert.AreEqual("", viewResult.ViewName);
         Assert.IsEmpty(home.ViewData);
         Assert.IsEmpty(home.ModelState);

         Assert.IsNotNull(home.ViewData.Model);
         Assert.IsInstanceOfType(typeof(List<TadmapImage>), viewResult.ViewData.Model);

         List<TadmapImage> images = viewResult.ViewData.Model as List<TadmapImage>;

         Assert.AreEqual(4, images.Count);
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
