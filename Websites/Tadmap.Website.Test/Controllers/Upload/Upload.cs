using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using TadmapTests.Mocks.Security;
using System.Web.Mvc;
using System.Security;
using System.Web;
using Tadmap.Model.Test.Mock;
using Tadmap.Mode.Test.Mock;
using Tadmap.Website.Test.Mock;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Upload
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
      public void Returns_Redirect_For_Collector()
      {
         ActionResult result = _controller.Upload("title", "description", Principals.Collector, new TestEmptyFile());

         Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
      }
   }
}
