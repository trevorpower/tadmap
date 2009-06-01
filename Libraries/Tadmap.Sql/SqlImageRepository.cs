using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tadmap.Model.Image;


namespace Tadmap.Sql
{
   public class SqlImageRepository : IImageRepository
   {
      private string ConnectionString {get; set;}

      public SqlImageRepository(string connectionString)
      {
         ConnectionString = connectionString;
      }

      #region IImageRepository Members

      public IQueryable<TadmapImage> GetAllImages()
      {
         var db = new TadmapDb(ConnectionString);

         return from i in db.Images
                select new TadmapImage
                {
                   Id = i.Id,
                   Title = i.Title,
                   Description = i.Description,
                   Key = i.Key,
                   IsPublic = i.Privacy == 1,
                   IsOffensive = i.OffensiveCount > 0,
                   OwnerName = i.User.OpenIds.Single().OpenIdUrl,
                   ImageSet = new ImageSet1(i.Key),
                   UserId = i.User.Id,
                   HasIcon = i.ThumbnailAvailable,
                   ZoomLevel = i.ZoomLevels ?? 0,
                   TileSize = i.TileSize ?? 0
                };
      }

      public void Save(TadmapImage image)
      {
         bool isNew = false;

         TadmapDb db = new TadmapDb(ConnectionString);
         Image dbImage = db.Images.SingleOrDefault(i => i.Id == image.Id);

         if (dbImage == null)
         {
            dbImage = new Image();

            // can't change date added, id or owner
            dbImage.DateAdded = DateTime.Now;
            dbImage.UserId = image.UserId;

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
         dbImage.TileSize = image.TileSize;
         dbImage.ZoomLevels = image.ZoomLevel > 0 ? new int?(image.ZoomLevel) : null;
         dbImage.ThumbnailAvailable = image.HasIcon;

         if (isNew)
            db.Images.InsertOnSubmit(dbImage);

         db.SubmitChanges();
      }

      #endregion
   }
}
