using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap_MVC.Views.Image
{
   public class ImageView
   {
      public ImageView()
      {
      }

      public ImageView(
         Guid id,
         string title,
         string description,
         bool isEditable,
         bool isPublic,
         string previewUrl,
         string originalUrl
      )
      {
         Id = id;
         Title = title;
         Description = description;
         IsEditable = isEditable;
         IsPublic = isPublic;
         PreviewUrl = previewUrl;
         OriginalUrl = originalUrl;
      }

      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public bool IsEditable { get; set; }
      public bool IsPublic { get; set; }
      public string PreviewUrl { get; set; }
      public string OriginalUrl { get; set; }

      public int OffensiveCount { get; set; }
      public bool ShowOffensiveCount { get; set; }

      public bool CanMarkOffensive { get; set; }
      public bool CanUnmarkOffensive { get; set; }
   }
}
