using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;
using System.IO;

namespace Tadmap_MVC.Models.ImageSets
{
   public interface IImageSet
   {
      string Original { get; }
      string Preview { get; }

      void Create(Stream stream, IBinaryRepository binaryRepository);
   }
}
