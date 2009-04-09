using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;
using System.IO;
using System.Security.Principal;
using Tadmap.DataAccess;
using Tadmap.Tadmap.Security;
using Tadmap.Infrastructure;
using Infrastructure.Security;
using System.Security.Permissions;
using Tadmap.Model.Image;
using Tadmap.Model;
using Tadmap.Messaging;

namespace Tadmap.Controllers
{
   public class UploadController : Controller
   {
      IImageRepository _imageRepository;
      IBinaryRepository _binaryRepository;
      IMessageQueue _messageQueue;

      public UploadController(
         IImageRepository imageRepository,
         IBinaryRepository binaryRepository,
         [Microsoft.Practices.Unity.Dependency("Image")] IMessageQueue messageQueue
      )
      {
         ActionInvoker = new ActionInvokers.ActionInvoker();
         _imageRepository = imageRepository;
         _binaryRepository = binaryRepository;
         _messageQueue = messageQueue;
      }

      [Authorize(Roles = TadmapRoles.Collector)]
      public ActionResult Index()
      {
         return View();
      }

      [AcceptVerbs(HttpVerbs.Post)]
      [PrincipalPermission(SecurityAction.Demand, Role = TadmapRoles.Collector)]
      public ActionResult Upload(string title, string description, IPrincipal principal, IUploadedFile file)
      {
         if (file.ContentLength > 0)
         {
            TadmapImage image = new TadmapImage(_binaryRepository);
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

      public void ConfirmUpload(IPrincipal principal, string name, string key)
      {
         TadmapImage image = new TadmapImage(_binaryRepository);
         image.Id = Guid.NewGuid();
         image.Title = Path.GetFileNameWithoutExtension(name);
         image.Description = "";
         image.Key = key;
         image.ImageSet = new ImageSet1(image.Key);
         image.UserId = (principal.Identity as TadmapIdentity).Id;
         image.ImageSetVersion = 0;

         _imageRepository.Save(image);

         _messageQueue.Add(key);
      }
   }
}