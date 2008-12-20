using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Tadmap_MVC.Models.Images
{
   public class ImageNotFound : Exception
   {
      public ImageNotFound():base()
      {
      }
   }
}
