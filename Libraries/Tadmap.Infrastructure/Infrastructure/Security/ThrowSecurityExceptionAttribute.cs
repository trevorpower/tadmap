using System.Web.Mvc;
using System.Security;
using System;

namespace Infrastructure.Security
{
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
   public class ThrowSecurityExceptionAttribute : AuthorizeAttribute
   {
      public override void OnAuthorization(AuthorizationContext filterContext)
      {
         if (!AuthorizeCore(filterContext.HttpContext))
         {
            throw new SecurityException();
         }
      }
   }
}
