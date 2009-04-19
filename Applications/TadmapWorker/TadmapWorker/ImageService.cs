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
      static IBinaryRepository _binaryRepository;// = new Tadmap.Amazon.BinaryRepository("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7", "tadtestus");
      static IMessageQueue _imageQueue;// = new Tadmap.Amazon.MessageQueue("debug-tadmap-image");
      static IMessageQueue _completeQueue;// = new Tadmap.Amazon.MessageQueue("debug-tadmap-complete");

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

      static private void ProcessImage(string imageName)
      {
         Stream binary = _binaryRepository.GetBinary(imageName);

         if (binary == null)
            return; // Image not available in the queue yet.

         // If I have an image I should renew the message.

         IImageSet imageSet = new ImageSet1(imageName);

         imageSet.Create(binary, _binaryRepository);
      }

   }
}
