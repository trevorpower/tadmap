using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap_MVC.Models.Images
{
   public static class TadmapImageFilters
   {
      public static IQueryable<TadmapImage> IsPublic(this IQueryable<TadmapImage> query)
      {
         return from i in query
                where i.IsPublic
                select i;
      }

   }
}
