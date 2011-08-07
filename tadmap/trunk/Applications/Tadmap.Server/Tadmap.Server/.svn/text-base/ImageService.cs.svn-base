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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Exceptioneer.WindowsFormsClient;

namespace Tadmap.Server
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
            Debugger.Break();

            while (true)
            {
               IMessage message = _imageQueue.Next(400);

               if (message != null)
               {
                  var result = ProcessImage.Process(message.Content, _binaryRepository);
                  
                  var serializer = new XmlSerializer(typeof(ImageProcessingResult));
                  var builder = new StringBuilder();
                  var writer = new StringWriter(builder);
                  serializer.Serialize(writer, result);

                  _completeQueue.Add(builder.ToString());
                  _imageQueue.Remove(message);
               }
               else
               {
                  Thread.Sleep(TimeSpan.FromSeconds(20));
               }
            }
         }
         catch (Exception e)
         {
            Client WC = new Client();
            WC.ApiKey = "CDE2C8B3-5770-4E32-BD29-A38624B8C5DB";
            WC.ApplicationName = "Tadmap Server";
            WC.CurrentException = e;
            WC.Submit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
         }
      }

      

   }
}
