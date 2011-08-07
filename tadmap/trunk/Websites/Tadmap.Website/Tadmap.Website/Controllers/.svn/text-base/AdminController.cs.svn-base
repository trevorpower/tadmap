using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Principal;
using Tadmap.DataAccess;
using System.Security;
using Infrastructure.Security;
using System.Security.Permissions;
using Tadmap.Model.Image;
using Tadmap.Model;
using Tadmap.Website.Models;

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

      [Authorize(Roles = TadmapRoles.Administrator)]
      public ActionResult Index()
      {
         ViewData.Model = _imageRepository.GetAllImages().Select(i =>
            new ImageItem
            {
               Id = i.Id,
               Title = i.Title,
               Description = i.Description,
               SquareUrl = _binaryRepository.GetUrl(i.ImageSet.Square),
               IconAvailable = i.HasIcon
            }
         ).ToList();

         return View();
      }

      public ActionResult Works()
      {
         return Json(string.Empty);
      }

      public ActionResult ThrowsException()
      {
         throw new NotSupportedException("Exception thrown, as requested!");
      }

      [PrincipalPermission(SecurityAction.Demand, Role = "nada")]
      public ActionResult NotAuthorized()
      {
         return View();
      }

   }
}
