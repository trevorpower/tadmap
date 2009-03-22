using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using System.Security;
using Tadmap.Model.Test.Mock;
using Tadmap.Mode.Test.Mock;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Index
   {
      UploadController _controller;

      [SetUp]
      public void ConstructController()
      {
         var storage = new List<BinaryRepository.Data>();
         var binaries = new BinaryRepository(storage);
         var images = new ImageRepository(binaries);

         _controller = new UploadController(images, binaries);
      }

      [TearDown]
      public void DestructController()
      {
         if (_controller != null)
         {
            _controller.Dispose();
            _controller = null;
         }
      }

      [Test]
      public void Returns_ViewResult_For_Collector()
      {
         ActionResult result = _controller.Index();

         Assert.IsInstanceOfType(typeof(ViewResult), result); 
      }

      [Test]
      public void Returns_ViewResult_With_Empty_Name_And_Null_Model_With_No_Errors()
      {
         ViewResult result = _controller.Index() as ViewResult;

         Assert.IsEmpty(result.ViewName);
         Assert.IsNull(result.ViewData.Model);
         Assert.AreEqual(0, result.ViewData.ModelState.Count);
      }
   }
}
