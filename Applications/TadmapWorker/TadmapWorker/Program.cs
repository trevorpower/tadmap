using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading;
using System.Windows.Forms;
using Tadmap.DataAccess;
using Tadmap.DataAccess.S3;
using Tadmap.Models.ImageSets;
using System.IO;

namespace TadmapWorker
{
   static class Program
   {
      static IBinaryRepository _binaryRepository = new S3BinaryRepository();

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         //Application.Run(new MainWindow());


         AmazonSQSClient client = new AmazonSQSClient("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7");

         while (true)
         {
            ReceiveMessageRequest request = new ReceiveMessageRequest()
            {
               MaxNumberOfMessages = 1,
               QueueName = "TadmapDev"
            };

            ReceiveMessageResponse response = client.ReceiveMessage(request);

            Amazon.SQS.Model.Message message = response.ReceiveMessageResult.Message.SingleOrDefault();

            if (message != null)
            {
               ProcessImage(message.Body);

               DeleteMessageRequest deleteRequest = new DeleteMessageRequest(){
                  QueueName = "TadmapDev",
                  ReceiptHandle = message.ReceiptHandle
               };

               client.DeleteMessage(deleteRequest);
            }
            else
            {
               Thread.Sleep(TimeSpan.FromSeconds(25));
            }
         }
      }

      static private void ProcessImage(string imageName)
      {
         IImageSet imageSet = new ImageSet1(imageName);

         Stream binary = _binaryRepository.Get(imageName);

         imageSet.Create(binary, _binaryRepository);
      }
   }
}
