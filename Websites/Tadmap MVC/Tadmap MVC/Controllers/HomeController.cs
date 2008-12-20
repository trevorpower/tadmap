using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Tadmap_MVC.Controllers
{
   [HandleError]
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         TadmapDb db = new TadmapDb();

         var images = from i in db.UserImages
                      where i.OffensiveCount == 0 && i.Privacy > 0
                      select i;

         ViewData.Model = images.ToList();

         return View();
      }

      public ActionResult About()
      {
         throw new NotImplementedException("'About' not implemented.");
      }
   }
}
