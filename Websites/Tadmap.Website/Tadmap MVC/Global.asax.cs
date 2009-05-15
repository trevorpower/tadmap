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
using Tadmap.Model.Image;
using System.Configuration;

namespace Tadmap
{
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
            CookieUserData userData = CookieUserData.Parse(authTicket.UserData);
            var identity = new TadmapIdentity(authTicket.Name, userData.DisplayName, userData.Id, "Forms");

            Context.User = new GenericPrincipal(identity, userData.Roles);
         }
      }

      private static void InitializeContainer()
      {
         if (_container == null)
            _container = new UnityContainer();

         if (bool.Parse(ConfigurationManager.AppSettings["RunLocal"]))
         {
            _container.RegisterType<IBinaryRepository, Local.BinaryRepository>(
               new InjectionConstructor("../TempFolder")
            );
         }
         else
         {
            _container.RegisterType<IBinaryRepository, Amazon.BinaryRepository>(
               new InjectionConstructor(
                  ConfigurationManager.AppSettings["S3AccessKey"],
                  ConfigurationManager.AppSettings["S3SecretAccessKey"],
                  ConfigurationManager.AppSettings["S3BucketName"]
               )
            );
         }

         _container.RegisterType<IImageRepository, Sql.SqlImageRepository>();
      }
   }
}