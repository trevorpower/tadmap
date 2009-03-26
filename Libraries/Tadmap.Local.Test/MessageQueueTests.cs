using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Messaging.Test;
using Tadmap.Messaging;

namespace Tadmap.Local.Test
{
   [TestFixture]
   public class MessageQueueTests : MessageQueueTestsBase
   {
      protected override IMessageQueue CreateQueue()
      {
         return null;
      }
   }
}
