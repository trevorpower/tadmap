using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Infrastructure.ErrorHandling;

namespace Tests.Infrastructure.ErrorHandling
{
   public class EmailErrorHandler : IErrorHandler
   {
      #region IErrorHandler Members

      public void HandleException(Exception exception)
      {
         StringBuilder message = new StringBuilder("An error occurred in the application:");
         message.AppendLine(exception.Message);
         message.AppendLine("Stack trace:");
         message.AppendLine(exception.StackTrace);

         StringBuilder s = new StringBuilder(exception.StackTrace);
         message.Append(s.Replace(" at ", " at \n"));

         message.AppendLine("Source:");
         message.AppendLine(exception.Source);

         // Mail the message to the developer
         System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

         client.Send("error@tadmap.com", "trevor_power@yahoo.com", "Tadmap Error", message.ToString());
      }

      #endregion
   }
}
