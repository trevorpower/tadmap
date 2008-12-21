using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.Models.Images;

namespace TadmapTests.DataAccess
{
   internal class TestImageRepository : IImageRepository
   {

      #region IImageRepository Members

      public IQueryable<Tadmap_MVC.Models.Images.TadmapImage> GetAllImages()
      {
         List<TadmapImage> images = new List<TadmapImage>();

         for (int i = 0; i < 5; i++)
            images.Add(new TadmapImage { Description = "description", Key = "Key", Title = "title", IsPublic = true });

         for (int i = 0; i < 5; i++)
            images.Add(new TadmapImage { Description = "description", Key = "Key", Title = "title", IsPublic = false });

         return images.AsQueryable();
      }

      #endregion
   }
}
