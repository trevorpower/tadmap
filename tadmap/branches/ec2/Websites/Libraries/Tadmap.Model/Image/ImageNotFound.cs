using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Tadmap.Model.Image
{
   [Serializable]
   public class ImageNotFoundException : Exception
   {
      public ImageNotFoundException()
         : base()
      {
      }

      public ImageNotFoundException(string message)
         : base(message)
      {
      }

      public ImageNotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      protected ImageNotFoundException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}
