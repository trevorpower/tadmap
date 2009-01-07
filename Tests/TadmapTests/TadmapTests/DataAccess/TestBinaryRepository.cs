using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tadmap_MVC.DataAccess;

namespace TadmapTests.DataAccess
{
   internal class TestBinaryRepository : IBinaryRepository
   {
      #region IBinaryRepository Members

      public void Add(System.IO.Stream data, string key, string contentType)
      {
         
      }

      public string GetUrl(string key)
      {
         return null;
      }

      #endregion
   }
}
