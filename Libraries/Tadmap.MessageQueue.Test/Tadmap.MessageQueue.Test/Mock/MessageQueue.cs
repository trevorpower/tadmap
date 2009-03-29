using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Messaging.Test.Mock
{
   class MessageQueue : IMessageQueue
   {
      #region IMessageQueue Members

      private class Message : IMessage
      {
         public string Content { get; set; }
         public DateTime Expires { get; set; }
      }

      private List<Message> _messages = new List<Message>();

      public IMessage Next(int timeout)
      {
         var available = from m in _messages
                         where m.Expires <= DateTime.Now
                         orderby m.Expires descending
                         select m;

         if (available.Count() == 0)
            return null;

         var message = available.First();
          
         message.Expires = message.Expires.AddMilliseconds(timeout);

         return message;
      }

      public void Add(string message)
      {
         _messages.Add(new Message { Content = message, Expires = DateTime.Now });
      }

      public void Remove(IMessage message)
      {
         if (message is Message)
            _messages.Remove(message as Message);
         else
            throw new NotSupportedException("Cannot remove message of type '" + message.GetType() + "'");
      }

      #endregion
   }
}
