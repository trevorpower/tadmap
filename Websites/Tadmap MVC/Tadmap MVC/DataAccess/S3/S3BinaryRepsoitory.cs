using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Affirma.ThreeSharp.Model;
using Tadmap.Configuration;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using Tadmap.Model.Image;
using Affirma.ThreeSharp.Wrapper;

namespace Tadmap.DataAccess.S3
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
         config.Format = CallingFormat.SUBDOMAIN;

         _service = new ThreeSharpQuery(config);
      }

      #region IBinaryRepository Members

      public void Add(Stream data, string key, string contentType)
      {
         //using (ObjectAddRequest objectAddRequest = new ObjectAddRequest(S3Storage.BucketName, key))
         //{
         //   byte[] buffer = new byte[data.Length];

         //   data.Read(buffer, 0, (int)data.Length);

         //   objectAddRequest.LoadStreamWithBytes(buffer, contentType);

         //   using (ObjectAddResponse objectAddResponse = _service.ObjectAdd(objectAddRequest))
         //   { }
         //}

         // The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
         // minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to upload
         // the file to the redirect URL
         String redirectUrl = null;
         using (BucketListRequest testRequest = new BucketListRequest(S3Storage.BucketName))
         {
            testRequest.Method = "HEAD";
            using (BucketListResponse testResponse = _service.BucketList(testRequest))
            {
               if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
               {
                  redirectUrl = testResponse.Headers["Location"].ToString() + key;
               }
            }
         }

         using (ObjectAddRequest request = new ObjectAddRequest(S3Storage.BucketName, key))
         {
            //request.Headers.Add("x-amz-acl", "public-read-write");
            //request.ContentType = ThreeSharpUtils.ConvertExtensionToMimeType(Path.GetExtension(strKey));
            //request.DataStream = oStream;
            //request.BytesTotal = (request.DataStream).Length;
            //request.Timeout = iTimeout;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);
            request.LoadStreamWithBytes(buffer, contentType);

            if (redirectUrl != null)
            {
               request.RedirectUrl = redirectUrl;
            }

            using (ObjectAddResponse response = _service.ObjectAdd(request))
            { }
         }
      }

      public Uri GetUrl(string key)
      {
         ThreeSharpWrapper s3 = new ThreeSharpWrapper(S3Storage.AccessKey, S3Storage.SecretAccessKey);
         return new Uri(s3.GetUrl(S3Storage.BucketName, key));
      }

      public Stream Get(string key)
      {
         MemoryStream stream = new MemoryStream();
 
         using (ObjectGetRequest objectGetRequest = new ObjectGetRequest("tadtestus", key))
         using (ObjectGetResponse objectGetResponse = _service.ObjectGet(objectGetRequest))
         {
            TransferStream(objectGetResponse.DataStream, stream);
         }

         return stream;
      }

      private void TransferStream(Stream responseStream, Stream outputStream)
      {
         //long bytesTransferred = 0;
         byte[] buffer = new byte[1024];
         int bytesRead = 0;
         while (true)
         {
            bytesRead = responseStream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0)
               break;

            outputStream.Write(buffer, 0, bytesRead);
            //bytesTransferred += bytesRead;
         }
         //this.BytesTotal = bytesTransferred
      }

      
      #endregion
   }
}
