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
    /// Terminate Instances  Samples
    /// </summary>
    public class TerminateInstancesSample
    {
    
                                                                                                                                                                                         
        /// <summary>
        /// The TerminateInstances operation shuts down one or more instances. This
        /// operation is idempotent; if you terminate an instance more than once, each call
        /// will succeed.
        /// Terminated instances will remain visible after termination (approximately one
        /// hour).
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">TerminateInstancesRequest request</param>
        public static void InvokeTerminateInstances(AmazonEC2 service, TerminateInstancesRequest request)
        {
            try 
            {
                TerminateInstancesResponse response = service.TerminateInstances(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        TerminateInstancesResponse");
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
                if (response.IsSetTerminateInstancesResult())
                {
                    Console.WriteLine("            TerminateInstancesResult");
                    TerminateInstancesResult  terminateInstancesResult = response.TerminateInstancesResult;
                    List<TerminatingInstance> terminatingInstanceList = terminateInstancesResult.TerminatingInstance;
                    foreach (TerminatingInstance terminatingInstance in terminatingInstanceList)
                    {
                        Console.WriteLine("                TerminatingInstance");
                        if (terminatingInstance.IsSetInstanceId())
                        {
                            Console.WriteLine("                    InstanceId");
                            Console.WriteLine("                        {0}", terminatingInstance.InstanceId);
                        }
                        if (terminatingInstance.IsSetShutdownState())
                        {
                            Console.WriteLine("                    ShutdownState");
                            InstanceState  shutdownState = terminatingInstance.ShutdownState;
                            if (shutdownState.IsSetCode())
                            {
                                Console.WriteLine("                        Code");
                                Console.WriteLine("                            {0}", shutdownState.Code);
                            }
                            if (shutdownState.IsSetName())
                            {
                                Console.WriteLine("                        Name");
                                Console.WriteLine("                            {0}", shutdownState.Name);
                            }
                        }
                        if (terminatingInstance.IsSetPreviousState())
                        {
                            Console.WriteLine("                    PreviousState");
                            InstanceState  previousState = terminatingInstance.PreviousState;
                            if (previousState.IsSetCode())
                            {
                                Console.WriteLine("                        Code");
                                Console.WriteLine("                            {0}", previousState.Code);
                            }
                            if (previousState.IsSetName())
                            {
                                Console.WriteLine("                        Name");
                                Console.WriteLine("                            {0}", previousState.Name);
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
