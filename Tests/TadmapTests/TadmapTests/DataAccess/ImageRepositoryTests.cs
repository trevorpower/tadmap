using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Model.Image;
using Tadmap.DataAccess;
using TadmapTests.DataAccess;
using Tadmap.Model.Test.Mock;

namespace TadmapTests.Models
{
   [TestFixture]
   public class IImageRepositoryTest
   {
      [Test]
      public void Contains_GetAll_Function()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.IsNotNull(repository.GetAllImages(new TestBinaryRepository()));
      }

      [Test]
      public void Contains_GetAll_Retunrs_10_Items()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(10, repository.GetAllImages(new TestBinaryRepository()).Count());
      }

      [Test]
      public void Contains_GetAll_Returns_5_Public_Items()
      {
         IImageRepository repository = new TestImageRepository();

         int count = 0;
         foreach (TadmapImage image in repository.GetAllImages(new TestBinaryRepository()))
            if (image.IsPublic)
               count++;

         Assert.AreEqual(5, count);
      }

      [Test]
      public void PublicFilter()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(5, repository.GetAllImages(new TestBinaryRepository()).IsPublic().Count());
      }

      [Test]
      public void OffensiveFilter()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(8, repository.GetAllImages(new TestBinaryRepository()).IsNotOffensive().Count());
      }

      [Test]
      public void With_Known_Id_Returns_1_Image()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(1, repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1")).Count());
      }

      [Test]
      public void With_Known_Id_Returns_1_Image_With_Correct_OwnerName()
      {
         IImageRepository repository = new TestImageRepository();
         TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1")).SingleOrDefault();
         Assert.AreEqual("the owner", image.OwnerName);
      }

      [Test]
      public void With_Non_Existant_Id_Returns_No_Image()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(0, repository.GetAllImages(new TestBinaryRepository()).WithId(Guid.NewGuid()).Count());
      }

      [Test]
      public void Contains_Save_Method()
      {
         IImageRepository repository = new TestImageRepository();

         TadmapImage image = new TadmapImage(new TestImageRepository(), new TestBinaryRepository());

         repository.Save(image);
      }
   }
}
