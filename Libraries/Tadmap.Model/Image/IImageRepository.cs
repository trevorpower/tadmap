using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Models.Images;

namespace Tadmap.DataAccess
{
   public interface IImageRepository
   {
      IQueryable<TadmapImage> GetAllImages(IBinaryRepository binaryRepository);

      void Save(TadmapImage image);
   }
}
