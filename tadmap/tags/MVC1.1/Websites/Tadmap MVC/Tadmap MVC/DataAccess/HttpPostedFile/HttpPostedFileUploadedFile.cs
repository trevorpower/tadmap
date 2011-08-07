using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tadmap_MVC.DataAccess.HttpPostedFile
{
   public class HttpPostedFileUploadedFile : IUploadedFile
   {
      System.Web.HttpPostedFile _file;

      public HttpPostedFileUploadedFile(System.Web.HttpPostedFile file)
      {
         _file = file;
      }

      #region IUploadedFile Members

      public System.IO.Stream InputStream
      {
         get { return _file.InputStream;  }
      }

      public string FileName
      {
         get { return _file.FileName; }
      }

      public int ContentLength
      {
         get { return _file.ContentLength; }
      }

      #endregion
   }
}
