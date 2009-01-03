using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tadmap_MVC.Models.Images;

namespace Tadmap_MVC.DataAccess.SQL
{
   public class SqlImageRepository : IImageRepository
   {
      #region IImageRepository Members

      public IQueryable<TadmapImage> GetAllImages()
      {
         TadmapDb db = new TadmapDb();

         return from i in db.UserImages
                select new TadmapImage
                {
                   Id = i.Id,
                   Title = i.Title,
                   Description = i.Description,
                   Key = i.Key,
                   IsPublic = i.Privacy == 1,
                   IsOffensive = i.OffensiveCount > 0
                };
      }

      public void MarkAsOffensive(Guid id)
      {
         throw new NotImplementedException();
      }

      public void MarkAsUnOffensive(Guid id)
      {
         throw new NotImplementedException();
      }

      public void MarkAsPublic(Guid id)
      {
         throw new NotImplementedException();
      }

      public void MarkAsPrivate(Guid id)
      {
         throw new NotImplementedException();
      }

      public void Save(TadmapImage image)
      {
         bool isNew = false;

         TadmapDb db = new TadmapDb();
         UserImage dbImage = db.UserImages.SingleOrDefault(i => i.Id == image.Id);
         
         if (dbImage == null)
         {
            dbImage = new UserImage();

            // can't chage date added or owner
            dbImage.DateAdded = DateTime.Now;
            dbImage.UserId = image.UserId;

            isNew = true;
         }
         else
         {
            isNew = false;
         }

         dbImage.Id = image.Id;
         dbImage.Title = image.Title;
         dbImage.Description = image.Description;
         dbImage.Key = image.Key;
         
         if (isNew)
            db.UserImages.InsertOnSubmit(dbImage);
         
         db.SubmitChanges();
      }

      #endregion
   }
}
