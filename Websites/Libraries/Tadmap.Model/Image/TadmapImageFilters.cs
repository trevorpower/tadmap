using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap.Model.Image
{
   public static class TadmapImageFilters
   {
      public static IQueryable<TadmapImage> IsPublic(this IQueryable<TadmapImage> query)
      {
         return from i in query
                where i.IsPublic
                select i;
      }

      public static IQueryable<TadmapImage> IsNotOffensive(this IQueryable<TadmapImage> query)
      {
         return from i in query
                where !i.IsOffensive
                select i;
      }

      public static IQueryable<TadmapImage> IsOwnedBy(this IQueryable<TadmapImage> query, Guid id)
      {
         return from i in query
                where i.UserId == id
                select i;
      }

      public static IQueryable<TadmapImage> WithId(this IQueryable<TadmapImage> query, Guid id)
      {
         return from i in query
                where i.Id == id
                select i;
      }
   }
}
