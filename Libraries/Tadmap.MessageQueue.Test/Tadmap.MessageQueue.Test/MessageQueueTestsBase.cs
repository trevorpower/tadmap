using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Messaging.Test.Mock;

namespace Tadmap.Messaging.Test
{
   public abstract class MessageQueueTestsBase
   {
      IMessageQueue _queue;

      protected abstract IMessageQueue CreateQueue();

      [SetUp]
      public void SetUp()
      {
         _queue = CreateQueue();
      }

      [Test]
      public void CanAddMessage()
      {
         string message = "My Message";
         _queue.Add(message);
         Assert.AreEqual(message, _queue.Next());
      }

      [Test]
      public void CanAddMultipleMessages()
      {
         List<string> messages = new List<string>();
         messages.Add("one");
         messages.Add("two");
         messages.Add("three");

         messages.ForEach(m => _queue.Add(m));

         string message = _queue.Next();

         while (message != null)  
         {
            Assert.That(messages.Contains(message));
            messages.Remove(message);
            message = _queue.Next();
         }

         Assert.IsEmpty(messages);
      }

      [Test]
      public void MessagesReappearsAfterTimeout()
      {
         _queue.Add("A Message");

         string message = _queue.Next();

         Assert.IsNull(_queue.Next());

         _queue.ReviveMessages();

         Assert.AreEqual("A Message", _queue.Next());
      }
   }
}
