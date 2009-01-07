using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap_MVC.DataAccess.SQL;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.Models.Images;
using System.Transactions;

namespace RepositoryIntegrationTests.SQL
{
   [TestFixture]
   public class SqlImageRepositoryTests
   {
      [Test]
      public void Can_Create()
      {
         IImageRepository repository = new SqlImageRepository();
      }

      [Test]
      public void Can_Get_All_Images_Returns_3()
      {
         IImageRepository repository = new SqlImageRepository();

         Assert.AreEqual(3, repository.GetAllImages().Count());
      }

      [Test]
      public void All_Public_Images_Returns_2()
      {
         IImageRepository repository = new SqlImageRepository();

         Assert.AreEqual(2, repository.GetAllImages().IsPublic().Count());
      }

      [Test]
      public void All_Non_Offensive_Returns_1()
      {
         IImageRepository repository = new SqlImageRepository();

         Assert.AreEqual(1, repository.GetAllImages().IsNotOffensive().Count());
      }

      [Test]
      public void Can_Save_Existing_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages().First();

            repository.Save(image);
         }
      }

      [Test]
      public void Can_Add_New_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage newImage = new TadmapImage(Guid.NewGuid(), "", "", "", true, false, new Guid("d23a1f2a-0db5-4efe-b084-b5529e9a2756"), repository, null);

            repository.Save(newImage);
         }
      }

      [Test]
      public void GetAll_Count_Is_4_After_Saving_New_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage newImage = new TadmapImage(Guid.NewGuid(), "", "", "", true, false, new Guid("d23a1f2a-0db5-4efe-b084-b5529e9a2756"), repository, null);
            
            repository.Save(newImage);

            Assert.AreEqual(4, repository.GetAllImages().Count());
         }
      }

      [Test]
      public void Making_First_Offensive_Image_Not_Offensive_Gives_Non_Offensive_Images_A_Count_Of_2()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages().FirstOrDefault(i => i.IsOffensive);

            image.IsOffensive = false;

            repository.Save(image);

            Assert.AreEqual(2, repository.GetAllImages().IsNotOffensive().Count());
         }
      }

      [Test]
      public void Making_First_Private_Image_Public_Gives_Public_Images_A_Count_Of_3()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages().FirstOrDefault(i => !i.IsPublic);

            image.IsPublic = true;

            repository.Save(image);

            Assert.AreEqual(3, repository.GetAllImages().IsPublic().Count());
         }
      }

      [Test]
      public void Change_To_Title_Is_Saved_And_Returned_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages().FirstOrDefault();

            image.Title = "First Image Title - Changed";
            
            string oldDescription = image.Description;

            repository.Save(image);

            Assert.AreEqual(oldDescription, repository.GetAllImages().FirstOrDefault().Description);
            Assert.AreEqual("First Image Title - Changed", repository.GetAllImages().FirstOrDefault().Title);
         }
      }

      [Test]
      public void Change_To_Description_Is_Saved_And_Returned_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages().FirstOrDefault();

            image.Description = "First Image Title - Changed";

            string oldTitle = image.Title;

            repository.Save(image);

            Assert.AreEqual(oldTitle, repository.GetAllImages().FirstOrDefault().Title);
            Assert.AreEqual("First Image Title - Changed", repository.GetAllImages().FirstOrDefault().Description);
         }
      }

   }
}
