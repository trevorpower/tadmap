using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Linq;
using System.Security;
using TadMap;
using TadMap.Configuration;
using TadMap.Security;

/// <summary>
/// Summary description for UpdateTitle
/// </summary>
[WebService(Namespace = "http://tadmap.com/WebServices")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class UpdateImage : System.Web.Services.WebService
{
   public UpdateImage()
   {
      //Uncomment the following line if using designed components 
      //InitializeComponent(); 
   }

   
}

