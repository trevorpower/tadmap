using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap.DataAccess;
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

      public Uri GetUrl(string key)
      {
         return new Uri("http://" + key + ".url");
      }

      #endregion
   }
}
