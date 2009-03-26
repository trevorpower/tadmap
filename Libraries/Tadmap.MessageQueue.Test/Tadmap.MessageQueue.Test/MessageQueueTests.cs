using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Messaging.Test.Mock;

namespace Tadmap.Messaging.Test
{
   [TestFixture]
   public class MessageQueueTests : MessageQueueTestsBase
   {
      protected override IMessageQueue CreateQueue()
      {
         return new MessageQueue();
      }
   }
}
