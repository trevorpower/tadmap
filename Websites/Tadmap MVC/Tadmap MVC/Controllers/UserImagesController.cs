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

namespace Tadmap.Controllers
{
   public class UserImagesController : Controller
   {
      private IImageRepository ImageRepository { get; set; }
      private IBinaryRepository BinaryRepository { get; set; }

      public UserImagesController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         ImageRepository = imageRepository;
         BinaryRepository = binaryRepository;
      }

      [Authorize(Roles=TadmapRoles.Collector)]
      public ActionResult Index(IPrincipal principal)
      {
         if (Request.IsAuthenticated)
         {
            List<TadmapImage> images = ImageRepository.GetAllImages(BinaryRepository)
             .IsOwnedBy((principal.Identity as TadmapIdentity).Id).ToList();

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
