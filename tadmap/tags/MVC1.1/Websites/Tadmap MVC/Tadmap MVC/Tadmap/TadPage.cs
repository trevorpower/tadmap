using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

/// <summary>
/// Summary description for TadPage
/// </summary>
public class TadPage : Page
{
	public TadPage()
	{
	}

   public List<string> OnClientLoad = new List<string>();
   public List<string> OnClientUnload = new List<string>();

   protected override void OnPreRender(EventArgs e)
   {
      HtmlControl oMainBody = (HtmlControl)Master.FindControl("MainBody");

      if (oMainBody != null)
      {
         bool bCustomEventsUsed = false;

         if (OnClientLoad.Count > 0)
         {
            RenderCustomEvents(oMainBody, OnClientLoad, "onload", "Load");
            bCustomEventsUsed = true;
         }

         if (OnClientUnload.Count > 0)
         {
            RenderCustomEvents(oMainBody, OnClientUnload, "onunload", "Unload");
            bCustomEventsUsed = true;
         }

         if (bCustomEventsUsed)
            Page.ClientScript.RegisterClientScriptInclude(GetType(), "CustomEvents", Request.ApplicationPath + "/JavaScript/CustomEvents.js");
      }


      base.OnPreRender(e);
   }

   void RenderCustomEvents(HtmlControl oControl, List<string> oCustomEvents, string strClientEvent, string strCustomName)
   {
      string strArrayName = "arr" + strCustomName + "Events";
      string strArrayValues = "";

      foreach (string oValue in oCustomEvents)
      {
         if (oCustomEvents.IndexOf(oValue) != 0)
            strArrayValues += ",";

         strArrayValues += "'";
         strArrayValues += oValue.Replace("'", "\\'");
         strArrayValues += "'";
      }

      Page.ClientScript.RegisterArrayDeclaration(strArrayName, strArrayValues);

      oControl.Attributes.Add(strClientEvent, "fireEvents(" + strArrayName + ")");
   }
}
