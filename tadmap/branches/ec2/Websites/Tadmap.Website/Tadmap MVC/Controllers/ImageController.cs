﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Tadmap.Configuration;
using System.Security;
using Tadmap;
using System.Security.Principal;
using Tadmap.DataAccess;
using Tadmap.Model.Image;
using Tadmap.Security;
using Tadmap.Model;
using Tadmap.Tadmap.Security;

namespace Tadmap.Controllers
{
   public class ImageController : Controller
   {
      private IImageRepository _imageRepository;
      private IBinaryRepository _binaryRepository;

      public ImageController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;
      }

      public ActionResult Index(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         try
         {
            TadmapImage image = _imageRepository.GetAllImages(_binaryRepository).WithId(id).Single();

            ImageView model = new ImageView(image.Id, image.Title, image.Description, null);

            if (image.IsOffensive)
            {
               if (!principal.IsInRole(TadmapRoles.Administrator))
                  throw new SecurityException("Only administrator can view images marked as offensive");

               model.OffensiveCount = 1;
               model.ShowOffensiveCount = true;
               model.CanUnmarkOffensive = true;
            }
            else
            {
               model.OffensiveCount = 0;
               model.ShowOffensiveCount = principal.IsInRole(TadmapRoles.Administrator);
               model.CanMarkOffensive = principal.IsInRole(TadmapRoles.Administrator);
            }

            if (principal.Identity.Name == image.OwnerName) // owner
            {
               model.IsEditable = principal.Identity.Name == image.OwnerName;
               model.OriginalUrl = _binaryRepository.GetUrl(image.Key);
            }
            else if (principal.IsInRole(TadmapRoles.Administrator)) // administrator
            {
               model.OriginalUrl = _binaryRepository.GetUrl(image.Key);
            }
            else // guest
            {
               model.OriginalUrl = null;
            }

            model.PreviewUrl = _binaryRepository.GetUrl(image.ImageSet.Preview);

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
               //      if (!HttpContext.User.IsInRole(TadmapRoles.Administrator))
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

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult MakePublic(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository).WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = true;

         _imageRepository.Save(image);

         return Json(1);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult MakePrivate(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository).WithId(id).SingleOrDefault();
         
         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = false;

         _imageRepository.Save(image);

         return Json(0);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult UpdateTitle(Guid id, string title, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (title == null)
            throw new ArgumentNullException("title");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository)
            .IsOwnedBy((principal.Identity as TadmapIdentity).Id)
            .WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.Title = title;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult UpdateDescription(Guid id, string description, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentNullException("id");

         if (description == null)
            throw new ArgumentNullException("description");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository)
            .IsOwnedBy((principal.Identity as TadmapIdentity).Id)
            .WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.Description = description;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Administrator)]
      public ActionResult Mark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadmapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as offensive.");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository).WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = true;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Administrator)]
      public ActionResult UnMark(Guid id, IPrincipal principal)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!principal.IsInRole(TadmapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as un-offensive.");

         TadmapImage image = _imageRepository.GetAllImages(_binaryRepository).WithId(id).SingleOrDefault();
         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = false;

         _imageRepository.Save(image);

         return Json(true);
      }
   }
}
