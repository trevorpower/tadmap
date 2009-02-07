using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using System.Web.Mvc;
using TadmapTests.Mocks.Security;
using System.Security.Principal;
using Tadmap.Models.Images;
using TadmapTests.DataAccess;
using System.Security;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class MakePrivate
   {
      private ImageController _imageController;

      [SetUp]
      public void CreateController()
      {
         _imageController = new ImageController(new TestImageRepository(), new TestBinaryRepository());
      }

      [Test]
      public void WithEmptyGuid()
      {
         AssertThrowsException(Guid.Empty, typeof(ArgumentException), Principals.Guest);
      }

      [Test]
      public void WithNonExistantGuid()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(ImageNotFoundException), Principals.Guest);
      }

      private void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         try
         {
            ActionResult result = _imageController.MakePrivate(id, Principals.Guest);
            Assert.Fail("Execption expected.");
         }
         catch (Exception e)
         {
            Assert.AreEqual(type, e.GetType());
            // this is expected
         }
      }
   }
}
