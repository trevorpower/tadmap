using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.SQL;
using TadMap.Security;
using System.Security;

namespace Tadmap_MVC.Controllers
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

      [Authorize(Roles=TadMapRoles.Administrator)]
      public ActionResult Index(IPrincipal principal)
      {
         //if (principal.Identity.IsAuthenticated && principal.IsInRole(TadMapRoles.Administrator))
         {
            ViewData.Model = _imageRepository.GetAllImages().ToList();

            return View();
         }

         //return RedirectToRoute("Default", new { Controller = "Home", Action = "Index" });
      }
   }
}
