using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Principal;
using Tadmap.DataAccess;
using Tadmap.DataAccess.SQL;
using Tadmap.Security;
using System.Security;

namespace Tadmap.Controllers
{
   public class AdminController : Controller
   {
      private IImageRepository _imageRepository;
      private IBinaryRepository _binaryRepository;

      public AdminController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;
      }

      [Authorize(Roles=TadmapRoles.Administrator)]
      public ActionResult Index(IPrincipal principal)
      {
         //if (principal.Identity.IsAuthenticated && principal.IsInRole(TadmapRoles.Administrator))
         {
            ViewData.Model = _imageRepository.GetAllImages(_binaryRepository).ToList();

            return View();
         }

         //return RedirectToRoute("Default", new { Controller = "Home", Action = "Index" });
      }

      public ActionResult Works()
      {
         return Json(string.Empty);
      }

      public ActionResult ThrowsException()
      {
         throw new NotSupportedException("Exception thrown, as requested!");
      }

      [Authorize(Roles="NeverAuthorized")]
      public ActionResult NotAuthorized()
      {
         return View();
      }

   }
}
