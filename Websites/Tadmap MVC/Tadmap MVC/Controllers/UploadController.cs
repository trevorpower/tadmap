using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;
using System.IO;
using TadMap.Security;
using TadMap;

namespace Tadmap_MVC.Controllers
{
   public class UploadController : Controller
   {
      public ActionResult Index()
      {
         if (!Request.IsAuthenticated)
            return new RedirectResult(FormsAuthentication.LoginUrl);

         return View();
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Index(string title, string description)
      {
         HttpPostedFileBase oFile = Request.Files["file"];

         if (oFile.ContentLength > 0)
         {
            TadImage oNewImage = TadImage.NewTadImage(oFile.InputStream, Guid.NewGuid() + Path.GetExtension(oFile.FileName));
            oNewImage.Title = title;
            oNewImage.Description = description;
            oNewImage.Save(HttpContext.User.Identity);
         }

         return RedirectToAction("Index", "Home");
      }
   }
}