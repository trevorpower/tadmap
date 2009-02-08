using System.Web.Mvc;
using System.Web.Security;
using System;
using System.IO;
using Tadmap.Security;
using Tadmap;

namespace Tadmap.Controllers
{
   public class UserImagesController : Controller
   {
      public ActionResult Index()
      {
         if (Request.IsAuthenticated)
         {
            ViewData["ImageList"] = TadImageList.GetTadImageList(HttpContext.User.Identity);

            return View();
         }
         else
         {
            return new RedirectResult(FormsAuthentication.LoginUrl);
         }
      }
   }
}
