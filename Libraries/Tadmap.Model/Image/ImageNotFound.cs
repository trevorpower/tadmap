﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Tadmap.Models.Image
{
   [Serializable]
   public class ImageNotFoundException : Exception
   {
      public ImageNotFoundException()
         : base()
      {
      }
   }
}
