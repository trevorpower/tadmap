using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tadmap.DataAccess;
using System.IO;
using Tadmap.Messaging;
using Tadmap.Model.Image;

namespace TadmapWorker
{
   static class Program
   {
      //static IBinaryRepository _binaryRepository = new Tadmap.Local.BinaryRepository("F:/TadmapLocalData/LocalBinaryFolder");
      //static IMessageQueue _imageQueue = new Tadmap.Local.MessageQueue("F:/TadmapLocalData/LocalImageMessageFolder");
      //static IMessageQueue _completeQueue = new Tadmap.Local.MessageQueue("F:/TadmapLocalData/LocalCompleteMessageFolder");

      static IBinaryRepository _binaryRepository = new Tadmap.Amazon.BinaryRepository("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7", "tadmap-dev");
      static IMessageQueue _imageQueue = new Tadmap.Amazon.MessageQueue("debug-tadmap-image");
      static IMessageQueue _completeQueue = new Tadmap.Amazon.MessageQueue("debug-tadmap-complete");

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         //Application.Run(new MainWindow());


         while (true)
         {
            IMessage message = _imageQueue.Next(600);

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
         ImageIndexConverter might NotFiniteNumberException be available form S3 yet

         IImageSet imageSet = new ImageSet1(imageName);

         Stream binary = _binaryRepository.GetBinary(imageName);

         imageSet.Create(binary, _binaryRepository);
      }
   }
}
