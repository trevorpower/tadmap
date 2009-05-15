using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
using System.IO;
using System.Web.Mvc;
using System.Web;
using Tadmap.Model.Image;


namespace Tadmap.DataAccess
{
   public class FileSystemBinaryRepository : IBinaryRepository
   {
      #region IBinaryRepository Members

      public void Add(System.IO.Stream data, string key, string contentType)
      {
         if (data.Length == 0)
            return;

         FileStream file = new FileStream("LocalBinaryRepository/" + key, FileMode.Create);
         
         int theByte = data.ReadByte();
         while (theByte != -1)
         {
            file.WriteByte((byte)theByte);
            theByte = data.ReadByte();
         }
      }

      public Uri GetUrl(string key)
      {
         return new Uri("http://localhost:51277/File.mvc/" + HttpUtility.UrlEncode(key));
      }

      #endregion
   }
}
