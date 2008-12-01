﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using TadMap.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Tadmap_MVC.Views.Shared
{
   public partial class Main : ViewMasterPage
   {
      protected override void OnLoad(EventArgs e)
      {

         //MyMaps.Visible = Context.User.IsInRole(TadMapRoles.Collector);

         
         base.OnLoad(e);
      }

      protected void Page_PreRender(object sender, EventArgs e)
      {
         //m_lblOpenId.Visible = HttpContext.Current.User.Identity.IsAuthenticated;
         //m_lblOpenId.Text = HttpContext.Current.User.Identity.Name;
      }

      protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
      {
         Session.Abandon();
         FormsAuthentication.SignOut();
         Response.Redirect("Default.aspx");
      }
      protected void MyMaps_Click(object sender, EventArgs e)
      {

      }
}
}
