using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Models.Images;
using TadmapTests.Mocks.Security;

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

         TadmapImage image = new TadmapImage(id, "title", "description", "key", true, true);

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(true, image.IsPublic);
         Assert.AreEqual(true, image.IsOffensive);
      }

      [Test]
      public void Should_IsPublic_Can_False()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", false, true);

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(false, image.IsPublic);
         Assert.AreEqual(true, image.IsOffensive);
      }

      [Test]
      public void Should_IsOffensive_Can_Be_False()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", false, false);

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(false, image.IsPublic);
         Assert.AreEqual(false, image.IsOffensive);
      }

      [Test]
      public void Administrator_Should_Be_Able_To_Mark_All_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false);

         Assert.IsTrue(image.CanUserMarkAsOffensive(Principals.Administrator));
      }

      [Test]
      public void Guest_Should_Not_Be_Able_To_Mark_All_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false);

         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Guest));
      }

      [Test]
      public void Collector_Should_Not_Be_Able_To_Mark_All_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(_imageId, "title", "description", "key", false, false);

         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Collector));
      }

   }
}
