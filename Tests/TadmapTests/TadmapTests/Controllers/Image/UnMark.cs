using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using System.Web.Mvc;
using System.Security;
using System.Security.Principal;
using Tadmap.Security;
using Tadmap.Model.Image;
using TadmapTests.Mocks.Security;
using TadmapTests.DataAccess;
using Tadmap.Model.Test.Mock;
using Tadmap.Models.Image;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class UnMark
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
      public void NonAdministrator()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void Collector()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(SecurityException), Principals.Collector);
      }

      [Test]
      public void Guest()
      {
        AssertThrowsException(Guid.NewGuid(), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void NonExistingImage()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(ImageNotFoundException), Principals.Administrator);
      }

      [Test]
      public void Administrator_Can_Mark_Existing_Image_As_Offensive()
      {
         ActionResult result = _imageController.UnMark(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"), Principals.Administrator);
      }

      [Test]
      public void Returns_Json_Result_On_Success()
      {
         ActionResult result = _imageController.UnMark(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"), Principals.Administrator);
         Assert.IsInstanceOfType(typeof(JsonResult), result);
      }

      [Test]
      public void Returns_Json_Result_With_True_On_Success()
      {
         ActionResult result = _imageController.UnMark(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"), Principals.Administrator);
         JsonResult jsonResult = result as JsonResult;

         Assert.AreEqual(true, jsonResult.Data);
      }

      private void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         try
         {
            ActionResult result = _imageController.UnMark(id, principal);
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
