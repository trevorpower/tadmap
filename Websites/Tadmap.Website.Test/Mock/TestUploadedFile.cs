using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
using System.IO;

namespace Tadmap.Website.Test.Mock
{
   internal class TestEmptyFile : IUploadedFile
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
