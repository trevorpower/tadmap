using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Models.Images;
using TadmapTests.Mocks.Security;
using TadmapTests.DataAccess;
using Tadmap_MVC.DataAccess;

namespace TadmapTests.Models
{
   [TestFixture]
   public class TadmapImageTests
   {
      Guid _imageId = Guid.NewGuid();

      [Test]
      public void Should_Contain_Id_Title_Description_StorageKey_IsPublic()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", true, true, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(true, image.IsPublic);
         Assert.AreEqual(true, image.IsOffensive);
      }

      [Test]
      public void IsPublic_Can_Be_False()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", false, true, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(false, image.IsPublic);
         Assert.AreEqual(true, image.IsOffensive);
      }

      public void Contains_OwnerName()
      {
         TadmapImage image = new TadmapImage { OwnerName = "Owner" };

         Assert.AreEqual("Owner", image.OwnerName);
      }

      [Test]
      public void IsOffensive_Can_Be_False()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", false, false, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(false, image.IsPublic);
         Assert.AreEqual(false, image.IsOffensive);
      }

      [Test]
      public void Administrator_Should_Be_Able_To_Mark_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.IsTrue(image.CanUserMarkAsOffensive(Principals.Administrator));
      }

      [Test]
      public void Guest_Should_Not_Be_Able_To_Mark_All_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Guest));
      }

      [Test]
      public void Collector_Should_Not_Be_Able_To_Mark_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false, Guid.NewGuid(), new TestImageRepository(), new TestBinaryRepository());

         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Collector));
      }

      [Test]
      public void Administrator_Can_Mark_As_Offensive()
      {
         IImageRepository imageRepository = new TestImageRepository();
         Guid id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1");

         TadmapImage image = image = imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         Assert.IsFalse(image.IsOffensive);

         image.IsOffensive = true;

         imageRepository.Save(image);

         Assert.AreEqual(7, imageRepository.GetAllImages().IsNotOffensive().Count());
      }

      [Test]
      public void Administrator_Can_Mark_As_Not_Offensive()
      {
         IImageRepository imageRepository = new TestImageRepository();
         Guid id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0");

         TadmapImage image = image = imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         Assert.IsTrue(image.IsOffensive);

         image.IsOffensive = false;

         imageRepository.Save(image);

         Assert.AreEqual(9, imageRepository.GetAllImages().IsNotOffensive().Count());
      }

   }
}
