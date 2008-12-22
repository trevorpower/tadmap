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
   using TadmapTests.DataAccess;
   using System.Security;

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

      [Test]
      public void Offensive_Image_Throws_Exception_For_Guest()
      {
         AssertThrowsException(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void Private_Image_Throws_Exception_For_Guest()
      {
         AssertThrowsException(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf8 "), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void Public_Returns_Correct_Image_In_Model_For_Guest()
      {
         ImageController home = new ImageController(new TestImageRepository());
         ViewResult result = (ViewResult)home.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Guest);

         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(TadmapImage), result.ViewData.Model);

         TadmapImage image = result.ViewData.Model as TadmapImage;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"));
         Assert.AreEqual(image.Title, "Title 1");
         Assert.AreEqual(image.IsOffensive, false);
         Assert.AreEqual(image.IsPublic, true);
      }

      [Test]
      public void Private_And_Offensive_Returns_Correct_Image_In_Model_For_Administrator()
      {
         ImageController home = new ImageController(new TestImageRepository());
         ViewResult result = (ViewResult)home.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"), Principals.Administrator);
         
         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(TadmapImage), result.ViewData.Model);

         TadmapImage image = result.ViewData.Model as TadmapImage;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"));
         Assert.AreEqual(image.Title, "Title 9");
         Assert.AreEqual(image.IsOffensive, true);
         Assert.AreEqual(image.IsPublic, false);
      }

      private static void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         ImageController home = new ImageController(new TestImageRepository());

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
