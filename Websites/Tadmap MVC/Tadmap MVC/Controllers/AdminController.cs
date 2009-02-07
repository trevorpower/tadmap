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

      public AdminController()
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = new SqlImageRepository();
      }

      public AdminController(IImageRepository imageRepository)
      {
         _imageRepository = imageRepository;
      }

      [Authorize(Roles=TadmapRoles.Administrator)]
      public ActionResult Index(IPrincipal principal)
      {
         //if (principal.Identity.IsAuthenticated && principal.IsInRole(TadmapRoles.Administrator))
         {
            ViewData.Model = _imageRepository.GetAllImages().ToList();

            return View();
         }

         //return RedirectToRoute("Default", new { Controller = "Home", Action = "Index" });
      }
   }
}
