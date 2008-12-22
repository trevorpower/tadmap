using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap_MVC.Models.Images
{
   public class TadmapImage
   {
      public TadmapImage()
      {
      }

      public TadmapImage(
         Guid id,
         string title,
         string description,
         string key,
         bool isPublic,
         bool isOffensive
      )
      {
         Id = id;
         Title = title;
         Description = description;
         Key = key;
         IsPublic = isPublic;
         IsOffensive = isOffensive;
      }

      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string Key { get; set; }
      public bool IsPublic { get; set; }
      public bool IsOffensive { get; set; }
   }
}
