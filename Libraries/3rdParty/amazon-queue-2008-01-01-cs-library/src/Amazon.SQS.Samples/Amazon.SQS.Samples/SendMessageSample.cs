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
    /// Send Message  Samples
    /// </summary>
    public class SendMessageSample
    {
    
                                                 
        /// <summary>
        /// The SendMessage action delivers a message to the specified queue.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonSQS service</param>
        /// <param name="request">SendMessageRequest request</param>
        public static void InvokeSendMessage(AmazonSQS service, SendMessageRequest request)
        {
            try 
            {
                SendMessageResponse response = service.SendMessage(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        SendMessageResponse");
                if (response.IsSetSendMessageResult()) 
                {
                    Console.WriteLine("            SendMessageResult");
                    SendMessageResult  sendMessageResult = response.SendMessageResult;
                    if (sendMessageResult.IsSetMessageId()) 
                    {
                        Console.WriteLine("                MessageId");
                        Console.WriteLine("                    {0}", sendMessageResult.MessageId);
                    }
                    if (sendMessageResult.IsSetMD5OfMessageBody()) 
                    {
                        Console.WriteLine("                MD5OfMessageBody");
                        Console.WriteLine("                    {0}", sendMessageResult.MD5OfMessageBody);
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
