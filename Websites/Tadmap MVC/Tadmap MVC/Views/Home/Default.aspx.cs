using System.Web.Mvc;
using TadMap.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : ViewPage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      //divMapList.Visible = true;

      //LoginText.Visible = !HttpContext.Current.User.Identity.IsAuthenticated;
   }

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
