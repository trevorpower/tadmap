using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;
using System.IO;
using TadMap.Security;
using TadMap;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;

namespace Tadmap_MVC.Controllers
{
   public class UploadController : Controller
   {
      public UploadController()
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
      }

      public ActionResult Index(IPrincipal principal)
      {
         if (!principal.Identity.IsAuthenticated)
            return new RedirectResult(FormsAuthentication.LoginUrl); 

         return View();
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Upload(string title, string description, IPrincipal principal, IUploadedFile file)
      {
         if (!principal.Identity.IsAuthenticated)
            return new RedirectResult(FormsAuthentication.LoginUrl);

         if (file.ContentLength > 0)
         {
            TadImage oNewImage = TadImage.NewTadImage(file.InputStream, Guid.NewGuid() + Path.GetExtension(file.FileName));
            oNewImage.Title = title;
            oNewImage.Description = description;
            oNewImage.Save(HttpContext.User.Identity);
         }

         return RedirectToAction("Index", "Home");
      }
   }
}