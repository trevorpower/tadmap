using System;
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
using Tadmap.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716", Target="Tadmap.Views.Shared", Scope="Namespace", MessageId="Shared", Justification = "Shared namespace is MVC convention.")]
namespace Tadmap.Views.Shared
{
   public partial class Main : ViewMasterPage
   {
      protected override void OnLoad(EventArgs e)
      {

         //MyMaps.Visible = Context.User.IsInRole(TadmapRoles.Collector);

         
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
