using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using TadMap;
using System.Linq;
using TadMap.Configuration;
using TadMap.Security;
using System.Security;

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

   [WebMethod]
   public bool UpdateTitle(string id, string title)
   {
      if (id == null)
         throw new ArgumentNullException("id");

      if (title == null)
         throw new ArgumentNullException("title");

      return TadImage.UpdateTitle(new Guid(id), title, HttpContext.Current.User) > 0;
   }

   [WebMethod]
   public bool UpdateDescription(string id, string description)
   {
      if (id == null)
         throw new ArgumentNullException("id");

      if (description == null)
         throw new ArgumentNullException("description");

      return TadImage.UpdateDescription(new Guid(id), description, HttpContext.Current.User) > 0;
   }

   [WebMethod]
   public bool Mark(string id)
   {
       if (!HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
           throw new SecurityException("Only administrators can mark images as offensive.");

       Tadmap tadmap = new Tadmap(Database.TadMapConnection);

       UserImage image = tadmap.UserImages.Single(i => i.Id == new Guid(id));

       image.OffensiveCount = 1;

       tadmap.SubmitChanges();

       return true;
   }

   [WebMethod]
   public bool UnMark(string id)
   {
       if (!HttpContext.Current.User.IsInRole(TadMapRoles.Administrator))
           throw new SecurityException("Only administrators can mark images as un-offensive.");

       Tadmap tadmap = new Tadmap(Database.TadMapConnection);

       UserImage image = tadmap.UserImages.Single(i => i.Id == new Guid(id));

       image.OffensiveCount = 0;

       tadmap.SubmitChanges();

       return true;
   }
}

