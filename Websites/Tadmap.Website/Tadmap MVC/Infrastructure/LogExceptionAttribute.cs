using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tadmap.Infrastructure.ErrorHandling;

namespace Tadmap.Infrastructure
{
   public class LogExceptionAttribute : ActionFilterAttribute, IExceptionFilter
   {
      #region IExceptionFilter Members

      public void OnException(ExceptionContext filterContext)
      {
         IErrorHandler errorHandler = new EmailErrorHandler();

         errorHandler.HandleException(filterContext.Exception);
      }

      #endregion
   }
}
