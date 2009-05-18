using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Messaging.Test;
using Tadmap.Messaging;
using System.IO;

namespace Tadmap.Local.Test
{
   [TestFixture]
   public class MessageQueueTests : MessageQueueTestsBase
   {
      private const string path = "LocalMessageFolderForTests";

      protected override IMessageQueue CreateQueue()
      {
         return new MessageQueue(path);
      }

      public override void SetUp()
      {
         Directory.CreateDirectory(path);

         base.SetUp();
      }

      [TearDown]
      public void DeleteFolder()
      {
         Directory.Delete(path, true);
      }
   }
}
