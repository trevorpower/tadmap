using System.Web.Mvc;
using System.Web.Security;
using System;
using System.IO;
using TadMap.Security;
using TadMap;

namespace Tadmap_MVC.Controllers
{
   public class UserImagesController : Controller
   {
      public ActionResult Index()
      {
         if (Request.IsAuthenticated)
         {
            ViewData["ImageList"] = TadImageList.GetTadImageList(HttpContext.User.Identity);

            return View();
         }
         else
         {
            return new RedirectResult(FormsAuthentication.LoginUrl);
         }
      }

      //void m_repMapRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
      //{
      //   TadImage oImageInfo = e.Item.DataItem as TadImage;

      //   LinkButton oLink = e.Item.FindControl("m_lbName") as LinkButton;
      //   oLink.Text = oImageInfo.Title;
      //   oLink.PostBackUrl = "ViewMap.aspx?ImageId=" + oImageInfo.Id;

      //   Label oLabel = e.Item.FindControl("m_lblDescription") as Label;
      //   oLabel.Text = oImageInfo.Description;

      //   Image oImage = e.Item.FindControl("m_imgImage") as Image;
      //   oImage.Width = 80;
      //   oImage.Height = 80;

      //   PostBackOptions options = new PostBackOptions(oLink, "", "ViewMap.aspx?ImageId=" + oImageInfo.Id, true, false, false, true, false, "");

      //   HtmlControl oDiv = e.Item.FindControl("ListItem") as HtmlControl;
      //   oDiv.Attributes.Add("onClick", ClientScript.GetPostBackEventReference(options));
      //   oDiv.Attributes.Add("onMouseOver", "this.style.background = '#FFFFCC';");
      //   oDiv.Attributes.Add("onMouseOut", "this.style.background = '#FFFFFF';");

      //   ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
      //   oImage.ImageUrl = s3.GetUrl(S3Storage.BucketName, "Square_" + oImageInfo.StorageKey);
      //   //oImage.ImageUrl = "http://" + S3Storage.BucketName + ".s3.amazonaws.com/Square_" + oImageInfo.StorageKey;
      //}
   }
}
