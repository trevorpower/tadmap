using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tadmap.ActionFIlters
{
   public class PrincipalActionFilterAttribute : ActionFilterAttribute
   {
      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         base.OnActionExecuting(filterContext);
      }
   }
}
