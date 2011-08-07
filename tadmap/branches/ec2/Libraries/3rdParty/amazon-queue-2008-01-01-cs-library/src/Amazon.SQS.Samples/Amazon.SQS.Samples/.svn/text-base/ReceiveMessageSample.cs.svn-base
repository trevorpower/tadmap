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
    /// Receive Message  Samples
    /// </summary>
    public class ReceiveMessageSample
    {
    
                                             
        /// <summary>
        /// 
        /// Retrieves one or more messages from the specified queue, including the message body and message ID of each message. Messages returned by this action stay in the queue until you delete them. However, once a message is returned to a ReceiveMessage request, it is not returned on subsequent ReceiveMessage requests for the duration of the VisibilityTimeout. If you do not specify a VisibilityTimeout in the request, the overall visibility timeout for the queue is used for the returned messages.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonSQS service</param>
        /// <param name="request">ReceiveMessageRequest request</param>
        public static void InvokeReceiveMessage(AmazonSQS service, ReceiveMessageRequest request)
        {
            try 
            {
                ReceiveMessageResponse response = service.ReceiveMessage(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        ReceiveMessageResponse");
                if (response.IsSetReceiveMessageResult()) 
                {
                    Console.WriteLine("            ReceiveMessageResult");
                    ReceiveMessageResult  receiveMessageResult = response.ReceiveMessageResult;
                    List<Message> messageList = receiveMessageResult.Message;
                    foreach (Message message in messageList) 
                    {
                        Console.WriteLine("                Message");
                        if (message.IsSetMessageId()) 
                        {
                            Console.WriteLine("                    MessageId");
                            Console.WriteLine("                        {0}", message.MessageId);
                        }
                        if (message.IsSetReceiptHandle()) 
                        {
                            Console.WriteLine("                    ReceiptHandle");
                            Console.WriteLine("                        {0}", message.ReceiptHandle);
                        }
                        if (message.IsSetMD5OfBody()) 
                        {
                            Console.WriteLine("                    MD5OfBody");
                            Console.WriteLine("                        {0}", message.MD5OfBody);
                        }
                        if (message.IsSetBody()) 
                        {
                            Console.WriteLine("                    Body");
                            Console.WriteLine("                        {0}", message.Body);
                        }
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
