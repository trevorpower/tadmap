using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TadMap.Security;
using TadMap.Configuration;
using System.Security;
using TadMap;
using System.Security.Principal;
using Tadmap_MVC.Models.Images;

namespace Tadmap_MVC.Controllers
{
   public class ImageController : Controller
   {
      IPrincipal _principal;

      public ImageController()
      {
         _principal = HttpContext.User;
      }

      public ImageController(IPrincipal principal)
      {
         if (principal == null)
            throw new ArgumentNullException("principal");

         _principal = principal;
      }

      public ActionResult Index(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapDb db = new TadmapDb();

         UserImage image = db.UserImages.Single(i => i.Id == id);

         if (image.OffensiveCount > 0)
         {
            if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
               throw new SecurityException("Only administrator can view images marked as offensive");
         }

         ViewData["CanEdit"] = false;

         if (HttpContext.User.Identity.IsAuthenticated)
         {
            UserOpenId openId = db.UserOpenIds.Single(i => i.OpenIdUrl == HttpContext.User.Identity.Name);

            if (image.UserId == openId.UserId)
            {
               // Owning user
               ViewData["CanEdit"] = true;
            }
            else
            {
               // Other registered user
               if (image.Privacy == 0)
                  if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
                     throw new SecurityException("Only owner or administrator can view private image");
            }
         }
         else if (image.Privacy == 0)
         {
            throw new SecurityException("User must be authenticated to view a private image");
         }

         ViewData["Id"] = image.Id;
         ViewData["Title"] = image.Title;
         ViewData["Description"] = image.Description;
         ViewData["IsPublic"] = image.Privacy > 0;

         //if (image != null)
         //{
         //   ScriptManager.RegisterClientScriptBlock(Page, GetType(), "MapId", "var imageId = '" + image.Id + "';", true);
         //   OwnerControls.Visible = false;

         //   if (HttpContext.Current.User.Identity.IsAuthenticated)
         //   {
         //      UserOpenId openId = tadmap.UserOpenIds.Single(i => i.OpenIdUrl == HttpContext.Current.User.Identity.Name);

         //      if (image.Privacy == 0 && image.UserId != openId.UserId)
         //      {
         //         if (!HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
         //            throw new SecurityException("Cannot view another users image if it is marked as private.");
         //      }

         //      if (image.UserId == openId.UserId)
         //      {
         //         OwnerControls.Visible = true;
         //         ScriptManager.RegisterClientScriptInclude(Page, GetType(), "EditDetails", Page.ResolveClientUrl("JavaScript/ViewMap.js"));
         //         privacyCheckBox.Checked = image.Privacy > 0;
         //         PrivacyStatus.Text = image.Privacy == 0 ? "<b>Only you</b> can view this image." : "<b>Anyone</b> can view this image.";
         //      }
         //   }
         //   else
         //   {
         //      if (image.Privacy == 0)
         //         throw new Exception("Guest cannot view private image");
         //   }

         //   Title = "Tadmap - " + image.Title;
         //   m_lblTitle.Text = image.Title;
         //   m_lblDescription.Text = image.Description;

         //   m_imgPicture.ImageUrl = TadImage.GetPreviewUrl(image);

            ViewData["OriginalUrl"] = TadImage.GetOriginalUrl(image);

         //   // tilesets are not implemented for version the beta version one so we hide this button for now
         //   m_lbViewTileSet.Visible = false;
         //   m_lbCreateTileSet.Visible = false;
         //   //if (mImage.HasTileSet)
         //   //{
         //   //   m_lbViewTileSet.Visible = true;
         //   //   m_lbViewTileSet.PostBackUrl = "Dev.aspx?MapId=" + mImage.Id;
         //   //}
         //   //else
         //   //{
         //   //   m_lbViewTileSet.Visible = false;
         //   //}

         //   if (HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
         //   {
         //      Offensive.Visible = true;
         //      UnOffensive.Visible = true;
         //      Offensive.OnClientClick = "UpdateImage.Mark(imageId); return false;";
         //      UnOffensive.OnClientClick = "UpdateImage.UnMark(imageId); return false;";
         //   }
         //   else
         //   {
         //      Offensive.Visible = false;
         //      UnOffensive.Visible = false;
         //   }
         //}
         return View();
      }

      public ActionResult CreateTileSet()
      {
         throw new NotImplementedException();

         //if (mImage != null)
         //{
         //   mImage.CreateTileset();
         //   mImage.Save(HttpContext.Current.User.Identity);
         //}      
      }

      public ActionResult MakePublic(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         TadmapDb tadmap = new TadmapDb(Database.TadMapConnection);

         var images = from i in tadmap.UserImages
                      join u in tadmap.UserOpenIds on i.UserId equals u.UserId
                      where i.Id == id && u.OpenIdUrl == HttpContext.User.Identity.Name
                      select i;

         if (images.Count() == 1)
         {
            UserImage image = images.First();

            image.Privacy = 1;

            tadmap.SubmitChanges();

            return Json(image.Privacy);
         }
         else
         {
            throw new Exception("Could not mark as public");
         }
         throw new NotImplementedException();
      }

      public ActionResult MakePrivate(Guid id)
      {
         TadmapDb tadmap = new TadmapDb(Database.TadMapConnection);

         var images = from i in tadmap.UserImages
                      join u in tadmap.UserOpenIds on i.UserId equals u.UserId
                      where i.Id == id && u.OpenIdUrl == HttpContext.User.Identity.Name
                      select i;

         if (images.Count() == 1)
         {
            UserImage image = images.First();

            image.Privacy = 0;

            tadmap.SubmitChanges();

            return Json(image.Privacy);
         }
         else
         {
            throw new Exception("Could not mark as public");
         }
      }

      public ActionResult UpdateTitle(Guid id, string title)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (title == null)
            throw new ArgumentNullException("title");

         return Json(TadImage.UpdateTitle(id, title, HttpContext.User) > 0);
      }

      public ActionResult UpdateDescription(Guid id, string description)
      {
         if (id == Guid.Empty)
            throw new ArgumentNullException("id");

         if (description == null)
            throw new ArgumentNullException("description");

         return Json(TadImage.UpdateDescription(id, description, HttpContext.User) > 0);
      }

      public ActionResult Mark(Guid id)
      {
         if (id == Guid.Empty)
            throw new ArgumentException("Cannot be empty(zeros)", "id");

         if (!_principal.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as offensive.");

         try
         {
            TadmapDb db = new TadmapDb();

            UserImage image = db.UserImages.Single(i => i.Id == id);

            if (image == null)
               throw new ImageNotFound();

            image.OffensiveCount = 1;

            db.SubmitChanges();

            return Json(true);
         }
         catch (InvalidOperationException e)
         {
            throw new ImageNotFound();
         }
      }

      public ActionResult UnMark(string id)
      {
         if (!HttpContext.User.IsInRole(TadMapRoles.Administrator))
            throw new SecurityException("Only administrators can mark images as un-offensive.");

         TadmapDb tadmap = new TadmapDb(Database.TadMapConnection);

         UserImage image = tadmap.UserImages.Single(i => i.Id == new Guid(id));

         image.OffensiveCount = 0;

         tadmap.SubmitChanges();

         return Json(true);
      }
   }
}
