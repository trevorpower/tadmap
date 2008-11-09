using System.Collections.Generic;
using System;
using System.Web.UI.WebControls;
using Affirma.ThreeSharp.Wrapper;
using TadMap;
using System.Web;
using System.Web.Security;

public partial class ViewMap : System.Web.UI.Page
{
   TadImage mImage;

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!HttpContext.Current.User.Identity.IsAuthenticated)
      {
         FormsAuthentication.RedirectToLoginPage();
      }
      else
      {
         ScriptManager1.Scripts.Add(
            new System.Web.UI.ScriptReference(Page.ResolveClientUrl("WebServices/UpdateImage.asmx/js"))
         );

         if (string.IsNullOrEmpty(Request["ImageId"]))
            throw new Exception("This page requires a 'ImageId' as part of the request.");

         Guid imageId = new Guid(Request["ImageId"]);

         mImage = TadImage.Get(imageId, HttpContext.Current.User);

         if (mImage != null)
         {
            m_lblTitle.Text = mImage.Title;
            m_lblDescription.Text = mImage.Description;

            m_imgPicture.ImageUrl = mImage.GetPreviewUrl();

            DownloadOriginal.NavigateUrl = mImage.GetOriginalUrl();

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
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(), "MapId", "var imageId = '" + mImage.Id + "';", true);
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
   
}
