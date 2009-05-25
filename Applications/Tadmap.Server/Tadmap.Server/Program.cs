using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tadmap.DataAccess;
using System.IO;
using Tadmap.Messaging;
using Tadmap.Model.Image;
using System.ServiceProcess;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using Tadmap.Server.Properties;

namespace Tadmap.Server
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         IUnityContainer container = SetupContainer(new UnityContainer());

         ServiceBase[] ServicesToRun;
         ServicesToRun = new ServiceBase[] 
			{ 
            container.Resolve<ImageService>() 
			};

         ServiceBase.Run(ServicesToRun);
      }

      private static IUnityContainer SetupContainer(IUnityContainer container)
      {
         if (Settings.Default.RunLocal)
         {
            container.RegisterType<IBinaryRepository, Tadmap.Local.BinaryRepository>(
               new InjectionConstructor(
                  Settings.Default.LocalBinaryRepository
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Local.MessageQueue>(
               "images",
               new InjectionConstructor(
                  Settings.Default.LocalImageQueue
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Local.MessageQueue>(
               "complete",
               new InjectionConstructor(
                  Settings.Default.LocalCompleteQueue
               )
            );
         }
         else if (Settings.Default.Live)
         {
            container.RegisterType<IBinaryRepository, Tadmap.Amazon.BinaryRepository>(
               new InjectionConstructor(
                  Settings.Default.S3AccessKey,
                  Settings.Default.S3SecretAccessKey,
                  Settings.Default.S3BucketName
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Amazon.MessageQueue>(
               "images",
               new InjectionConstructor(
                  Settings.Default.LiveImageQueue
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Amazon.MessageQueue>(
               "complete",
               new InjectionConstructor(
                  Settings.Default.LiveCompleteQueue
               )
            );

         }
         else
         {
            container.RegisterType<IBinaryRepository, Tadmap.Amazon.BinaryRepository>(
               new InjectionConstructor(
                  Settings.Default.S3AccessKey,
                  Settings.Default.S3SecretAccessKey,
                  Settings.Default.DevS3BucketName
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Amazon.MessageQueue>(
               "images",
               new InjectionConstructor(
                  Settings.Default.DevImageQueue
               )
            );

            container.RegisterType<IMessageQueue, Tadmap.Amazon.MessageQueue>(
               "complete",
               new InjectionConstructor(
                  Settings.Default.DevCompleteQueue
               )
            );
         }

         return container;
      }
   }
}
