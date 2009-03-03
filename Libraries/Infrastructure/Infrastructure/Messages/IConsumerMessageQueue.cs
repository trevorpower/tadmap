using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Messages
{
   public interface IConsumerMessageQueue
   {
      string Next();

      void Complete();
   }
}
