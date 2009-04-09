using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Messaging.Test;
using Tadmap.Messaging;
using NUnit.Framework;

namespace Tadmap.Amazon.Test
{
   [TestFixture]
   public class MessageQueueTests : MessageQueueTestsBase
   {
      protected override IMessageQueue CreateQueue()
      {
         return new MessageQueue("TadmapDev");
      }
   }
}
