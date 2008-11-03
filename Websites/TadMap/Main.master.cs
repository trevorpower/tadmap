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
using TadMap.Security;

public partial class Main : System.Web.UI.MasterPage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      btnMyMaps.Visible = false;
   }

   protected void Page_PreRender(object sender, EventArgs e)
   {
      m_lblOpenId.Visible = HttpContext.Current.User.Identity.IsAuthenticated;
      m_lblOpenId.Text = HttpContext.Current.User.Identity.Name;
   }

   protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
   {
      Session.Abandon();
      FormsAuthentication.SignOut();
      Response.Redirect("Default.aspx");
   }
}
