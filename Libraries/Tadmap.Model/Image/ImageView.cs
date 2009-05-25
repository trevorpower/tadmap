using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap.Model.Image
{
   public class ImageView
   {
      public ImageView()
      {
      }

      public ImageView(
         int id,
         string title,
         string description,
         Uri originalUrl
      )
      {
         Id = id;
         Title = title;
         Description = description;
         OriginalUrl = originalUrl;
      }

      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public bool IsEditable { get; set; }
      public bool IsPublic { get; set; }
      public Uri PreviewUrl { get; set; }
      public Uri OriginalUrl { get; set; }

      public int OffensiveCount { get; set; }
      public bool ShowOffensiveCount { get; set; }

      public bool CanMarkOffensive { get; set; }
      public bool CanUnmarkOffensive { get; set; }
   }
}
