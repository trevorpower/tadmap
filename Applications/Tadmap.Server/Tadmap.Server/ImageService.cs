using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Tadmap.Messaging;
using Tadmap.Model.Image;
using System.IO;
using Microsoft.Practices.Unity;

namespace TadmapWorker
{
   public partial class ImageService : ServiceBase
   {
      static IBinaryRepository _binaryRepository;
      static IMessageQueue _imageQueue;
      static IMessageQueue _completeQueue;

      static Thread _thread = new Thread(new ThreadStart(Loop));

      public ImageService(
         IBinaryRepository binaryRepository,
         [Dependency("images")] IMessageQueue imageQueue,
         [Dependency("complete")] IMessageQueue completeQueue
      )
      {
         _binaryRepository = binaryRepository;
         _imageQueue = imageQueue;
         _completeQueue = completeQueue;

         InitializeComponent();
      }

      protected override void OnStart(string[] args)
      {
         _thread.Start();
      }

      protected override void OnStop()
      {
      }

      private static void Loop()
      {
         try
         {
            while (true)
            {
               IMessage message = _imageQueue.Next(200);

               if (message != null)
               {
                  ProcessImage(message.Content);

                  _completeQueue.Add(message.Content);
                  _imageQueue.Remove(message);
               }
               else
               {
                  Thread.Sleep(TimeSpan.FromSeconds(15));
               }
            }
         }
         catch (Exception e)
         {
            if (!EventLog.SourceExists("Tadmap"))
               EventLog.CreateEventSource("Tadmap", "Application");

            var log = new EventLog("Application")
            {
               Source = "Tadmap"
            };
            var builder = new StringBuilder();

            var currentException = e;
            string prefix = "";

            while (e != null)
            {
               builder.AppendLine(prefix + e.Message);
               e = e.InnerException;
               prefix += ">";
            }

            log.WriteEntry(builder.ToString(), EventLogEntryType.Error);

            throw;
         }
      }

      static private void ProcessImage(string imageName)
      {
         var log = new EventLog("Application")
         {
            Source = "Tadmap"
         };

         log.WriteEntry("Processing image:" + imageName, EventLogEntryType.Information);

         Stream binary = _binaryRepository.GetBinary(imageName);

         if (binary == null)
         {
            log.WriteEntry("No binary found:" + imageName, EventLogEntryType.Warning);
            return; // Image not available in the queue yet.
         }

         // If I have an image I should renew the message.

         IImageSet imageSet = new ImageSet1(imageName);

         imageSet.Create(binary, _binaryRepository);
         log.WriteEntry("Processing finished.", EventLogEntryType.Information);
      }

   }
}
