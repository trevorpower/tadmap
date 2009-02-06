using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Tadmap.DataAccess;
using Tadmap.DataAccess.SQL;
using Tadmap.Models.Images;

namespace Tadmap.Controllers
{
   [HandleError]
   public class HomeController : Controller
   {
      private IImageRepository _imageRepository;

      public HomeController()
      {
         _imageRepository = new SqlImageRepository();
      }

      public HomeController(IImageRepository imageRepository)
      {
         _imageRepository = imageRepository;
      }

      public ActionResult Index()
      {
         ViewData.Model =_imageRepository.GetAllImages()
            .IsPublic()
            .IsNotOffensive().ToList();

         return View();
      }
   }
}
