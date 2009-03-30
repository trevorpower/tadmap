using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Messaging;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Tadmap.Amazon
{
   public class MessageQueue : IMessageQueue
   {
      private class AmazonMessage : IMessage
      {
         public string Content { get; set; }
         public Message Message { get; set; }
      }

      AmazonSQSClient _client;

      public MessageQueue()
      {
         _client = new AmazonSQSClient("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7");
      }
      #region IMessageQueue Members

      public void Add(string message)
      {
         SendMessageRequest request = new SendMessageRequest
         {
            MessageBody = message,
            QueueName = "TadmapDev"
         };

         SendMessageResponse response = _client.SendMessage(request);
      }

      public IMessage Next(int timeout)
      {
         ReceiveMessageRequest request = new ReceiveMessageRequest()
         {
            MaxNumberOfMessages = 1,
            QueueName = "TadmapDev"
         };

         ReceiveMessageResponse response = _client.ReceiveMessage(request);

         Message message = response.ReceiveMessageResult.Message.SingleOrDefault();

         if (message == null)
            return null;

         return new AmazonMessage { Content = message.Body, Message = message };
      }

      public void Remove(IMessage message)
      {
         if (message is AmazonMessage)
         {
            DeleteMessageRequest deleteRequest = new DeleteMessageRequest()
            {
               QueueName = "TadmapDev",
               ReceiptHandle = (message as AmazonMessage).Message.ReceiptHandle
            };

            _client.DeleteMessage(deleteRequest);
         }
      }

      #endregion
   }
}
