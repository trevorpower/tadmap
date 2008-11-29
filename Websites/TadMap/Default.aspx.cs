using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TadMap;
using Affirma.ThreeSharp.Wrapper;
using TadMap.Configuration;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
   {
      m_repMapRepeater.ItemDataBound += new RepeaterItemEventHandler(m_repMapRepeater_ItemDataBound);

      Tadmap db = new Tadmap(Database.TadMapConnection);

      var images = from i in db.UserImages
                   where i.OffensiveCount == 0 && i.Privacy > 0
                   select i;

      m_repMapRepeater.DataSource = images;
      m_repMapRepeater.DataBind();
      divMapList.Visible = true;

      LoginText.Visible = !HttpContext.Current.User.Identity.IsAuthenticated;
   }

   void m_repMapRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
      UserImage imageInfo = e.Item.DataItem as UserImage;

      LinkButton oLink = e.Item.FindControl("m_lbName") as LinkButton;
      oLink.Text = imageInfo.Title;
      oLink.PostBackUrl = "ViewMap.aspx?ImageId=" + imageInfo.Id;

      Label oLabel = e.Item.FindControl("m_lblDescription") as Label;
      oLabel.Text = imageInfo.Description;

      Image oImage = e.Item.FindControl("m_imgImage") as Image;
      oImage.Width = 80;
      oImage.Height = 80;

      PostBackOptions options = new PostBackOptions(oLink, "", "ViewMap.aspx?ImageId=" + imageInfo.Id, true, false, false, true, false, "");

      HtmlControl oDiv = e.Item.FindControl("ListItem") as HtmlControl;
      oDiv.Attributes.Add("onClick", ClientScript.GetPostBackEventReference(options));
      oDiv.Attributes.Add("onMouseOver", "this.style.background = '#FFFFCC';");
      oDiv.Attributes.Add("onMouseOut", "this.style.background = '#FFFFFF';");

      ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
      oImage.ImageUrl = s3.GetUrl(S3Storage.BucketName, "Square_" + imageInfo.Key);
      //oImage.ImageUrl = "http://" + S3Storage.BucketName + ".s3.amazonaws.com/Square_" + oImageInfo.StorageKey;
   }
}
