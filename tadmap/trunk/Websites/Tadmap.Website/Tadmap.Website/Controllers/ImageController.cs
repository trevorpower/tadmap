using System;
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

      public ActionResult Index(int id, IPrincipal principal)
      {
         try
         {
            TadmapImage image = _imageRepository.GetAllImages().WithId(id).Single();

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

            model.StorageKey = image.Key;

            if (image.HasIcon)
               model.PreviewUrl = _binaryRepository.GetUrl(image.ImageSet.Preview);

            model.IsPublic = image.IsPublic;

            model.TileSize = image.TileSize;
            model.ZoomLevels = image.ZoomLevel;

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
      public ActionResult MakePublic(int id)
      {
         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = true;

         _imageRepository.Save(image);

         return Json(1);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult MakePrivate(int id)
      {
         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         
         if (image == null)
            throw new ImageNotFoundException();

         image.IsPublic = false;

         _imageRepository.Save(image);

         return Json(0);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult UpdateTitle(int id, string title, IPrincipal principal)
      {
         if (title == null)
            throw new ArgumentNullException("title");

         TadmapImage image = _imageRepository.GetAllImages()
            .IsOwnedBy((principal.Identity as TadmapIdentity).Id)
            .WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.Title = title;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult UpdateDescription(int id, string description, IPrincipal principal)
      {
         if (description == null)
            throw new ArgumentNullException("description");

         TadmapImage image = _imageRepository.GetAllImages()
            .IsOwnedBy((principal.Identity as TadmapIdentity).Id)
            .WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.Description = description;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Administrator)]
      public ActionResult Mark(int id, IPrincipal principal)
      {
         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();

         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = true;

         _imageRepository.Save(image);

         return Json(true);
      }

      [Authorize(Roles = TadmapRoles.Administrator)]
      public ActionResult UnMark(int id, IPrincipal principal)
      {
         TadmapImage image = _imageRepository.GetAllImages().WithId(id).SingleOrDefault();
         if (image == null)
            throw new ImageNotFoundException();

         image.IsOffensive = false;

         _imageRepository.Save(image);

         return Json(true);
      }

      public ActionResult GetTile(IPrincipal principal, int id, int tileX, int tileY, int zoom)
      {
         var image = _imageRepository.GetAllImages().WithId(id).Single();

         if (!image.IsPublic && image.UserId != (principal.Identity as TadmapIdentity).Id)
            throw new ImageNotFoundException();

         return Json(new { url = _binaryRepository.GetUrl(string.Format("Tile_{0}_{1}_{2}_{3}", tileX, tileY, zoom, image.Key)) });
      }

      public ActionResult GetTiles(IPrincipal principal, int id)
      {
         var image = _imageRepository.GetAllImages().WithId(id).Single();

         if (!image.IsPublic && image.UserId != (principal.Identity as TadmapIdentity).Id)
            throw new ImageNotFoundException();

         var tiles = new string[image.ZoomLevel + 1][,];

         int numberOfTiles = 1;

         for (int z = 0; z <= image.ZoomLevel; z++)
         {
            tiles[z] = new string[numberOfTiles, numberOfTiles];

            for (int x = 0; x < numberOfTiles; x++)
            {
               for (int y = 0; y < numberOfTiles; y++)
               {
                  tiles[z][x,y] = _binaryRepository.GetUrl(string.Format("Tile_{0}_{1}_{2}_{3}", x, y, z, image.Key)).OriginalString;
               }
            }

            numberOfTiles *= 2;
         }

         return Json(tiles);
      }
   }
}
