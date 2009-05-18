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
using Amazon.SQS.Mock;
using Amazon.SQS.Model;

namespace Amazon.SQS.Samples
{

    /// <summary>
    /// Amazon SQS  Samples
    /// </summary>
    public class AmazonSQSSamples 
    {
    
       /**
        * Samples for Amazon SQS functionality
        */
        public static void Main(string [] args) 
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Welcome to Amazon SQS Samples!");
            Console.WriteLine("===========================================");

            Console.WriteLine("To get started:");
            Console.WriteLine("===========================================");
            Console.WriteLine("  - Fill in your AWS credentials");
            Console.WriteLine("  - Uncomment sample you're interested in trying");
            Console.WriteLine("  - Set request with desired parameters");
            Console.WriteLine("  - Hit F5 to run!");
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Samples Output");
            Console.WriteLine("===========================================");
            Console.WriteLine();

           /************************************************************************
            * Access Key ID and Secret Acess Key ID, obtained from:
            * http://aws.amazon.com
            ***********************************************************************/
            String accessKeyId = "<Your Access Key ID>";
            String secretAccessKey = "<Your Secret Access Key>";
        
            /************************************************************************
            * Instantiate  Implementation of Amazon SQS 
            ***********************************************************************/
            AmazonSQS service = new AmazonSQSClient(accessKeyId, secretAccessKey);
        
            /************************************************************************
            * Uncomment to try advanced configuration options. Available options are:
            *
            *  - Signature Version
            *  - Proxy Host and Proxy Port
            *  - Service URL
            *  - User Agent String to be sent to Amazon SQS  service
            *
            ***********************************************************************/
            // AmazonSQSConfig config = new AmazonSQSConfig();
            // config.SignatureVersion = "0";
            // AmazonSQS service = new AmazonSQSClient(accessKeyId, secretAccessKey, config);
        
        
            /************************************************************************
            * Uncomment to try out Mock Service that simulates Amazon SQS 
            * responses without calling Amazon SQS  service.
            *
            * Responses are loaded from local XML files. You can tweak XML files to
            * experiment with various outputs during development
            *
            * XML files available under Amazon\SQS\Mock tree
            *
            ***********************************************************************/
            // AmazonSQS service = new AmazonSQSMock();


            /************************************************************************
            * Uncomment to invoke Create Queue Action
            ***********************************************************************/
            // CreateQueueRequest request = new CreateQueueRequest();
            // @TODO: set request parameters here
            // CreateQueueSample.InvokeCreateQueue(service, request);
            /************************************************************************
            * Uncomment to invoke List Queues Action
            ***********************************************************************/
            // ListQueuesRequest request = new ListQueuesRequest();
            // @TODO: set request parameters here
            // ListQueuesSample.InvokeListQueues(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Message Action
            ***********************************************************************/
            // DeleteMessageRequest request = new DeleteMessageRequest();
            // @TODO: set request parameters here
            // DeleteMessageSample.InvokeDeleteMessage(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Queue Action
            ***********************************************************************/
            // DeleteQueueRequest request = new DeleteQueueRequest();
            // @TODO: set request parameters here
            // DeleteQueueSample.InvokeDeleteQueue(service, request);
            /************************************************************************
            * Uncomment to invoke Get Queue Attributes Action
            ***********************************************************************/
            // GetQueueAttributesRequest request = new GetQueueAttributesRequest();
            // @TODO: set request parameters here
            // GetQueueAttributesSample.InvokeGetQueueAttributes(service, request);
            /************************************************************************
            * Uncomment to invoke Receive Message Action
            ***********************************************************************/
            // ReceiveMessageRequest request = new ReceiveMessageRequest();
            // @TODO: set request parameters here
            // ReceiveMessageSample.InvokeReceiveMessage(service, request);
            /************************************************************************
            * Uncomment to invoke Send Message Action
            ***********************************************************************/
            // SendMessageRequest request = new SendMessageRequest();
            // @TODO: set request parameters here
            // SendMessageSample.InvokeSendMessage(service, request);
            /************************************************************************
            * Uncomment to invoke Set Queue Attributes Action
            ***********************************************************************/
            // SetQueueAttributesRequest request = new SetQueueAttributesRequest();
            // @TODO: set request parameters here
            // SetQueueAttributesSample.InvokeSetQueueAttributes(service, request);
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine("End of output. You can close this window");
            Console.WriteLine("===========================================");

            System.Threading.Thread.Sleep(50000);
        }

    }
}
