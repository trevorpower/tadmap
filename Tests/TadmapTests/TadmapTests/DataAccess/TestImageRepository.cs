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

         for (int i = 0; i < 10; i++)
            images.Add(new TadmapImage { Description = "description", Key = "Key", Title = "Title " + i, IsPublic = i < 5 });

         images[0].IsOffensive = true;
         images[1].Id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf1");
         images[8].Id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf8");
         images[9].Id = new Guid("16b4d816-2e1e-4d54-9b66-78ef0fb7cbf9");
         images[9].IsOffensive = true;

         return images.AsQueryable();
      }

      #endregion
   }
}
