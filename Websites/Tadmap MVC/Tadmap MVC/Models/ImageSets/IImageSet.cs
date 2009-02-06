using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
using System.IO;

namespace Tadmap.Models.ImageSets
{
   public interface IImageSet
   {
      string Original { get; }
      string Preview { get; }

      void Create(Stream stream, IBinaryRepository binaryRepository);
   }
}
