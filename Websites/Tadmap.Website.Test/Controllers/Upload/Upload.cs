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
using Tadmap.Messaging;
using Tadmap.Messaging.Test.Mock;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Upload
   {
      UploadController _controller;
      MessageQueue _queue;

      [SetUp]
      public void ConstructController()
      {
         var storage = new List<BinaryRepository.Data>();
         var binaries = new BinaryRepository(storage);
         var images = new ImageRepository(binaries);
         _queue = new MessageQueue();

         _controller = new UploadController(images, binaries, _queue);
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
      public void ConfirmUploadCreatesMessage()
      {
         _controller.ConfirmUpload("The name");

         Assert.AreEqual(_queue.Next(int.MaxValue).Content, "The name");
      }
   }
}
