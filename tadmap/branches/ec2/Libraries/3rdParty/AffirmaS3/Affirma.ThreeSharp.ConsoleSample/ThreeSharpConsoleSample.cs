/******************************************************************************* 
 *  Licensed under the Apache License, Version 2.0 (the "License"); 
 *  
 *  You may not use this file except in compliance with the License. 
 *  You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0.html 
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 *  specific language governing permissions and limitations under the License.
 * ***************************************************************************** 
 * 
 *  Joel Wetzel
 *  Affirma Consulting
 *  jwetzel@affirmaconsulting.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp.Query;
using Affirma.ThreeSharp.Wrapper;

namespace Affirma.ThreeSharp.ConsoleSample
{
    class ThreeSharpConsoleSample
    {
        // Insert your AWS access keys here.
        private static readonly string awsAccessKeyId = "<INSERT YOUR ACCESS KEY HERE>";
        private static readonly string awsSecretAccessKey = "<INSERT YOUR SECRET ACCESS KEY HERE>";

        // These must point to an existing file to test file upload streaming.
        private static readonly string uploadPath = "c:\\";
        private static readonly string uploadFile = "Test.txt";
        private static readonly string downloadPath = "c:\\";

        // The ThreeSharp Library uses DES encryption.  This requires an eight-character key, and
        // and eight-character initialization vector.
        private static readonly string encryptionKey = "ABCDEFGH";
        private static readonly string encryptionIV = "IJKLMNOP";

        static void Main(string[] args)
        {
            try
            {
                if (awsAccessKeyId.StartsWith("<INSERT") || awsSecretAccessKey.StartsWith("<INSERT"))
                    throw new Exception("You must edit the code and insert your access keys to run the samples.");

                DemonstrateThreeSharp();

                DemonstrateThreeSharpWrapper();
            }
            catch (ThreeSharpException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("An exception occurred.");
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("HTTP Status Code: " + ex.StatusCode.ToString());
                sb.AppendLine("Error Code: " + ex.ErrorCode);
                sb.AppendLine("XML: " + ex.XML);
                Console.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DemonstrateThreeSharp()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Demonstrating the ThreeSharp Library");
            Console.WriteLine("===========================================");

            ThreeSharpConfig config = new ThreeSharpConfig();
            config.AwsAccessKeyID = awsAccessKeyId;
            config.AwsSecretAccessKey = awsSecretAccessKey;

            IThreeSharp service = new ThreeSharpQuery(config);

            // Convert the bucket name to lowercase for vanity domains.
            // the bucket must be lower case since DNS is case-insensitive.
            Random r = new Random();
            string testBucketName = awsAccessKeyId.ToLower() + "-test-bucket" + r.Next(50000).ToString();;
            string testBucketName2 = awsAccessKeyId.ToLower() + "-test-bucket" + r.Next(50000).ToString(); ;
            string stringKeyName = "StringObject";
            string encryptedFileKeyName = "EncryptedFileObject";


            Console.WriteLine("\n----- Creating Bucket " + testBucketName + " -----");
            using (BucketAddRequest request = new BucketAddRequest(testBucketName))
            using (BucketAddResponse response = service.BucketAdd(request))
            { }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Creating Bucket " + testBucketName2 + " -----");
            using (BucketAddRequest request = new BucketAddRequest(testBucketName2))
            using (BucketAddResponse response = service.BucketAdd(request))
            { }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName2 + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName2))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Adding String Object to " + testBucketName + " -----");
            using (ObjectAddRequest request = new ObjectAddRequest(testBucketName, stringKeyName))
            {
                request.LoadStreamWithString("This is a string object.");
                using (ObjectAddResponse response = service.ObjectAdd(request))
                { }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Getting String Object -----");
            using (ObjectGetRequest request = new ObjectGetRequest(testBucketName, stringKeyName))
            {
                using (ObjectGetResponse response = service.ObjectGet(request))
                {
                    Console.WriteLine(response.StreamResponseToString());
                }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Copying String Object to " + testBucketName2 + " -----");
            using (ObjectCopyRequest request = new ObjectCopyRequest(testBucketName, stringKeyName, testBucketName2, stringKeyName + "Copy"))
            using (ObjectCopyResponse response = service.ObjectCopy(request))
            {
                int i = 0;
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName2 + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName2))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Getting Copied String Object -----");
            using (ObjectGetRequest request = new ObjectGetRequest(testBucketName2, stringKeyName + "Copy"))
            {
                using (ObjectGetResponse response = service.ObjectGet(request))
                {
                    Console.WriteLine(response.StreamResponseToString());
                }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();


            Console.WriteLine("\n----- Streaming File Object -----");
            using (ObjectAddRequest request = new ObjectAddRequest(testBucketName, uploadFile))
            {
                request.LoadStreamWithFile(uploadPath + uploadFile);
                using (ObjectAddResponse response = service.ObjectAdd(request))
                { }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming/Encrypting File Object -----");
            using (ObjectAddRequest request = new ObjectAddRequest(testBucketName, uploadFile + "Encrypted"))
            {
                request.LoadStreamWithFile(uploadPath + uploadFile);
                request.EncryptStream(encryptionKey, encryptionIV);
                using (ObjectAddResponse response = service.ObjectAdd(request))
                { }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming File to Disk -----");
            using (ObjectGetRequest request = new ObjectGetRequest(testBucketName, uploadFile))
            using (ObjectGetResponse response = service.ObjectGet(request))
            {
                response.StreamResponseToFile(downloadPath + uploadFile);
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming/Decrypting File to Disk -----");
            using (ObjectGetRequest request = new ObjectGetRequest(testBucketName, uploadFile + "Encrypted"))
            using (ObjectGetResponse response = service.ObjectGet(request))
            {
                response.DecryptStream(encryptionKey, encryptionIV);
                response.StreamResponseToFile(downloadPath + "decrypted-" + uploadFile);
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Query String Authorization Example -----");
            using (UrlGetRequest request = new UrlGetRequest(testBucketName, uploadFile))
            {
                request.ExpiresIn = 60 * 1000;
                using (UrlGetResponse response = service.UrlGet(request))
                {
                    Console.WriteLine("Try this url in your web browser (it will only work for 60 seconds)\n");
                    Console.WriteLine(response.StreamResponseToString());
                }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Adding String Object with Metadata and Public Read ACL -----");
            using (ObjectAddRequest request = new ObjectAddRequest(testBucketName, stringKeyName + "Public"))
            {
                request.LoadStreamWithString("This is a publicly readable test.");
                request.MetaData.Add("blah", "foo");
                //request.Headers.Add("x-amz-acl", "private");
                request.Headers.Add("x-amz-acl", "public-read");
                //request.Headers.Add("x-amz-acl", "public-read-write");
                //request.Headers.Add("x-amz-acl", "authenticated-read");
                using (ObjectAddResponse response = service.ObjectAdd(request))
                { }
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Getting Object's ACL -----");
            using (ACLGetRequest request = new ACLGetRequest(testBucketName, stringKeyName + "Public"))
            using (ACLGetResponse response = service.ACLGet(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Objects -----");
            using (ObjectDeleteRequest request = new ObjectDeleteRequest(testBucketName, stringKeyName))
            using (ObjectDeleteResponse response = service.ObjectDelete(request))
            { }
            using (ObjectDeleteRequest request = new ObjectDeleteRequest(testBucketName2, stringKeyName + "Copy"))
            using (ObjectDeleteResponse response = service.ObjectDelete(request))
            { }
            using (ObjectDeleteRequest request = new ObjectDeleteRequest(testBucketName, uploadFile))
            using (ObjectDeleteResponse response = service.ObjectDelete(request))
            { }
            using (ObjectDeleteRequest request = new ObjectDeleteRequest(testBucketName, uploadFile + "Encrypted"))
            using (ObjectDeleteResponse response = service.ObjectDelete(request))
            { }
            using (ObjectDeleteRequest request = new ObjectDeleteRequest(testBucketName, stringKeyName + "Public"))
            using (ObjectDeleteResponse response = service.ObjectDelete(request))
            { }

            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName2 + " -----");
            using (BucketListRequest request = new BucketListRequest(testBucketName2))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing All My Buckets -----");
            using (BucketListRequest request = new BucketListRequest(null))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Bucket " + testBucketName + " -----");
            using (BucketDeleteRequest request = new BucketDeleteRequest(testBucketName))
            using (BucketDeleteResponse response = service.BucketDelete(request))
            { }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Bucket " + testBucketName2 + " -----");
            using (BucketDeleteRequest request = new BucketDeleteRequest(testBucketName2))
            using (BucketDeleteResponse response = service.BucketDelete(request))
            { }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing All My Buckets -----");
            using (BucketListRequest request = new BucketListRequest(null))
            using (BucketListResponse response = service.BucketList(request))
            {
                Console.WriteLine(response.StreamResponseToString());
            }
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\nTotal data transfers: " + service.GetTransfers().Length.ToString());
            Console.WriteLine("Total bytes uploaded: " + service.GetTotalBytesUploaded().ToString());
            Console.WriteLine("Total bytes downloaded: " + service.GetTotalBytesDownloaded().ToString());
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

        }


        static void DemonstrateThreeSharpWrapper()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Demonstrating the ThreeSharp Library Wrapper");
            Console.WriteLine("===========================================");

            // Convert the bucket name to lowercase for vanity domains.
            // the bucket must be lower case since DNS is case-insensitive.
            Random r = new Random();
            string testBucketName = awsAccessKeyId.ToLower() + "-test-bucket" + r.Next(50000).ToString(); ;
            string testBucketName2 = awsAccessKeyId.ToLower() + "-test-bucket" + r.Next(50000).ToString(); ;
            string stringKeyName = "StringObject";

            ThreeSharpWrapper wrapper = new ThreeSharpWrapper(awsAccessKeyId, awsSecretAccessKey);

            Console.WriteLine("\n----- Creating Bucket " + testBucketName + " -----");
            wrapper.AddBucket(testBucketName);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName + " -----");
            Console.WriteLine(wrapper.ListBucket(testBucketName));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Creating Bucket " + testBucketName2 + " -----");
            wrapper.AddBucket(testBucketName2);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName2 + " -----");
            Console.WriteLine(wrapper.ListBucket(testBucketName2));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Adding String Object -----");
            wrapper.AddStringObject(testBucketName, stringKeyName, "This is a test");
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Copying String Object -----");
            wrapper.CopyObject(testBucketName, stringKeyName, testBucketName2, stringKeyName + "Copy");
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Getting Copied String Object -----");
            Console.WriteLine(wrapper.GetStringObject(testBucketName2, stringKeyName + "Copy"));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming File Object -----");
            wrapper.AddFileObject(testBucketName, uploadFile, uploadPath + uploadFile);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming/Encrypting File Object -----");
            wrapper.AddEncryptFileObject(testBucketName, uploadFile + "Encrypted", uploadPath + uploadFile, encryptionKey, encryptionIV);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket -----");
            Console.WriteLine(wrapper.ListBucket(testBucketName));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming File to Disk -----");
            wrapper.GetFileObject(testBucketName, uploadFile, downloadPath + uploadFile);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Streaming/Decrypting File to Disk -----");
            wrapper.GetDecryptFileObject(testBucketName, uploadFile + "Encrypted", downloadPath + "decrypted-" + uploadFile, encryptionKey, encryptionIV);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Query String Authorization Example -----");
            Console.WriteLine("Try this url in your web browser (it will only work for 60 seconds)\n");
            Console.WriteLine(wrapper.GetUrl(testBucketName, uploadFile));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Objects -----");
            wrapper.DeleteObject(testBucketName, stringKeyName);
            wrapper.DeleteObject(testBucketName2, stringKeyName + "Copy");
            wrapper.DeleteObject(testBucketName, uploadFile);
            wrapper.DeleteObject(testBucketName, uploadFile + "Encrypted");
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName + " -----");
            Console.WriteLine(wrapper.ListBucket(testBucketName));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing Bucket " + testBucketName2 + " -----");
            Console.WriteLine(wrapper.ListBucket(testBucketName2));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing All My Buckets -----");
            Console.WriteLine(wrapper.ListBucket(null));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Bucket " + testBucketName + " -----");
            wrapper.DeleteBucket(testBucketName);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Deleting Bucket " + testBucketName2 + " -----");
            wrapper.DeleteBucket(testBucketName2);
            Console.WriteLine("\npress enter >");
            Console.ReadLine();

            Console.WriteLine("\n----- Listing All My Buckets -----");
            Console.WriteLine(wrapper.ListBucket(null));
            Console.WriteLine("\npress enter >");
            Console.ReadLine();
            
        }

    }
}
