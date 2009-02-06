using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tadmap.DataAccess
{
   public interface IUploadedFile
   {
      Stream InputStream { get; }
      string FileName { get; }
      int ContentLength { get; }
   }
}
