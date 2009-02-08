using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Models.Images;
using TadmapTests.Mocks.Security;
using TadmapTests.DataAccess;
using Tadmap.DataAccess;
using NUnit.Framework.SyntaxHelpers;
using Tadmap.Models.ImageSets;

namespace TadmapTests.Models
{
   [TestFixture]
   public class TadmapImageTests
   {
      Guid _imageId = Guid.NewGuid();

      public void Contains_OwnerName()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository()) { OwnerName = "Owner" };

         Assert.AreEqual("Owner", image.OwnerName);
      }

      [Test]
      public void Administrator_Should_Be_Able_To_Mark_As_Offensive()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository())
         {
            Id = _imageId,
            Title = "title",
            Description = "description",
            Key = "key",
            IsPublic =  false,
            IsOffensive = false,
            UserId = Guid.NewGuid(),
         };

         Assert.IsTrue(image.CanUserMarkAsOffensive(Principals.Administrator));
      }

      [Test]
      public void Guest_Should_Not_Be_Able_To_Mark_All_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository())
         {
            Id = _imageId,
            Title = "title",
            Description = "description",
            Key = "key",
            IsPublic = false,
            IsOffensive = false,
            UserId = Guid.NewGuid()
         };
         
         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Guest));
      }

      [Test]
      public void Collector_Should_Not_Be_Able_To_Mark_Image_As_Offensive()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository())
         {
            Id = _imageId,
            Title = "title",
            Description = "description",
            Key = "key",
            IsPublic = false,
            IsOffensive = false,
            UserId = Guid.NewGuid()
         };

         Assert.IsFalse(image.CanUserMarkAsOffensive(Principals.Collector));
      }

      [Test]
      public void Has_ImageSet_Accessor()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository());

         Assert.IsNull(image.ImageSet);
      }

      [Test]
      public void Administrator_Can_Mark_As_Offensive()
      {
         IImageRepository imageRepository = new TestImageRepository();
         Guid id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1");

         TadmapImage image = image = imageRepository.GetAllImages(new TestBinaryRepository()).WithId(id).SingleOrDefault();
         Assert.IsFalse(image.IsOffensive);

         image.IsOffensive = true;

         imageRepository.Save(image);

         Assert.AreEqual(7, imageRepository.GetAllImages(new TestBinaryRepository()).IsNotOffensive().Count());
      }

      [Test]
      public void Administrator_Can_Mark_As_Not_Offensive()
      {
         IImageRepository imageRepository = new TestImageRepository();
         Guid id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf0");

         TadmapImage image = image = imageRepository.GetAllImages(new TestBinaryRepository()).WithId(id).SingleOrDefault();
         Assert.IsTrue(image.IsOffensive);

         image.IsOffensive = false;

         imageRepository.Save(image);

         Assert.AreEqual(9, imageRepository.GetAllImages(new TestBinaryRepository()).IsNotOffensive().Count());
      }

      [Test]
      public void HasThumbUrl()
      {
         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository())
         {
            Key = "theKey",
            ImageSet = new ImageSet1("theKey")
         };


         Assert.That(image.SquareUrl, Is.EqualTo(new Uri("http://Square_theKey.url")));

      }
   }
}
