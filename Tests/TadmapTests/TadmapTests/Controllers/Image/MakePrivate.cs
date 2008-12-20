using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Controllers;
using System.Web.Mvc;
using TadmapTests.Mocks.Security;
using System.Security.Principal;
using Tadmap_MVC.Models.Images;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class MakePrivate
   {
      [Test]
      public void WithEmptyGuid()
      {
         AssertThrowsException(Guid.Empty, typeof(ArgumentException), Principals.Guest);
      }

      [Test]
      public void WithNonExistantGuid()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(ImageNotFound), Principals.Guest);
      }

      private static void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         ImageController imageController = new ImageController();

         try
         {
            ActionResult result = imageController.MakePrivate(id);
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
