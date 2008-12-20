using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TadmapTests.Controllers.Image
{
   using NUnit.Framework;
   using System.Web.Mvc;
   using Tadmap_MVC.Controllers;
   using TadmapTests.Mocks.Security;
   using System.Security.Principal;
   using Tadmap_MVC.Models.Images;

   [TestFixture]
   public class IndexAction
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
         ImageController home = new ImageController();

         try
         {
            ActionResult result = home.Index(id, principal);
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
