using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using Tadmap.Model.Image;
using Affirma.ThreeSharp.Wrapper;

namespace Tadmap.Amazon
{
   public class BinaryRepository : IBinaryRepository
   {
      private IThreeSharp _service;

      private string BucketName { get; set; }
      private string AccessKey { get; set; }
      private string SecretKey { get; set; }

      private BinaryRepository()
      {
         throw new NotSupportedException("Requires access keys");
      }

      public BinaryRepository(string accessKey, string secretKey, string bucketName)
      {
         AccessKey = accessKey;
         SecretKey = secretKey;
         BucketName = bucketName;

         ThreeSharpConfig config;

         config = new ThreeSharpConfig();
         config.AwsAccessKeyID = accessKey;
         config.AwsSecretAccessKey = secretKey;
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

         try
         {
            String redirectUrl = null;
            using (BucketListRequest testRequest = new BucketListRequest(BucketName))
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

            using (ObjectAddRequest request = new ObjectAddRequest(BucketName, key))
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
         catch (ThreeSharpException exception)
         {
            throw new BinaryRepositoryException("Upload Failed", exception);
         }
      }

      public Uri GetUrl(string key)
      {
         ThreeSharpWrapper s3 = new ThreeSharpWrapper(AccessKey, SecretKey);
         return new Uri(s3.GetUrl(BucketName, key));
      }


      public Stream GetBinary(string key)
      {
         try
         {
            MemoryStream stream = new MemoryStream();
            using (ObjectGetRequest objectGetRequest = new ObjectGetRequest(BucketName, key))
            using (ObjectGetResponse objectGetResponse = _service.ObjectGet(objectGetRequest))
            {

               byte[] buffer = new byte[1024];
               int bytesRead = 0;
               while (true)
               {
                  bytesRead = objectGetResponse.DataStream.Read(buffer, 0, buffer.Length);
                  if (bytesRead == 0)
                     break;

                  stream.Write(buffer, 0, bytesRead);
               }
            }

            return stream;
         }
         catch (ThreeSharpException e)
         {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
               return null;

            throw;
         }
      }

      #endregion
   }
}
