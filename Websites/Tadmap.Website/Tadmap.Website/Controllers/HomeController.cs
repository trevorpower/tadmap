using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Tadmap.DataAccess;
using Tadmap.Model.Image;
using Tadmap.Model;
using Tadmap.Website.Models;

namespace Tadmap.Controllers
{
   [HandleError]
   public class HomeController : Controller
   {
      private IImageRepository ImageRepository { get; set; }
      private IBinaryRepository BinaryRepository { get; set; }

      public HomeController(IImageRepository imageRepository, IBinaryRepository binaryRepository)
      {
         ImageRepository = imageRepository;
         BinaryRepository = binaryRepository;
      }

      public ActionResult Index()
      {
         List<ImageItem> images = ImageRepository.GetAllImages()
            .IsPublic()
            .IsNotOffensive()
            .Select(i =>
               new ImageItem
               {
                  Id = i.Id,
                  Title = i.Title,
                  Description = i.Description,
                  SquareUrl = BinaryRepository.GetUrl(i.ImageSet.Square)
               }
            ).ToList();

         ViewData.Model = new HomeView
         {
            Images = images
         };

         return View();
      }
   }
}
