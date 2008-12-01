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
using System.Web.Mvc;
using System.Collections.Generic;

public partial class _Default : ViewPage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      //divMapList.Visible = true;

      //LoginText.Visible = !HttpContext.Current.User.Identity.IsAuthenticated;
   }

   public static List<UserImage> ImageList
   {
      get
      {
         Tadmap db = new Tadmap(Database.TadMapConnection);

         var images = from i in db.UserImages
                      where i.OffensiveCount == 0 && i.Privacy > 0
                      select i;

         return images.ToList();
      }
   }

   void m_repMapRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
      //PostBackOptions options = new PostBackOptions(oLink, "", "ViewMap.aspx?ImageId=" + imageInfo.Id, true, false, false, true, false, "");

      //HtmlControl oDiv = e.Item.FindControl("ListItem") as HtmlControl;
      //oDiv.Attributes.Add("onClick", ClientScript.GetPostBackEventReference(options));
      //oDiv.Attributes.Add("onMouseOver", "this.style.background = '#FFFFCC';");
      //oDiv.Attributes.Add("onMouseOut", "this.style.background = '#FFFFFF';");
   }
}
