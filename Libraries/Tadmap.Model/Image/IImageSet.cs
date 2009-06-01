using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
using System.IO;

namespace Tadmap.Model.Image
{
   public interface IImageSet
   {
      string Original { get; }
      string Preview { get; }
      string LargeThumb { get; }
      string Square { get; }

      void Create(
         Stream stream,
         IBinaryRepository binaryRepository,
         out int zoomLevels,
         out int tileSize
      );
   }
}
