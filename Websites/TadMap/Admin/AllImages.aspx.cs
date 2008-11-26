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
                m_repMapRepeater.DataSource = TadImageList.GetAllImages(HttpContext.Current.User.Identity);
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
        TadImage oImageInfo = e.Item.DataItem as TadImage;

        LinkButton oLink = e.Item.FindControl("m_lbName") as LinkButton;
        oLink.Text = oImageInfo.Title;
        oLink.PostBackUrl = "ViewMap.aspx?ImageId=" + oImageInfo.Id;

        Label oLabel = e.Item.FindControl("m_lblDescription") as Label;
        oLabel.Text = oImageInfo.Description;

        Image oImage = e.Item.FindControl("m_imgImage") as Image;
        oImage.Width = 80;
        oImage.Height = 80;

        PostBackOptions options = new PostBackOptions(oLink, "", "../ViewMap.aspx?ImageId=" + oImageInfo.Id, true, false, false, true, false, "");

        HtmlControl oDiv = e.Item.FindControl("ListItem") as HtmlControl;
        oDiv.Attributes.Add("onClick", ClientScript.GetPostBackEventReference(options));
        oDiv.Attributes.Add("onMouseOver", "this.style.background = '#FFFFCC';");
        oDiv.Attributes.Add("onMouseOut", "this.style.background = '#FFFFFF';");

        ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
        oImage.ImageUrl = s3.GetUrl(S3Storage.BucketName, "Square_" + oImageInfo.StorageKey);
        //oImage.ImageUrl = "http://" + S3Storage.BucketName + ".s3.amazonaws.com/Square_" + oImageInfo.StorageKey;
    }
}
