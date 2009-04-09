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

      public MessageQueue(string name)
      {
         Name = name;
         _client = new AmazonSQSClient("1RYDPTK2VKP6739SPGR2", "FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7");
      }

      #region IMessageQueue Members

      public void Add(string message)
      {
         SendMessageRequest request = new SendMessageRequest
         {
            MessageBody = message,
            QueueName = Name
         };

         SendMessageResponse response = _client.SendMessage(request);
      }

      public IMessage Next(int timeout)
      {
         var request = new ReceiveMessageRequest()
         {
            MaxNumberOfMessages = 1,
            QueueName = Name,
            VisibilityTimeout = timeout
         };

         var response = _client.ReceiveMessage(request);

         var message = response.ReceiveMessageResult.Message.SingleOrDefault();

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
               QueueName = Name,
               ReceiptHandle = (message as AmazonMessage).Message.ReceiptHandle
            };

            _client.DeleteMessage(deleteRequest);
         }
      }

      #endregion

      private string Name { get; set; }
   }
}
