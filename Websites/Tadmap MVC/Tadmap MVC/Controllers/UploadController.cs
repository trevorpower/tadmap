using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using System;
using System.IO;
using TadMap.Security;

namespace Tadmap_MVC.Controllers
{
   public class UploadController : Controller
   {
      public ActionResult Index()
      {
         if (!Request.IsAuthenticated)
            return new RedirectResult(FormsAuthentication.LoginUrl);

         return View("Upload");
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Upload()
      {
         //HttpPostedFile oFile = FileInput.PostedFile;

         //if (oFile.ContentLength > 0)
         //{
         //   TadImage oNewImage = TadImage.NewTadImage(oFile.InputStream, Guid.NewGuid() + Path.GetExtension(oFile.FileName));
         //   oNewImage.Title = m_txtTitle.Text;
         //   oNewImage.Description = m_txtDescription.Text;
         //   oNewImage.Save(HttpContext.Current.User.Identity);
         //}

         //Response.Redirect("Default.aspx", true);

         return View();
      }
   }
}