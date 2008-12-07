using System.Web.Mvc;
using TadMap.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TadMap_MVC.Views.Home
{
   public partial class Index : ViewPage
   {
      public static List<UserImage> ImageList
      {
         get
         {
            TadmapDb db = new TadmapDb(Database.TadMapConnection);

            var images = from i in db.UserImages
                         where i.OffensiveCount == 0 && i.Privacy > 0
                         select i;

            return images.ToList();
         }
      }
   }
}
