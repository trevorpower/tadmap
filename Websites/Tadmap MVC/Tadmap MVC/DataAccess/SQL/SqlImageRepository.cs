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

      #endregion
   }
}
