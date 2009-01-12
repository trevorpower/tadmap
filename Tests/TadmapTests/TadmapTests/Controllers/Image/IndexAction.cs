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
   using Tadmap_MVC.Views.Image;

   [TestFixture]
   public class IndexAction
   {
      private ImageController _imageController;

      [SetUp]
      public void CreateController()
      {
         _imageController = new ImageController(new TestImageRepository(), new TestBinaryRepository());
      }

      [Test]
      public void Throws_ArgumentException_For_EmptyGuid()
      {
         AssertThrowsException(Guid.Empty, typeof(ArgumentException), Principals.Guest);
      }
      
      [Test]
      public void Throws_ImageNotFoundException_For_Non_Existant_Guid()
      {
         AssertThrowsException(Guid.NewGuid(), typeof(ImageNotFoundException), Principals.Guest);
      }

      [Test]
      public void Offensive_Image_Throws_SecurityException_For_Guest()
      {
         AssertThrowsException(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void Private_Image_Throws_SecurityException_For_Guest()
      {
         AssertThrowsException(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf8 "), typeof(SecurityException), Principals.Guest);
      }

      [Test]
      public void Returns_Correct_Public_Image_In_Model_For_Guest()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Guest);

         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"));
         Assert.AreEqual(image.Title, "Title 1");

         Assert.AreEqual(false, image.ShowOffensiveCount);
         Assert.AreEqual(0, image.OffensiveCount);
      }

      [Test]
      public void Private_And_Offensive_Returns_Correct_Image_In_Model_For_Administrator()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"), Principals.Administrator);
         
         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"));
         Assert.AreEqual(image.Title, "Title 9");

         Assert.AreEqual(true, image.ShowOffensiveCount);
         Assert.AreEqual(1, image.OffensiveCount);
      }

      [Test]
      public void Public_And_Offensive_Returns_Correct_Image_In_Model_For_Administrator()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"), Principals.Administrator);

         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"));
         Assert.AreEqual(image.Title, "Title 0");

         Assert.AreEqual(true, image.ShowOffensiveCount);
         Assert.AreEqual(1, image.OffensiveCount);
      }

      [Test]
      public void Returns_Correct_Private_Image_In_Model_For_Administrator()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf6"), Principals.Administrator);

         Assert.IsNotNull(result);
         Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.AreEqual(image.Id, new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf6"));
         Assert.AreEqual(image.Title, "Title 6");
         Assert.AreEqual(true, image.ShowOffensiveCount);
         Assert.AreEqual(0, image.OffensiveCount);
      }

      [Test]
      public void Show_Offensive_Count_Is_True_For_All_Images_For_Administrator()
      {
         for (int i = 0; i < 10; i++)
         {
            ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf" + i), Principals.Administrator);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

            ImageView image = result.ViewData.Model as ImageView;
            Assert.AreEqual(true, image.ShowOffensiveCount);
         }
      }

      [Test]
      public void Show_Offensive_Count_Is_False_For_All_Images_For_Collector()
      {
         for (int i = 0; i < 10; i++)
         {
            try
            {
               ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf" + i), Principals.Collector);

               Assert.IsNotNull(result);
               Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

               ImageView image = result.ViewData.Model as ImageView;
               Assert.AreEqual(false, image.ShowOffensiveCount);
            }
            catch (SecurityException)
            {
               // ignore this error for private/offensive images
               // we are testing that for all images the user can view the show offensive count is false
               // and the value is 0
            }
         }
      }

      [Test]
      public void Show_Offensive_Count_Is_False_For_All_Images_For_Guest()
      {
         for (int i = 0; i < 10; i++)
         {
            try
            {
               ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf" + i), Principals.Guest);

               Assert.IsNotNull(result);
               Assert.IsInstanceOfType(typeof(ImageView), result.ViewData.Model);

               ImageView image = result.ViewData.Model as ImageView;
               Assert.AreEqual(false, image.ShowOffensiveCount);
               Assert.AreEqual(0, image.OffensiveCount);
            }
            catch (SecurityException)
            {
               // ignore this error for private/offensive images
               // we are testing that for all images the user can view the show offensive count is false
               // and the value is 0
            }
         }
      }


      [Test]
      public void CanMarkOffensive_And_CanUnmarkOffensive_Is_False_For_Public_Image_For_Guest()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf2"), Principals.Guest);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.IsFalse(image.CanMarkOffensive);
         Assert.IsFalse(image.CanUnmarkOffensive);
      }

      [Test]
      public void CanMarkOffensive_And_CanUnmarkOffensive_Is_False_For_Public_Image_For_Collector()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf2"), Principals.Collector);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.IsFalse(image.CanMarkOffensive);
         Assert.IsFalse(image.CanUnmarkOffensive);
      }

      [Test]
      public void CanMarkOffensive_Is_False_And_CanUnmarkOffensive_Is_True_For_Offensive_Image_For_Administrator()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0"), Principals.Administrator);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.IsFalse(image.CanMarkOffensive);
         Assert.IsTrue(image.CanUnmarkOffensive);
      }

      [Test]
      public void CanMarkOffensive_Is_True_And_CanUnmarkOffensive_Is_False_For_Unoffensive_Image_For_Administrator()
      {
         ViewResult result = (ViewResult)_imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Administrator);

         ImageView image = result.ViewData.Model as ImageView;
         Assert.IsTrue(image.CanMarkOffensive);
         Assert.IsFalse(image.CanUnmarkOffensive);
      }

      [Test]
      public void Original_Not_Returned_For_Public_Image_For_Guest_User()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Guest);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsNull(model.OriginalUrl);
      }

      [Test]
      public void Original_Url_Returned_For_Admin()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Administrator);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsNotNull(model.OriginalUrl);
      }

      [Test]
      public void Original_Url_Returned_For_Owner()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsNotNull(model.OriginalUrl);
      }

      [Test]
      public void Has_Url_Equal_To_Key_Plus_Url()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.AreEqual("Keyurl", model.OriginalUrl);
      }

      [Test]
      public void Has_Preview_Url_Equal_To_PreviewKey_Plus_Url_For_Owner()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.AreEqual("Preview_Keyurl", model.PreviewUrl);
      }

      [Test]
      public void Image_IsEditable_Is_True_For_Owner()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsTrue(model.IsEditable);
      }

      [Test]
      public void Model_IsPublic_Is_True_For_Public_Image_For_Collector()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsTrue(model.IsPublic);
      }

      [Test]
      public void Model_IsPublic_Is_False_For_Private_Image_For_Collector()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf6"), Principals.Collector);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsFalse(model.IsPublic);
      }

      [Test]
      public void Model_IsEditable_Is_False_For_Guest()
      {
         ActionResult result = _imageController.Index(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf2"), Principals.Guest);
         ViewResult viewResult = result as ViewResult;
         ImageView model = viewResult.ViewData.Model as ImageView;

         Assert.IsFalse(model.IsEditable);
      }

      private void AssertThrowsException(Guid id, Type type, IPrincipal principal)
      {
         try
         {
            ActionResult result = _imageController.Index(id, principal);
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
