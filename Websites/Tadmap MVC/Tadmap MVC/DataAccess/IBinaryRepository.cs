using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tadmap.DataAccess
{
   public interface IBinaryRepository
   {
      void Add(Stream data, string key, string contentType);

      Uri GetUrl(string key);
   }
}
