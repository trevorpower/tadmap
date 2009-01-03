using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Tadmap_MVC.DataAccess;
using Tadmap_MVC.DataAccess.HttpPostedFile;

namespace Tadmap_MVC.ActionInvokers
{
   public class ActionInvoker : ControllerActionInvoker
   {
      protected override object GetParameterValue(System.Reflection.ParameterInfo parameterInfo)
      {
         if (parameterInfo.ParameterType == typeof(IPrincipal))
            return HttpContext.Current.User;

         if (parameterInfo.ParameterType == typeof(IUploadedFile))
            return new HttpPostedFileUploadedFile( HttpContext.Current.Request.Files[parameterInfo.Name] );

         return base.GetParameterValue(parameterInfo);
      }
   }
}
