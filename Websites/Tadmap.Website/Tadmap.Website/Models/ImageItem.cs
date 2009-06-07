using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap.Website.Models
{
   public class ImageItem
   {
      public int Id { get; set; }

      public string Title { get; set; }

      public string Description { get; set; }

      public Uri SquareUrl { get; set; }
   }
}
