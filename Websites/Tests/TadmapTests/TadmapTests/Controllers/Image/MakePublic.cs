using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Controllers;
using System.Web.Mvc;
using TadmapTests.Mocks.Security;
using System.Security.Principal;
using Tadmap.Model.Image;
using TadmapTests.DataAccess;
using Tadmap.Models.Image;
using Tadmap.Model.Test.Mock;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class MakePublic
   {
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

      private static void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         ImageController imageController = new ImageController(new TestImageRepository(), new TestBinaryRepository());

         try
         {
            ActionResult result = imageController.MakePublic(id);
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
