﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.DataAccess.SQL;
using Tadmap.DataAccess;
using Tadmap.Model.Image;
using System.Transactions;
using TadmapTests.DataAccess;
using Tadmap.Model.Test.Mock;

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

         Assert.AreEqual(3, repository.GetAllImages(new TestBinaryRepository()).Count());
      }

      [Test]
      public void All_Public_Images_Returns_2()
      {
         IImageRepository repository = new SqlImageRepository();

         Assert.AreEqual(2, repository.GetAllImages(new TestBinaryRepository()).IsPublic().Count());
      }

      [Test]
      public void All_Non_Offensive_Returns_1()
      {
         IImageRepository repository = new SqlImageRepository();

         Assert.AreEqual(1, repository.GetAllImages(new TestBinaryRepository()).IsNotOffensive().Count());
      }

      [Test]
      public void Can_Save_Existing_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).First();

            repository.Save(image);
         }
      }

      [Test]
      public void Can_Add_New_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage newImage = new TadmapImage(repository, null)
            {
               Id = Guid.NewGuid(),
               Title = "",
               Description = "",
               Key = "",
               IsPublic = true,
               IsOffensive = false,
               UserId = new Guid("d23a1f2a-0db5-4efe-b084-b5529e9a2756")
            };

            repository.Save(newImage);
         }
      }

      [Test]
      public void GetAll_Count_Is_4_After_Saving_New_Image()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage newImage = new TadmapImage(repository, null)
            {
               Id = Guid.NewGuid(),
               Title = "",
               Description = "",
               Key = "",
               IsPublic = true,
               IsOffensive = false,
               UserId = new Guid("d23a1f2a-0db5-4efe-b084-b5529e9a2756")
            };

            repository.Save(newImage);

            Assert.AreEqual(4, repository.GetAllImages(new TestBinaryRepository()).Count());
         }
      }

      [Test]
      public void Making_First_Offensive_Image_Not_Offensive_Gives_Non_Offensive_Images_A_Count_Of_2()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault(i => i.IsOffensive);

            image.IsOffensive = false;

            repository.Save(image);

            Assert.AreEqual(2, repository.GetAllImages(new TestBinaryRepository()).IsNotOffensive().Count());
         }
      }



      [Test]
      public void Making_First_Private_Image_Public_Gives_Public_Images_A_Count_Of_3()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault(i => !i.IsPublic);

            image.IsPublic = true;

            repository.Save(image);

            Assert.AreEqual(3, repository.GetAllImages(new TestBinaryRepository()).IsPublic().Count());
         }
      }

      [Test]
      public void Change_To_Title_Is_Saved_And_Returned_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault();

            image.Title = "First Image Title - Changed";
            
            string oldDescription = image.Description;

            repository.Save(image);

            Assert.AreEqual(oldDescription, repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault().Description);
            Assert.AreEqual("First Image Title - Changed", repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault().Title);
         }
      }

      [Test]
      public void Change_To_Description_Is_Saved_And_Returned_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault();

            image.Description = "First Image Title - Changed";

            string oldTitle = image.Title;

            repository.Save(image);

            Assert.AreEqual(oldTitle, repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault().Title);
            Assert.AreEqual("First Image Title - Changed", repository.GetAllImages(new TestBinaryRepository()).FirstOrDefault().Description);
         }
      }

      [Test]
      public void Change_Private_Image_To_Public_Saves_And_Returns_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.IsNotNull(image);
            Assert.IsFalse(image.IsPublic);

            image.IsPublic = true;

            repository.Save(image);

            TadmapImage latestImage = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.IsTrue(latestImage.IsPublic);
         }
      }

      [Test]
      public void Returns_Image_With_Correct_Owner_Name()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.AreEqual("http://taduser.myopenid.com/", image.OwnerName);
         }
      }

      [Test]
      public void Returns_Image_With_ImageSet()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.IsNotNull(image.ImageSet);
         }
      }


      [Test]
      public void Returns_Image_With_Correct_Original_Key()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.AreEqual("f0b999ba-36b1-4cc2-beb1-455367b5897a.gif", image.ImageSet.Original);
         }
      }

      [Test]
      public void Returns_Image_With_Correct_Original_Preview_Key()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("ede4567f-32be-4aba-97ea-a0c6be3fcbfd")).SingleOrDefault();

            Assert.AreEqual("Preview_f0b999ba-36b1-4cc2-beb1-455367b5897a.gif", image.ImageSet.Preview);
         }
      }

      [Test]
      public void Change_Public_Image_To_Private_Saves_And_Returns_Correctly()
      {
         using (TransactionScope transaction = new TransactionScope())
         {
            IImageRepository repository = new SqlImageRepository();

            TadmapImage image = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59")).SingleOrDefault();

            Assert.IsNotNull(image);
            Assert.IsTrue(image.IsPublic);

            image.IsPublic = false;

            repository.Save(image);

            TadmapImage latestImage = repository.GetAllImages(new TestBinaryRepository()).WithId(new Guid("57c95cb2-dea3-486b-a951-d650e346ab59")).SingleOrDefault();

            Assert.IsFalse(latestImage.IsPublic);
         }
      }
   }
}
