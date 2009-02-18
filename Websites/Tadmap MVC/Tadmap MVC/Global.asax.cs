using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using Tadmap.Tadmap.Security;
using Microsoft.Practices.Unity;
using Tadmap.Infrastructure;
using Tadmap.Infrastructure.ErrorHandling;
using Tests.Infrastructure.ErrorHandling;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:DontUseKeywordShared", MessageId = "Shared", Target = "Tadmap.Views.Shared")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:DontUseKeywordShared", MessageId = "Shareed", Target = "Tadmap.Views.Shared")]
namespace Tadmap
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801
   public class TadmapApplication : System.Web.HttpApplication
   {
      private static IUnityContainer _container;
      private static IErrorHandler _errorHandler = new EmailErrorHandler();

      public static IUnityContainer Container
      {
         get { return _container; }
      }

      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         routes.MapRoute(
             "image",                                                // Route name
             "Image.mvc/{id}/{action}",                              // URL with parameters
             new { controller = "Image", action = "Index", id = "" } // Parameter defaults
         );

         routes.MapRoute(
             "file",                                                 // Route name
             "File.mvc/{key}",                                       // URL with parameters
             new { controller = "File", action = "Index", key = "" } // Parameter defaults
         );

         routes.MapRoute(
             "Default",                                              // Route name
             "{controller}.mvc/{action}/{id}",                       // URL with parameters
             new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
         );

         routes.MapRoute(
           "Root",
           "",
           new { controller = "Home", action = "Index", id = "" }
         );
      }

      protected void Application_Start()
      {
         InitializeContainer();
         ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
         RegisterRoutes(RouteTable.Routes);
      }

      protected void Application_Error(object sender, EventArgs e)
      {
         _errorHandler.HandleException(Server.GetLastError()); 
      }

      protected void Application_AuthenticateRequest(Object sender, EventArgs e)
      {
         HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

         if (authCookie != null)
         {
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            // the database id for the user is stored along with the list of roles in the user
            // data of the ticket. they are seperated by a '+' character
            CookieUserData userData = CookieUserData.Parse(authTicket.UserData);

            var identity = new TadmapIdentity(authTicket.Name, userData.DisplayName, userData.Id, "Forms");

            Context.User = new GenericPrincipal(identity, userData.Roles);
         }
      }

      private void InitializeContainer()
      {
         if (_container == null)
            _container = new UnityContainer();

         _container.RegisterType<DataAccess.IBinaryRepository, DataAccess.S3.S3BinaryRepository>();
         _container.RegisterType<DataAccess.IImageRepository, DataAccess.SQL.SqlImageRepository>();
      }
   }
}