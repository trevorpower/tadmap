using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;

namespace Tadmap.Model.Image
{
   public interface IImageRepository
   {
      IQueryable<TadmapImage> GetAllImages(IBinaryRepository binaryRepository);

      void Save(TadmapImage image);
   }
}
