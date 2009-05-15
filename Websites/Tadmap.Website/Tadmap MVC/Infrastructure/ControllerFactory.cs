using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tadmap.Infrastructure
{
   public class ControllerFactory : DefaultControllerFactory
   {
      protected override IController GetControllerInstance(Type controllerType)
      {
         return TadmapApplication.Container.Resolve(controllerType) as IController;
      }
      
   }
}
