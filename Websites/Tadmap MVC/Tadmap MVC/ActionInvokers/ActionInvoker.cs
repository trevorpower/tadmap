using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.HttpPostedFile;
using System.Reflection;

namespace Tadmap_MVC.ActionInvokers
{
   public class ActionInvoker : ControllerActionInvoker
   {
      protected override object GetParameterValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
      {
         if (parameterDescriptor.ParameterType == typeof(IPrincipal))
            return HttpContext.Current.User;

         if (parameterDescriptor.ParameterType == typeof(IUploadedFile))
            return new HttpPostedFileUploadedFile(HttpContext.Current.Request.Files[0]);

         return base.GetParameterValue(controllerContext, parameterDescriptor);
      }
   }
}
