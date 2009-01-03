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
   }
}
