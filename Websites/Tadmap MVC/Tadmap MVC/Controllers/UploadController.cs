using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;
using System.IO;
using TadMap.Security;
using TadMap;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.Models.Images;
using Tadmap_MVC.DataAccess.SQL;
using Tadmap_MVC.DataAccess.S3;
using Tadmap_MVC.Models.ImageSets;
using Tadmap_MVC.Tadmap.Security;

namespace Tadmap_MVC.Controllers
{
   public class UploadController : Controller
   {
      IImageRepository _imageRepository;
      IBinaryRepository _binaryRepository;

      public UploadController()
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = new SqlImageRepository();
         _binaryRepository = new S3BinaryRepository();
      }

      public UploadController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;
      }

      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult Index()
      {
         return View();
      }

      [AcceptVerbs(HttpVerbs.Post)]
      [Authorize(Roles = TadMapRoles.Collector)]
      public ActionResult Upload(string title, string description, IPrincipal principal, IUploadedFile file)
      {
         if (file.ContentLength > 0)
         {
            TadmapImage image = new TadmapImage();
            image.Id = Guid.NewGuid();
            image.Title = title ?? Path.GetFileNameWithoutExtension(file.FileName);
            image.Description = description ?? "";
            image.Key = Guid.NewGuid() + Path.GetExtension(file.FileName);
            image.ImageSet = new ImageSet1(image.Key);
            image.UserId = (principal.Identity as TadmapIdentity).Id;
            image.ImageSet.Create(file.InputStream, _binaryRepository);

            _imageRepository.Save(image);
         }

         return RedirectToAction("Index", "Home");
      }
   }
}