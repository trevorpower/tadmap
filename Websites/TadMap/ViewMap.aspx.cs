using System.Collections.Generic;
using System;
using System.Web.UI.WebControls;
using Affirma.ThreeSharp.Wrapper;
using TadMap;
using System.Web;
using System.Web.Security;
using TadMap.Security;
using System.Web.UI;
using TadMap.Configuration;
using System.Linq;
using System.Security;
using System.Web.Services;

public partial class ViewMap : System.Web.UI.Page
{
   TadImage mImage;

   protected void Page_Load(object sender, EventArgs e)
   {
      ScriptManager1.Scripts.Add(
         new System.Web.UI.ScriptReference(Page.ResolveClientUrl("WebServices/UpdateImage.asmx/js"))
      );

      if (string.IsNullOrEmpty(Request["ImageId"]))
         throw new Exception("This page requires a 'ImageId' as part of the request.");

      Guid imageId = new Guid(Request["ImageId"]);

      Tadmap db = new Tadmap(Database.TadMapConnection);

      Tadmap tadmap = new Tadmap(Database.TadMapConnection);

      UserImage image = tadmap.UserImages.Single(i => i.Id == imageId);

      if (image != null)
      {
         ScriptManager.RegisterClientScriptBlock(Page, GetType(), "MapId", "var imageId = '" + image.Id + "';", true);
         OwnerControls.Visible = false;

         if (image.OffensiveCount > 0)
         {
            if (!HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
               throw new SecurityException("Only administrator can view images marked as offensive");
         }
         else if (HttpContext.Current.User.Identity.IsAuthenticated)
         {
            UserOpenId openId = tadmap.UserOpenIds.Single(i => i.OpenIdUrl == HttpContext.Current.User.Identity.Name);

            if (image.Privacy == 0 && image.UserId != openId.UserId)
               throw new SecurityException("Cannot view another users image if it is marked as private.");

            if (image.UserId == openId.UserId)
            {
               OwnerControls.Visible = true;
               ScriptManager.RegisterClientScriptInclude(Page, GetType(), "EditDetails", Page.ResolveClientUrl("JavaScript/ViewMap.js"));
               privacyCheckBox.Checked = image.Privacy > 0;
               PrivacyStatus.Text = image.Privacy == 0 ? "<b>Only you</b> can view this image." : "<b>Anyone</b> can view this image.";
            }
         }
         else
         {
            if (image.Privacy == 0)
               throw new Exception("Guest cannot view private image");
         }

         Title = "Tadmap - " + image.Title;
         m_lblTitle.Text = image.Title;
         m_lblDescription.Text = image.Description;

         m_imgPicture.ImageUrl = TadImage.GetPreviewUrl(image);

         DownloadOriginal.NavigateUrl = TadImage.GetOriginalUrl(image);

         // tilesets are not implemented for version the beta version one so we hide this button for now
         m_lbViewTileSet.Visible = false;
         m_lbCreateTileSet.Visible = false;
         //if (mImage.HasTileSet)
         //{
         //   m_lbViewTileSet.Visible = true;
         //   m_lbViewTileSet.PostBackUrl = "Dev.aspx?MapId=" + mImage.Id;
         //}
         //else
         //{
         //   m_lbViewTileSet.Visible = false;
         //}

         if (HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
         {
            Offensive.Visible = true;
            UnOffensive.Visible = true;
            Offensive.OnClientClick = "UpdateImage.Mark(imageId); return false;";
            UnOffensive.OnClientClick = "UpdateImage.UnMark(imageId); return false;";
         }
         else
         {
            Offensive.Visible = false;
            UnOffensive.Visible = false;
         }
      }
   }

   void m_repMapRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
      //MapElement oElement = e.Item.DataItem as MapElement;
      //IElementTraits oTraits = ElementTraitsFactory.Get(oElement.ElementType);

      //Label oNameLabel = e.Item.FindControl("m_lblTitle") as Label;
      //oNameLabel.Text = oTraits.Name + ":";

      //PlaceHolder oPlaceHolder = e.Item.FindControl("m_phPlaceHolder") as PlaceHolder;

      //if (oTraits.DataType == DataType.Image)
      //{
      //   Image oImage = new Image();
      //   oImage.BorderWidth = Unit.Pixel(2);
      //   oImage.CssClass = "ItemDetailImage";

      //   ThreeSharpWrapper oWrapper = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
      //   oImage.ImageUrl = "http://" + S3Storage.BucketName + ".s3.amazonaws.com/" + oElement.Value;

      //   oPlaceHolder.Controls.Add(oImage);
      //}
      //else
      //{
      //   Label oTitleLabel = e.Item.FindControl("m_lblName") as Label;
      //   oTitleLabel.Text = oElement.Value;
      //}
   }


   protected void m_lbCreateTileSet_Click(object sender, EventArgs e)
   {
      throw new NotImplementedException();

      if (mImage != null)
      {
         mImage.CreateTileset();
         mImage.Save(HttpContext.Current.User.Identity);
      }
   }

   [WebMethod]
   public static int MakePublic(string id)
   {
      Tadmap tadmap = new Tadmap(Database.TadMapConnection);

      var images = from i in tadmap.UserImages
                   join u in tadmap.UserOpenIds on i.UserId equals u.UserId
                   where i.Id == new Guid(id) && u.OpenIdUrl == HttpContext.Current.User.Identity.Name
                   select i;

      if (images.Count() == 1)
      {
         UserImage image = images.First();

         image.Privacy = 1;

         tadmap.SubmitChanges();

         return image.Privacy;
      }
      else
      {
         throw new Exception("Could not marke as public");
      }
   }

   [WebMethod]
   public static int MakePrivate(string id)
   {
      Tadmap tadmap = new Tadmap(Database.TadMapConnection);

      var images = from i in tadmap.UserImages
                   join u in tadmap.UserOpenIds on i.UserId equals u.UserId
                   where i.Id == new Guid(id) && u.OpenIdUrl == HttpContext.Current.User.Identity.Name
                   select i;

      if (images.Count() == 1)
      {
         UserImage image = images.First();

         image.Privacy = 0;

         tadmap.SubmitChanges();

         return image.Privacy;
      }
      else
      {
         throw new Exception("Could not marke as public");
      }
   }
}
