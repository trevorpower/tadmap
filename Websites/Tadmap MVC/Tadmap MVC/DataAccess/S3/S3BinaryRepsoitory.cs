using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Affirma.ThreeSharp.Model;
using TadMap.Configuration;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;

namespace Tadmap_MVC.DataAccess.S3
{
   public class S3BinaryRepository : IBinaryRepository
   {
      private IThreeSharp _service;

      public S3BinaryRepository()
      {
         ThreeSharpConfig config;
         
         config = new ThreeSharpConfig();
         config.AwsAccessKeyID = S3Storage.AccessKey;
         config.AwsSecretAccessKey = S3Storage.SecretAccessKey;
         config.ConnectionLimit = 40;
         config.IsSecure = true;

         _service = new ThreeSharpQuery(config);
      }

      #region IBinaryRepository Members

      public void Add(Stream data, string key, string contentType)
      {
         using (ObjectAddRequest objectAddRequest = new ObjectAddRequest(S3Storage.BucketName, key))
         {
            byte[] buffer = new byte[data.Length];

            data.Read(buffer, 0, (int)data.Length);

            objectAddRequest.LoadStreamWithBytes(buffer, contentType);

            using (ObjectAddResponse objectAddResponse = _service.ObjectAdd(objectAddRequest))
            { }
         }
      }

      public string GetUrl(string key)
      {
         throw new NotImplementedException();
      }

      #endregion
   }
}
