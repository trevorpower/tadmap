using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap_MVC.Models.ImageSets
{
   public class ImageSet1 : IImageSet
   {
      private string Key { get; set; }

      public ImageSet1(string key)
      {
         Key = key;
      }

      #region IImageSet Members

      public string Original
      {
         get { return Key; }
      }

      public string Preview
      {
         get { return "Preview_" + Key; }
      }

      #endregion
   }
}
