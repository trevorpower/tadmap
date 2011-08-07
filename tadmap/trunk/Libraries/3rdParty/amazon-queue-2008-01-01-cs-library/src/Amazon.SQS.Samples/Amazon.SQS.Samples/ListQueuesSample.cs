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
 *  Amazon SQS CSharp Library
 *  API Version: 2008-01-01
 *  Generated: Fri Dec 26 23:54:02 PST 2008 
 * 
 */

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Amazon.SQS;
using Amazon.SQS.Model;



namespace Amazon.SQS.Samples
{

    /// <summary>
    /// List Queues  Samples
    /// </summary>
    public class ListQueuesSample
    {
    
                             
        /// <summary>
        /// 
        /// The ListQueues action returns a list of your queues.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonSQS service</param>
        /// <param name="request">ListQueuesRequest request</param>
        public static void InvokeListQueues(AmazonSQS service, ListQueuesRequest request)
        {
            try 
            {
                ListQueuesResponse response = service.ListQueues(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        ListQueuesResponse");
                if (response.IsSetListQueuesResult()) 
                {
                    Console.WriteLine("            ListQueuesResult");
                    ListQueuesResult  listQueuesResult = response.ListQueuesResult;
                    List<String> queueUrlList  =  listQueuesResult.QueueUrl;
                    foreach (String queueUrl in queueUrlList) 
                    { 
                        Console.WriteLine("                QueueUrl");
                        Console.WriteLine("                    {0}", queueUrl);
                    }	
                } 
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

            } 
            catch (AmazonSQSException ex) 
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
