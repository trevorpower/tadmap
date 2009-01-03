using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tadmap_MVC.DataAccess
{
   public interface IUploadedFile
   {
      Stream InputStream { get; }
      string FileName { get; }
      int ContentLength { get; }
   }
}
