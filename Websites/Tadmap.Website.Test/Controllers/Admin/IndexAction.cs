using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using Tadmap.Controllers;
using Tadmap.Model.Image;
using System.Reflection;
using Tadmap.Model.Test.Mock;
using Tadmap.Mode.Test.Mock;
using TadmapTests.Mocks.Security;

namespace TadmapTests.Controllers.Admin
{
   [TestFixture]
   public class IndexAction
   {
      AdminController _controller;

      [SetUp]
      public void CreateController()
      {
         var storage = new List<BinaryRepository.Data>();
         var binaries = new BinaryRepository(storage);
         var images = new ImageRepository();

         _controller = new AdminController(images, binaries);
      }

      [TearDown]
      public void DestroyController()
      {
         if (_controller != null)
            _controller.Dispose();
   
         _controller = null;
      }

      [Test]
      public void Returns_View_Result_With_No_Name_For_Administrator()
      {
         ViewResult result = (ViewResult)_controller.Index();

         Assert.AreEqual("", result.ViewName);
      }

      [Test]
      public void Model_Contains_10_Images()
      {
         ViewResult result = (ViewResult)_controller.Index();

         Assert.IsNotNull(result.ViewData);
         Assert.IsNotNull(result.ViewData.Model);
         Assert.IsInstanceOfType(typeof(List<TadmapImage>), result.ViewData.Model);

         List<TadmapImage> images = result.ViewData.Model as List<TadmapImage>;

         Assert.AreEqual(10, images.Count);
      }
   }
}
