using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;
using System.IO;

namespace TadmapTests.DataAccess
{
   internal class TestBinaryRepository : IBinaryRepository
   {
      #region IBinaryRepository Members

      public void Add(System.IO.Stream data, string key, string contentType)
      {
         if (data.Length == 0)
            return;

         FileStream file = new FileStream("../../TestBinaryRepository/" + key, FileMode.Create);
         
         int theByte = data.ReadByte();
         while (theByte != -1)
         {
            file.WriteByte((byte)theByte);
            theByte = data.ReadByte();
         }
      }

      public string GetUrl(string key)
      {
         return key + "url";
      }

      #endregion
   }
}
