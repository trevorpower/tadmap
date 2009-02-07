using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Models.Images;
using Tadmap.Controllers;

namespace TadmapTests.Controllers.Home
{
   using NUnit.Framework;
   using System.Web.Mvc;
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
         Assert.AreEqual(0, home.ViewData.Count);
         Assert.AreEqual(0, home.ModelState.Count);

         Assert.IsNotNull(home.ViewData.Model);
         Assert.IsInstanceOfType(typeof(List<TadmapImage>), viewResult.ViewData.Model);

         List<TadmapImage> images = viewResult.ViewData.Model as List<TadmapImage>;

         Assert.AreEqual(4, images.Count);
      }
   }

}
