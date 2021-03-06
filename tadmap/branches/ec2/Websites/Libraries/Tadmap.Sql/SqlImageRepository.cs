﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tadmap.Model.Image;


namespace Tadmap.Sql
{
   public class SqlImageRepository : IImageRepository
   {
      #region IImageRepository Members

      public IQueryable<TadmapImage> GetAllImages(IBinaryRepository binaryRepository)
      {
         TadmapDb db = new TadmapDb();

         return from i in db.UserImages
                select new TadmapImage(binaryRepository)
                {
                   Id = i.Id,
                   Title = i.Title,
                   Description = i.Description,
                   Key = i.Key,
                   IsPublic = i.Privacy == 1,
                   IsOffensive = i.OffensiveCount > 0,
                   OwnerName = i.User.UserOpenIds.Single().OpenIdUrl,
                   ImageSet = new ImageSet1(i.Key)
                };
      }

      public void Save(TadmapImage image)
      {
         bool isNew = false;

         TadmapDb db = new TadmapDb();
         UserImage dbImage = db.UserImages.SingleOrDefault(i => i.Id == image.Id);

         if (dbImage == null)
         {
            dbImage = new UserImage();

            // can't change date added, id or owner
            dbImage.DateAdded = DateTime.Now;
            dbImage.UserId = image.UserId;
            dbImage.Id = image.Id;

            isNew = true;
         }
         else
         {
            isNew = false;
         }

         dbImage.Title = image.Title;
         dbImage.Description = image.Description;
         dbImage.Key = image.Key;
         dbImage.OffensiveCount = image.IsOffensive ? (byte)1 : (byte)0;
         dbImage.Privacy = image.IsPublic ? (byte)1 : (byte)0;

         if (isNew)
            db.UserImages.InsertOnSubmit(dbImage);

         db.SubmitChanges();
      }

      #endregion
   }
}
