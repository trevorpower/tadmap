using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Controllers;
using System.Web.Mvc;
using System.Security;
using System.Security.Principal;
using TadMap.Security;
using Tadmap_MVC.Models.Images;
using TadmapTests.Mocks.Security;

namespace TadmapTests.Controllers.Image
{
   [TestFixture]
   public class UnMark
   {
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
         AssertThrowsException(Guid.NewGuid(), typeof(ImageNotFound), Principals.Administrator);
      }

      private static void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         ImageController imageController = new ImageController();
 
         try
         {
            ActionResult result = imageController.UnMark(id, principal);
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
