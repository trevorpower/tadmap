using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.Models.Images;
using Tadmap_MVC.DataAccess;
using TadmapTests.DataAccess;

namespace TadmapTests.Models
{
   [TestFixture]
   public class IImageRepositoryTest
   {
      [Test]
      public void Contains_GetAll_Function()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.IsNotNull(repository.GetAllImages());
      }

      [Test]
      public void Contains_GetAll_Retunrs_10_Items()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(10, repository.GetAllImages().Count());
      }

      [Test]
      public void Contains_GetAll_Returns_5_Public_Items()
      {
         IImageRepository repository = new TestImageRepository();

         int count = 0;
         foreach (TadmapImage image in repository.GetAllImages())
            if (image.IsPublic)
               count++;

         Assert.AreEqual(5, count);
      }

      [Test]
      public void PublicFilter()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(5, repository.GetAllImages().IsPublic().Count());
      }

      [Test]
      public void OffensiveFilter()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(8, repository.GetAllImages().IsNotOffensive().Count());
      }

      [Test]
      public void With_Known_Id_Returns_1_Image()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(1, repository.GetAllImages().WithId(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1")).Count());
      }

      [Test]
      public void With_Non_Existant_Id_Returns_No_Image()
      {
         IImageRepository repository = new TestImageRepository();

         Assert.AreEqual(0, repository.GetAllImages().WithId(Guid.NewGuid()).Count());
      }

      [Test]
      public void Can_Mark_Image_As_Offensive()
      {
         IImageRepository repository = new TestImageRepository();
         TadmapImage image = repository.GetAllImages().WithId(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1")).SingleOrDefault();
         repository.MarkAsOffensive(image.Id);

         Assert.AreEqual(7, repository.GetAllImages().IsNotOffensive().Count());
      }

      [Test]
      public void Can_Mark_Image_As_UnOffensive()
      {
         IImageRepository repository = new TestImageRepository();
         TadmapImage image = repository.GetAllImages().WithId(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9")).SingleOrDefault();
         repository.MarkAsUnOffensive(image.Id);

         Assert.AreEqual(9, repository.GetAllImages().IsNotOffensive().Count());
      }

      [Test]
      public void Can_Mark_Image_As_Public()
      {
         IImageRepository repository = new TestImageRepository();
         repository.MarkAsPublic(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9"));

         Assert.AreEqual(6, repository.GetAllImages().IsPublic().Count());
      }

      [Test]
      public void Can_Mark_Image_As_Private()
      {
         IImageRepository repository = new TestImageRepository();
         repository.MarkAsPrivate(new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1"));

         Assert.AreEqual(4, repository.GetAllImages().IsPublic().Count());
      }

      [Test]
      public void Contains_Save_Method()
      {
         IImageRepository repository = new TestImageRepository();

         TadmapImage image = new TadmapImage();

         repository.Save(image);
      }

      
   }
}
