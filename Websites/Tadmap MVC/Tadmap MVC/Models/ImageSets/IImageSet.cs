using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap_MVC.Models.ImageSets
{
   public interface IImageSet
   {
      string Original { get; }
      string Preview { get; }
   }
}
