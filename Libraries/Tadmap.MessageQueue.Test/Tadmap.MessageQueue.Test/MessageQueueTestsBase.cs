using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tadmap.Messaging.Test.Mock;
using System.Threading;

namespace Tadmap.Messaging.Test
{
   public abstract class MessageQueueTestsBase
   {
      IMessageQueue _queue;

      protected abstract IMessageQueue CreateQueue();

      [SetUp]
      public virtual void SetUp()
      {
         _queue = CreateQueue();
      }

      [Test]
      public void CanAddMessage()
      {
         string message = "My Message";
         _queue.Add(message);
         Assert.AreEqual(message, _queue.Next(int.MaxValue).Content);
      }

      [Test]
      public void CanAddMultipleMessages()
      {
         List<string> messages = new List<string>();
         messages.Add("one");
         messages.Add("two");
         messages.Add("three");

         messages.ForEach(m => _queue.Add(m));

         var message = _queue.Next(int.MaxValue);

         while (message != null)  
         {
            Assert.IsNotEmpty(messages, "We weren't expecting any more massages");
            Assert.That(messages.Contains(message.Content), "We got a message we were not expecting.");
            messages.Remove(message.Content);
            message = _queue.Next(int.MaxValue);
         }

         Assert.IsEmpty(messages, "We didn't get all the messages.");
      }

      [Test]
      public void MessagesReappearsAfterTimeout()
      {
         _queue.Add("A Message");

         var message = _queue.Next(0);

         Assert.AreEqual("A Message", _queue.Next(int.MaxValue).Content);
      }

      [Test]
      public void MessageDoesntReappearsAfterRemove()
      {
         _queue.Add("A Message");

         var message = _queue.Next(0);
         _queue.Remove(message);

         Assert.IsNull(_queue.Next(int.MaxValue));
      }

   }
}
