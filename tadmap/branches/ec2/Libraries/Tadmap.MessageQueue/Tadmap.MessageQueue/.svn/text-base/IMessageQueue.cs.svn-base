using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Messaging
{
   /// <summary>
   /// Interface for a basic message queue.
   /// </summary>
   public interface IMessageQueue
   {
      /// <summary>
      /// Add a message to the queue.
      /// </summary>
      /// <param name="message">The content that the new message should have.</param>
      void Add(string message);

      /// <summary>
      /// Gets a message from the queue. The message will
      /// expire after the timeout and will be available on the queue
      /// again.
      /// </summary>
      /// <param name="timeout">The timeout in seconds.</param>
      /// <returns>A message if one is available or null otherwise.</returns>
      IMessage Next(int timeout);

      /// <summary>
      /// Removes the given message from the queue.
      /// </summary>
      /// <param name="message">The message to remove.</param>
      void Remove(IMessage message);
   }
}
