using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Models.Images;

namespace TadmapTests.Models
{
   [TestFixture]
   public class TadmapImageTests
   {
      [Test]
      public void Should_Contain_Id_Title_Description_StorageKey_IsPublic()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", true);

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(true, image.IsPublic);
      }

      [Test]
      public void Should_IsPublic_Can_False()
      {
         Guid id = Guid.NewGuid();

         TadmapImage image = new TadmapImage(id, "title", "description", "key", false);

         Assert.AreEqual(id, image.Id);
         Assert.AreEqual("title", image.Title);
         Assert.AreEqual("description", image.Description);
         Assert.AreEqual("key", image.Key);
         Assert.AreEqual(false, image.IsPublic);
      }
   }
}
