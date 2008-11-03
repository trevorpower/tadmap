using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using Affirma.ThreeSharp.Wrapper;
using System.IO;
using System.Drawing;
using ImageManipulation;
using TadMap;
using System.Web.Security;
using TadMap.Security;

public partial class AddMap : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!HttpContext.Current.User.IsInRole(TadMapRoles.Collector))
      {
         FormsAuthentication.RedirectToLoginPage();
      }
   }

   protected void m_btnAdd_Click(object sender, EventArgs e)
   {

      //int i = 0;
      //foreach (IElementTraits oTraits in Map.ValidElements)
      //{
      //   if (oTraits.DataType == DataType.ShortString || oTraits.DataType == DataType.LongString)
      //   {
      //      TextBox oValueText = m_repInputs.Items[i].FindControl("TextBox" + oTraits.Type) as TextBox;

      //      if (!string.IsNullOrEmpty(oValueText.Text))
      //      {
      //         MapElement oMapDescriptionElement = MapElement.NewMapElement();
      //         oMapDescriptionElement.ElementType = oTraits.Type;
      //         oMapDescriptionElement.Value = oValueText.Text;

      //         oNewMap.Elements.Add(oMapDescriptionElement);
      //      }
      //   }
      //   else if (oTraits.DataType == DataType.Image)
      //   {
      //      HtmlInputFile oFileInput = m_repInputs.Items[i].FindControl("FileInput" + oTraits.Type) as HtmlInputFile;
      //      HttpPostedFile oFile = oFileInput.PostedFile;

      //      if (oFile.ContentLength > 0)
      //      {
      //         Picture oPicture = Picture.NewPicture(Tad.ApplicationContext.User.Identity.Name, oFile.InputStream, Guid.NewGuid() + Path.GetExtension(oFile.FileName));
      //         oPicture.Description = "Test Description";
      //         oNewMap.Pictures.Add(oPicture);
      //      }
      //   }

      //   i++;
      //}

      //oNewMap.Save();

      
      HttpPostedFile oFile = FileInput.PostedFile;

      if (oFile.ContentLength > 0)
      {
         TadImage oNewImage = TadImage.NewTadImage(oFile.InputStream, Guid.NewGuid() + Path.GetExtension(oFile.FileName) );
         oNewImage.Title = m_txtTitle.Text;
         oNewImage.Description = m_txtDescription.Text;
         oNewImage.Save(HttpContext.Current.User.Identity);
      }

      Response.Redirect("Default.aspx", true);
   }

   void m_repInputs_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
      //IElementTraits oElementTraits = e.Item.DataItem as IElementTraits;

      //Label oNameLabel = e.Item.FindControl("m_lblName") as Label;
      //oNameLabel.Text = oElementTraits.Name + ":";

      //PlaceHolder oValuePlaceHolder = e.Item.FindControl("m_phPlaceHolder") as PlaceHolder;

      //switch (oElementTraits.DataType)
      //{
      //   case DataType.Image:
      //      {
      //         HtmlInputFile oFileInputControl = new HtmlInputFile();
      //         oFileInputControl.ID = "FileInput" + oElementTraits.Type;
      //         oFileInputControl.Attributes.Add("class", "ItemDetailControl");
      //         oFileInputControl.Attributes.Add("length", "75");
      //         oValuePlaceHolder.Controls.Add(oFileInputControl);
      //         break;
      //      }
      //   case DataType.ShortString:
      //      {
      //         TextBox oValueText = new TextBox();
      //         oValueText.CssClass = "ItemDetailControl";
      //         oValueText.ID = "TextBox" + oElementTraits.Type;
      //         oValuePlaceHolder.Controls.Add(oValueText);
      //         break;
      //      }
      //   case DataType.LongString:
      //      {
      //         TextBox oValueText = new TextBox();
      //         oValueText.CssClass = "ItemDetailControl";
      //         oValueText.TextMode = TextBoxMode.MultiLine;
      //         oValueText.Height = 75;
      //         oValueText.ID = "TextBox" + oElementTraits.Type;
      //         oValuePlaceHolder.Controls.Add(oValueText);
      //         break;
      //      }
      //}
   }
}
