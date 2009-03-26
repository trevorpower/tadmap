using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Messaging
{
   public interface IMessageQueue
   {
      void Add(string message);

      string Next();

      void ReviveMessages();
   }
}
