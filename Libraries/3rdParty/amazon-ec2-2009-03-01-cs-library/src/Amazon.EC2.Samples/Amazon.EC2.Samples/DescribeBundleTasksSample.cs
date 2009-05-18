/******************************************************************************* 
 *  Copyright 2008 Amazon Technologies, Inc.
 *  Licensed under the Apache License, Version 2.0 (the "License"); 
 *  
 *  You may not use this file except in compliance with the License. 
 *  You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 *  specific language governing permissions and limitations under the License.
 * ***************************************************************************** 
 *    __  _    _  ___ 
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 * 
 *  Amazon EC2 CSharp Library
 *  API Version: 2009-03-01
 *  Generated: Thu Apr 09 21:42:47 PDT 2009 
 * 
 */

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Amazon.EC2;
using Amazon.EC2.Model;



namespace Amazon.EC2.Samples
{

    /// <summary>
    /// Describe Bundle Tasks  Samples
    /// </summary>
    public class DescribeBundleTasksSample
    {
    
                                                                                                 
        /// <summary>
        /// The DescribeBundleTasks operation describes in-progress
        /// and recent bundle tasks. Complete and failed tasks are
        /// removed from the list a short time after completion.
        /// If no bundle ids are given, all bundle tasks are returned.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeBundleTasksRequest request</param>
        public static void InvokeDescribeBundleTasks(AmazonEC2 service, DescribeBundleTasksRequest request)
        {
            try 
            {
                DescribeBundleTasksResponse response = service.DescribeBundleTasks(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeBundleTasksResponse");
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata  responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }
                if (response.IsSetDescribeBundleTasksResult())
                {
                    Console.WriteLine("            DescribeBundleTasksResult");
                    DescribeBundleTasksResult  describeBundleTasksResult = response.DescribeBundleTasksResult;
                    List<BundleTask> bundleTaskList = describeBundleTasksResult.BundleTask;
                    foreach (BundleTask bundleTask in bundleTaskList)
                    {
                        Console.WriteLine("                BundleTask");
                        if (bundleTask.IsSetInstanceId())
                        {
                            Console.WriteLine("                    InstanceId");
                            Console.WriteLine("                        {0}", bundleTask.InstanceId);
                        }
                        if (bundleTask.IsSetBundleId())
                        {
                            Console.WriteLine("                    BundleId");
                            Console.WriteLine("                        {0}", bundleTask.BundleId);
                        }
                        if (bundleTask.IsSetBundleState())
                        {
                            Console.WriteLine("                    BundleState");
                            Console.WriteLine("                        {0}", bundleTask.BundleState);
                        }
                        if (bundleTask.IsSetStartTime())
                        {
                            Console.WriteLine("                    StartTime");
                            Console.WriteLine("                        {0}", bundleTask.StartTime);
                        }
                        if (bundleTask.IsSetUpdateTime())
                        {
                            Console.WriteLine("                    UpdateTime");
                            Console.WriteLine("                        {0}", bundleTask.UpdateTime);
                        }
                        if (bundleTask.IsSetStorage())
                        {
                            Console.WriteLine("                    Storage");
                            Storage  storage = bundleTask.Storage;
                            if (storage.IsSetS3())
                            {
                                Console.WriteLine("                        S3");
                                S3Storage  s3 = storage.S3;
                                if (s3.IsSetBucket())
                                {
                                    Console.WriteLine("                            Bucket");
                                    Console.WriteLine("                                {0}", s3.Bucket);
                                }
                                if (s3.IsSetPrefix())
                                {
                                    Console.WriteLine("                            Prefix");
                                    Console.WriteLine("                                {0}", s3.Prefix);
                                }
                                if (s3.IsSetAWSAccessKeyId())
                                {
                                    Console.WriteLine("                            AWSAccessKeyId");
                                    Console.WriteLine("                                {0}", s3.AWSAccessKeyId);
                                }
                                if (s3.IsSetUploadPolicy())
                                {
                                    Console.WriteLine("                            UploadPolicy");
                                    Console.WriteLine("                                {0}", s3.UploadPolicy);
                                }
                                if (s3.IsSetUploadPolicySignature())
                                {
                                    Console.WriteLine("                            UploadPolicySignature");
                                    Console.WriteLine("                                {0}", s3.UploadPolicySignature);
                                }
                            }
                        }
                        if (bundleTask.IsSetProgress())
                        {
                            Console.WriteLine("                    Progress");
                            Console.WriteLine("                        {0}", bundleTask.Progress);
                        }
                        if (bundleTask.IsSetBundleTaskError())
                        {
                            Console.WriteLine("                    BundleTaskError");
                            BundleTaskError  bundleTaskError = bundleTask.BundleTaskError;
                            if (bundleTaskError.IsSetCode())
                            {
                                Console.WriteLine("                        Code");
                                Console.WriteLine("                            {0}", bundleTaskError.Code);
                            }
                            if (bundleTaskError.IsSetMessage())
                            {
                                Console.WriteLine("                        Message");
                                Console.WriteLine("                            {0}", bundleTaskError.Message);
                            }
                        }
                    }
                }

            } 
            catch (AmazonEC2Exception ex) 
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
            }
        }
                                                                                                }
}
