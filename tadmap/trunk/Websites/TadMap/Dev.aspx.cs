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

public partial class Dev : TadPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (string.IsNullOrEmpty(Request["MapId"]))
          throw new Exception("This page requires a 'MapId' as part of the request.");

       Guid guidMapId = new Guid(Request["MapId"]);

       //Map oMap = Map.GetMap(guidMapId);

       //if (oMap != null)
       //{
       //   if (oMap.Pictures.Count > 0)
       //   {
       //      if (oMap.Pictures[0].HasTileSet)
       //      {
       //         string strSetupMapScript = string.Format(
       //            "setupMap('tmap', '{0}', {1}, {2} );",
       //            oMap.Pictures[0].Key,
       //            oMap.Pictures[0].ZoomLevels,
       //            oMap.Pictures[0].TileSize
       //         );


       //         OnClientLoad.Add(strSetupMapScript);
       //         OnClientUnload.Add("GUnload();");


       //         Page.ClientScript.RegisterClientScriptInclude(GetType(), "MapExplorer", Request.ApplicationPath + "/JavaScript/MapExplorer.js");
       //      }
       //   }
       //   else
       //   {
       //   }
       //}

       OnClientLoad.Add("loadGmap();");

       Page.ClientScript.RegisterClientScriptInclude(GetType(), "MapExplorer", Request.ApplicationPath + "/JavaScript/SyncPoints.js");
    }
}
