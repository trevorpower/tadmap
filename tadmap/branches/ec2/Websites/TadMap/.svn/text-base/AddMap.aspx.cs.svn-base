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
}
