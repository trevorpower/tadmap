using System.Web.Mvc;
using System.Web.Security;
using System;
using System.IO;
using Tadmap.Security;
using Tadmap;
using System.Collections.Generic;
using Tadmap.Model.Image;
using System.Linq;
using Tadmap.Model;
using System.Security.Principal;
using Tadmap.Tadmap.Security;
using Tadmap.Website.Models;

namespace Tadmap.Controllers
{
   public class UserImagesController : Controller
   {
      private IImageRepository ImageRepository { get; set; }
      private IBinaryRepository BinaryRepository { get; set; }

      public UserImagesController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();

         ImageRepository = imageRepository;
         BinaryRepository = binaryRepository;
      }

      //[Authorize(Roles=TadmapRoles.Collector)]
      public ActionResult Index(IPrincipal principal)
      {
         if (Request.IsAuthenticated)
         {
            List<ImageItem> images = ImageRepository.GetAllImages()
             .IsOwnedBy((principal.Identity as TadmapIdentity).Id)
             .Select(i =>
               new ImageItem
               {
                  Id = i.Id,
                  Title = i.Title,
                  Description = i.Description,
                  SquareUrl = BinaryRepository.GetUrl(i.ImageSet.Square)
               }
            ).ToList();

            ViewData.Model = images;

            return View();
         }
         else
         {
            return new RedirectResult(FormsAuthentication.LoginUrl);
         }
      }
   }
}
