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

namespace Tadmap_MVC.Controllers
{
   public class ImageController : Controller
   {
      private IImageRepository _imageRepository;

      public ImageController()
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = new SqlImageRepository();
      }

      public ImageController(IImageRepository imageRepository)
      {
         _imageRepository = imageRepository;
      }

      public ActionResult Index(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapDb db = new TadmapDb();

         try
         {
            TadmapImage image = _imageRepository.GetAllImages().WithId(id).Single();

            //if (image.IsOffensive)
            //{
            //   if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
            //      throw new SecurityException("Only administrator can view images marked as offensive");
            //}

            //ViewData["CanEdit"] = false;

            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //   //UserOpenId openId = db.UserOpenIds.Single(i => i.OpenIdUrl == HttpContext.User.Identity.Name);

            //   //if (image.UserId == openId.UserId)
            //   //{
            //   //   // Owning user
            //   //   ViewData["CanEdit"] = true;
            //   //}
            //   //else
            //   //{
            //   //   // Other registered user
            //   //   if (image.Privacy == 0)
            //   //      if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
            //   //         throw new SecurityException("Only owner or administrator can view private image");
            //   //}
            //}
            //else if (!image.IsPublic)
            //{
            //   throw new SecurityException("User must be authenticated to view a private image");
            //}

            //ViewData["Id"] = image.Id;
            //ViewData["Title"] = image.Title;
            //ViewData["Description"] = image.Description;
            //ViewData["IsPublic"] = image.IsPublic;

            //ViewData["OriginalUrl"] = TadImage.GetOriginalUrl(image.Key);

            ViewData.Model = image;
         }
         catch (InvalidOperationException e)
         {
            throw new ImageNotFound();
         }

         return View();
      }

      public ActionResult CreateTileSet()
      {
         throw new NotImplementedException();

         //if (mImage != null)
         //{
         //   mImage.CreateTileset();
         //   mImage.Save(HttpContext.Current.User.Identity);
         //}      
      }

      public ActionResult MakePublic(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapDb db = new TadmapDb();

         try
         {
            UserImage image = db.UserImages.Single(i => i.Id == id);

            image.Privacy = 1;

            db.SubmitChanges();

            return Json(image.Privacy);
         }
         catch (InvalidOperationException e)
         {
            throw new ImageNotFound();
         }
      }

      public ActionResult MakePrivate(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapDb db = new TadmapDb();

         try
         {
            UserImage image = db.UserImages.Single(i => i.Id == id);

            image.Privacy = 0;

            db.SubmitChanges();

            return Json(image.Privacy);
         }
         catch (InvalidOperationException e)
         {
            throw new ImageNotFound();
         }
      }

      public ActionResult UpdateTitle(Guid id, string title)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (title == null)
            throw new ArgumentNullException("title");

         return Json(TadImage.UpdateTitle(id, title, HttpContext.User) > 0);
      }

      public ActionResult UpdateDescription(Guid id, string description)
      {
         if (id == Guid.Empty)
            throw new ArgumentNullException("id");

         if (description == null)
            throw new ArgumentNullException("description");

         return Json(TadImage.UpdateDescription(id, description, HttpContext.User) > 0);
      }

      public ActionResult Mark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as offensive.");

         try
         {
            TadmapDb db = new TadmapDb();

            UserImage image = db.UserImages.Single(i => i.Id == id);

            if (image == null)
               throw new ImageNotFound();

            image.OffensiveCount = 1;

            db.SubmitChanges();

            return Json(true);
         }
         catch (InvalidOperationException e)
         {
            throw new ImageNotFound();
         }
      }

      public ActionResult UnMark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as un-offensive.");

         try
         {
            TadmapDb db = new TadmapDb();

            UserImage image = db.UserImages.Single(i => i.Id == id);

            image.OffensiveCount = 0;
            
            db.SubmitChanges();

            return Json(true);
         }
         catch (InvalidOperationException)
         {
            throw new ImageNotFound();
         }
      }
   }
}
