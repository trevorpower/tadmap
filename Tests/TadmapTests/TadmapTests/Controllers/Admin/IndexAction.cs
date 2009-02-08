using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TadmapTests.DataAccess;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using Tadmap.Controllers;
using Tadmap.Models.Images;
using System.Reflection;

namespace TadmapTests.Controllers.Admin
{
   [TestFixture]
   public class IndexAction
   {
      [Test]
      public void Returns_View_Result_With_No_Name_For_Administrator()
      {
         AdminController admin = new AdminController(new TestImageRepository(), new TestBinaryRepository());

         ViewResult result = (ViewResult)admin.Index(Principals.Administrator);

         Assert.AreEqual("", result.ViewName);
      }

      [Test]
      public void Model_Contains_10_Images()
      {
         AdminController admin = new AdminController(new TestImageRepository(), new TestBinaryRepository());

         ViewResult result = (ViewResult)admin.Index(Principals.Administrator);

         Assert.IsNotNull(result.ViewData);
         Assert.IsNotNull(result.ViewData.Model);
         Assert.IsInstanceOfType(typeof(List<TadmapImage>), result.ViewData.Model);

         List<TadmapImage> images = result.ViewData.Model as List<TadmapImage>;

         Assert.AreEqual(10, images.Count);
      }
   }
}
