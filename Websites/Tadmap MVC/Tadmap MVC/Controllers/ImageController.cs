using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TadMap.Security;
using TadMap.Configuration;
using System.Security;
using TadMap;
using System.Security.Principal;
using Tadmap_MVC.Models.Images;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.SQL;
using Tadmap_MVC.Views.Image;
using Tadmap_MVC.DataAccess.S3;

namespace Tadmap_MVC.Controllers
{
   public class ImageController : Controller
   {
      private IImageRepository _imageRepository;
      private IBinaryRepository _binaryRepository;

      public ImageController()
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = new SqlImageRepository();
         _binaryRepository = new S3BinaryRepository();
      }

      public ImageController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;
      }

      public ActionResult Index(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         try
         {
            TadmapImage image = _imageRepository.GetAllImages().WithId(id).Single();

            ImageView model = new ImageView(image.Id, image.Title, image.Description, "PreviewUrl", null);

            if (image.IsOffensive)
            {
               if (!principal.IsInRole(TadMapRoles.Administrator))
                  throw new SecurityException("Only administrator can view images marked as offensive");

               model.OffensiveCount = 1;
               model.ShowOffensiveCount = true;
               model.CanUnmarkOffensive = true;
            }
            else
            {
               model.OffensiveCount = 0;
               model.ShowOffensiveCount = principal.IsInRole(TadMapRoles.Administrator);
               model.CanMarkOffensive = principal.IsInRole(TadMapRoles.Administrator);
            }

            model.IsEditable = principal.Identity.Name == image.OwnerName;

            model.IsPublic = image.IsPublic;

            if (principal.Identity.IsAuthenticated)
            {
               //UserOpenId openId = db.UserOpenIds.Single(i => i.OpenIdUrl == HttpContext.User.Identity.Name);

               //if (image.UserId == openId.UserId)
               //{
               //   // Owning user
               //   ViewData["CanEdit"] = true;
               //}
               //else
               //{
               //   // Other registered user
               //   if (image.Privacy == 0)
               //      if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
               //         throw new SecurityException("Only owner or administrator can view private image");
               //}
            }
            else if (!image.IsPublic)
            {
               throw new SecurityException("User must be authenticated to view a private image");
            }

            ViewData.Model = model;
         }
         catch (InvalidOperationException)
         {
            throw new ImageNotFoundException();
         }

         return View();
      }

      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult MakePublic(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = true;

         _imageRepository.Save(image);

         return Json(1);
      }

      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult MakePrivate(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         
         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = false;

         _imageRepository.Save(image);

         return Json(0);
      }

      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult UpdateTitle(Guid id, string title)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (title == null)
            throw new ArgumentNullException("title");

         return Json(TadImage.UpdateTitle(id, title, HttpContext.User) > 0);
      }

      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult UpdateDescription(Guid id, string description)
      {
         if (id == Guid.Empty)
            throw new ArgumentNullException("id");

         if (description == null)
            throw new ArgumentNullException("description");

         return Json(TadImage.UpdateDescription(id, description, HttpContext.User) > 0);
      }

      [Authorize(Roles = TadMapRoles.Administrator)]
      public ActionResult Mark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as offensive.");

         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();


         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = true;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadMapRoles.Administrator)]
      public ActionResult UnMark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as un-offensive.");

         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = false;

         _imageRepository.Save(image);

         return Json(true);
      }
   }
}
