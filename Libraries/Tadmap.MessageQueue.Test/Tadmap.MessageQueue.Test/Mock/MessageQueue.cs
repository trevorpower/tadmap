using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Messaging.Test.Mock
{
   class MessageQueue : IMessageQueue
   {
      #region IMessageQueue Members

      private List<string> _messages = new List<string>();
      private List<string> _pending = new List<string>();

      public string Next()
      {
         string message = null;

         if (_messages.Count > 0)
         {
            message = _messages[0];
            _pending.Add(message);
            _messages.RemoveAt(0);
         }

         return message;
      }

      public void Add(string message)
      {
         _messages.Add(message);
      }

      public void ReviveMessages()
      {
         _messages.AddRange(_pending);
         _pending.Clear();
      }

      #endregion
   }
}
