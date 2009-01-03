using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;
using System.IO;

namespace TadmapTests.DataAccess
{
   internal class TestUploadedFile : IUploadedFile
   {
      #region IUploadedFile Members

      public string FileName
      {
         get { return ""; }
      }

      public Stream InputStream
      {
         get { return new MemoryStream(); }
      }

      public int ContentLength
      {
         get { return 0; }
      }

      #endregion
   }
}
