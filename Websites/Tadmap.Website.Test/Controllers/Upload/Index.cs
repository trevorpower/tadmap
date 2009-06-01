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
using Tadmap.Messaging.Test.Mock;
using Rhino.Mocks;
using com.flajaxian;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Index
   {
      UploadController _controller;
      MessageQueue _messageQueue;
      MockRepository _mocks;

      [SetUp]
      public void ConstructController()
      {
         var storage = new List<BinaryRepository.Data>();
         var binaries = new BinaryRepository(storage);
         var images = new ImageRepository();
         _messageQueue = new MessageQueue();

         _mocks = new MockRepository();

         _controller = new UploadController(images, binaries, _messageQueue, _mocks.CreateMock<FileUploaderAdapter>());
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
         Assert.IsInstanceOfType(typeof(FileUploaderAdapter), result.ViewData.Model);
         Assert.AreEqual(0, result.ViewData.ModelState.Count);
      }
   }
}
