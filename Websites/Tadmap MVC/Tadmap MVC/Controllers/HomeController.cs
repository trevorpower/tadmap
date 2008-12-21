using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.SQL;
using Tadmap_MVC.Models.Images;

namespace Tadmap_MVC.Controllers
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
         ViewData.Model = _imageRepository.GetAllImages().IsPublic().ToList();

         return View();
      }

      public ActionResult About()
      {
         throw new NotImplementedException("'About' not implemented.");
      }
   }
}
