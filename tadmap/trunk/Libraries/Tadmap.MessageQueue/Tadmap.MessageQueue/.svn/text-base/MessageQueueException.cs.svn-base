using System;
using System.Runtime.Serialization;

namespace Tadmap.Messaging
{
   /// <summary>
   ///  The exception that is thrown when a non-fatal application
   ///  error occurs inside a Tadmap.MessageQueue.IMessageQueue queue implementation.
   /// </summary>
   public class MessageQueueException : ApplicationException
   {
      /// <summary>
      /// Initializes a new instance of the Tadmap.MessageQueue.MessageQueueException class.
      /// </summary>
      public MessageQueueException()
      {
      }

      /// <summary>
      /// Initializes a new instance of the Tadmap.MessageQueue.MessageQueueException class with
      /// a specified error message.
      /// </summary>
      /// <param name="message">A message that describes the error.</param>
      public MessageQueueException(string message) :
         base(message)
      {
      }

      /// <summary>
      /// Initializes a new instance of the Tadmap.MessageQueue.MessageQueueException class with
      /// serialized data.
      /// </summary>
      /// <param name="info">The object that holds the serialized object data.</param>
      /// <param name="context">The contextual information about the source or destination.</param>
      protected MessageQueueException(SerializationInfo info, StreamingContext context) :
         base(info, context)
      {
      }

      /// <summary>
      /// Initializes a new instance of the Tadmap.MessageQueue.MessageQueueException class with
      /// a specified error message and a reference to the inner exception that is</summary>
      /// the cause of this exception.
      /// <param name="message">The error message that explains the reason for the exception.</param>
      /// <param name="innerException">
      /// The exception that is the cause of the current exception. If the innerException
      /// parameter is not a null reference, the current exception is raised in a catch
      /// block that handles the inner exception.
      /// </param>
      public MessageQueueException(string message, Exception innerException):
         base(message, innerException)
      {
      }
   }
}
