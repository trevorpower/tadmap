using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Tadmap_MVC.Models.Images
{
   public class ImageNotFoundException : Exception
   {
      public ImageNotFoundException()
         : base()
      {
      }
   }
}
