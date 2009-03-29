using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Messaging
{
   public interface IMessage
   {
      string Content { get; }
   }
}
