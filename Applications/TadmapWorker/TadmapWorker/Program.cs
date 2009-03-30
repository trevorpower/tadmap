﻿using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SQS;
using Amazon.SQS.Model;
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
      static IBinaryRepository _binaryRepository = new Tadmap.Local.BinaryRepository("F:/TadmapLocalData/LocalBinaryFolder");
      static IMessageQueue _messageQueue = new Tadmap.Local.MessageQueue("F:/TadmapLocalData/LocalMessageFolder");

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
            IMessage message = _messageQueue.Next(50000);

            if (message != null)
            {
               ProcessImage(message.Content);

               _messageQueue.Remove(message);
            }
            else
            {
               Thread.Sleep(TimeSpan.FromSeconds(5));
            }
         }
      }

      static private void ProcessImage(string imageName)
      {
         IImageSet imageSet = new ImageSet1(imageName);

         Stream binary = _binaryRepository.GetBinary(imageName);

         imageSet.Create(binary, _binaryRepository);
      }
   }
}
