using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.Models.Images;

namespace Tadmap_MVC.DataAccess
{
   public interface IImageRepository
   {
      IQueryable<TadmapImage> GetAllImages();

      void Save(TadmapImage image);
   }
}
