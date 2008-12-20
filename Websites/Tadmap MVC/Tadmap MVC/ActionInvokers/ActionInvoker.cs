using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace Tadmap_MVC.ActionInvokers
{
   public class ActionInvoker : ControllerActionInvoker
   {
      protected override object GetParameterValue(System.Reflection.ParameterInfo parameterInfo)
      {
         if (parameterInfo.Name == "principal")
            return HttpContext.Current.User;

         return base.GetParameterValue(parameterInfo);
      }
   }
}
