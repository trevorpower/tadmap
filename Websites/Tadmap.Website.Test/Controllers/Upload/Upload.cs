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
using Tadmap.Model.Image;

namespace TadmapTests.Controllers.Upload
{
   [TestFixture]
   public class Upload
   {
      UploadController _controller;
      MessageQueue _queue;

      List<BinaryRepository.Data> _binaryStorage;
      ImageRepository _imageRepository;
      IBinaryRepository _binaryRepository;

      [SetUp]
      public void ConstructController()
      {
         _binaryStorage = new List<BinaryRepository.Data>();
         _binaryRepository = new BinaryRepository(_binaryStorage);
         _imageRepository = new ImageRepository(_binaryRepository);
         _queue = new MessageQueue();

         _controller = new UploadController(_imageRepository, _binaryRepository, _queue);
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
      public void ConfirmUploadCreatesMessageAndImage()
      {
         _controller.ConfirmUpload(Principals.Collector, "The name", "The key");

         Assert.AreEqual(_queue.Next(int.MaxValue).Content, "The key");

         TadmapImage newImage = _imageRepository.GetAllImages(_binaryRepository).Where(i => i.Key == "The key").Single();

         //Assert.AreEqual(0, newImage.ImageSetVersion);
         //Assert.AreEqual("The name", newImage.Title);
      }
   }
}
