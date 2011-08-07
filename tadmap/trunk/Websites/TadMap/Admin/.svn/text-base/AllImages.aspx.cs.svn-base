using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TadMap;
using Affirma.ThreeSharp.Wrapper;
using System.Web.UI.HtmlControls;
using TadMap.Configuration;
using System.Security;
using TadMap.Security;

public partial class Admin_AllImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
            {
                m_repMapRepeater.ItemDataBound += new RepeaterItemEventHandler(m_repMapRepeater_ItemDataBound);

                Tadmap db = new Tadmap(Database.TadMapConnection);

                var images = from i in db.UserImages
                             select i;

                m_repMapRepeater.DataSource = images;
                m_repMapRepeater.DataBind();
                divMapList.Visible = true;
            }
            else
            {
                throw new SecurityException("Must be administrator to access AllImages page");
            }
        }
        else
        {
            throw new SecurityException("Must authenticated to access AllImages page");
        }
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

       PostBackOptions options = new PostBackOptions(oLink, "", "../ViewMap.aspx?ImageId=" + imageInfo.Id, true, false, false, true, false, "");

       HtmlControl oDiv = e.Item.FindControl("ListItem") as HtmlControl;
       oDiv.Attributes.Add("onClick", ClientScript.GetPostBackEventReference(options));
       oDiv.Attributes.Add("onMouseOver", "this.style.background = '#FFFFCC';");
       oDiv.Attributes.Add("onMouseOut", "this.style.background = '#FFFFFF';");

       ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
       oImage.ImageUrl = s3.GetUrl(S3Storage.BucketName, "Square_" + imageInfo.Key);
       //oImage.ImageUrl = "http://" + S3Storage.BucketName + ".s3.amazonaws.com/Square_" + oImageInfo.StorageKey;
    }
}
