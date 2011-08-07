using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
using System.IO;

namespace TadmapTests.DataAccess
{
   internal class TestFileFromDisk : IUploadedFile
   {
      FileStream _stream;
      string _path;

      public TestFileFromDisk(string path)
      {
         string dir = System.Environment.CurrentDirectory;
         _stream = new FileStream(path, FileMode.Open);
         _path = path;
      }

      #region IUploadedFile Members

      public string FileName
      {
         get { return Path.GetFileName(_path); }
      }

      public Stream InputStream
      {
         get { return _stream; }
      }

      public int ContentLength
      {
         get { return (int)_stream.Length; }
      }

      #endregion
   }
}
