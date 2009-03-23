using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tadmap.Model.Image;

namespace Tadmap.Model.Test.Mock
{
   public class BinaryRepository : IBinaryRepository
   {
      private List<Data> _storage;

      private BinaryRepository()
      {
         throw new NotSupportedException("Must provide a storage list.");
      }

      public BinaryRepository(List<Data> storage)
      {
         _storage = storage;
      }

      #region IBinaryRepository Members

      public void Add(System.IO.Stream data, string key, string contentType)
      {
         Data newData = new Data { Key = key, ContenType = contentType };
         newData.Binary = new byte[data.Length];

         data.Read(newData.Binary, 0, newData.Binary.Length);

         _storage.Add(newData);
      }

      public Uri GetUrl(string key)
      {
         return new Uri("http://" + key + ".url");
      }

      public Stream GetBinary(string key)
      {
         return new MemoryStream(_storage.Find(d => d.Key == key).Binary);
      }

      #endregion

      public class Data
      {
         public byte[] Binary { get; set; }
         public string Key { get; set; }
         public string ContenType { get; set; }
      }
   }

}
