using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tadmap.Infrastructure.ErrorHandling
{
   public interface IErrorHandler
   {
      void HandleException(Exception exception);
   }
}
