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
using Tadmap.Messaging;
using System.IO;
using System.Threading;

namespace Tadmap.Website
{
   public class TadmapApplication : System.Web.HttpApplication
   {
      private static IUnityContainer _container;
      private static IErrorHandler _errorHandler = new EmailErrorHandler();

      private static IMessageQueue _completeQueue;

      private static Thread _pollingThread;

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

         _pollingThread = new Thread(CompletePolling);

         var repositories = new Repositories
         {
            BinaryRepository = _container.Resolve<IBinaryRepository>(),
            ImageRepository = _container.Resolve<IImageRepository>()
         };

         _pollingThread.Start(repositories);
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
               new InjectionConstructor("F:/TadmapLocalData/LocalBinaryFolder")
            );

            _completeQueue = new Local.MessageQueue("F:/TadmapLocalData/LocalCompleteMessageFolder");
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

         _container.RegisterType<IMessageQueue, Local.MessageQueue>(
            new InjectionConstructor("F:/TadmapLocalData/LocalImageMessageFolder")
         );

         _container.RegisterType<IImageRepository, Sql.SqlImageRepository>();
      }

      /// <summary>
      /// Poll the completed message queue for image tasks that have been completed and
      /// update the images image set number.
      /// </summary>
      private static void CompletePolling(object param)
      {
         Repositories repositories = (Repositories)param;

         while (true)
         {
            IMessage message = _completeQueue.Next(50000);

            if (message != null)
            {
               var image = repositories.ImageRepository.GetAllImages(repositories.BinaryRepository).Single(i => i.Key == message.Content);

               image.ImageSetVersion = 1;

               repositories.ImageRepository.Save(image);

               _completeQueue.Remove(message);
            }
            else
            {
               Thread.Sleep(TimeSpan.FromSeconds(45));
            }
         }
      }

      class Repositories
      {
         public IImageRepository ImageRepository { get; set; }
         public IBinaryRepository BinaryRepository { get; set; }
      }
   }
}