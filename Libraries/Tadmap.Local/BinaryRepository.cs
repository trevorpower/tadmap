using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.Model.Image;
using System.IO;
using System.Web;

namespace Tadmap.Local
{
   public class BinaryRepository : IBinaryRepository
   {
      string _path;

      public BinaryRepository(string path)
      {
         _path = path;
      }

      #region IBinaryRepository Members

      public void Add(System.IO.Stream data, string key, string contentType)
      {
         if (data.Length == 0)
            return;

         using (FileStream file = new FileStream(Path.Combine(_path, key), FileMode.Create, FileAccess.Write))
         {
            int theByte = data.ReadByte();
            while (theByte != -1)
            {
               file.WriteByte((byte)theByte);
               theByte = data.ReadByte();
            }

            file.Close();
         }
      }

      public Uri GetUrl(string key)
      {
         return new Uri("http://localhost:51277/File.mvc/" + HttpUtility.UrlEncode(key));
      }

      public Stream GetBinary(string key)
      {
         return new FileStream(Path.Combine(_path, key), FileMode.Open, FileAccess.Read);
      }

      #endregion
   }
}
