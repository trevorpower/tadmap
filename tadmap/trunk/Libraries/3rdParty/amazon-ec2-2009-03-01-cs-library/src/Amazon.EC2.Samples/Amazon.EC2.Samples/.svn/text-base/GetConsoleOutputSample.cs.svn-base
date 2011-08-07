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
    /// Get Console Output  Samples
    /// </summary>
    public class GetConsoleOutputSample
    {
    
                                                                                                                                                     
        /// <summary>
        /// The GetConsoleOutput operation retrieves console output for the specified
        /// instance.
        /// Instance console output is buffered and posted shortly after instance boot,
        /// reboot, and termination. Amazon EC2 preserves the most recent 64 KB output
        /// which will be available for at least one hour after the most recent post.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">GetConsoleOutputRequest request</param>
        public static void InvokeGetConsoleOutput(AmazonEC2 service, GetConsoleOutputRequest request)
        {
            try 
            {
                GetConsoleOutputResponse response = service.GetConsoleOutput(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        GetConsoleOutputResponse");
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
                if (response.IsSetGetConsoleOutputResult())
                {
                    Console.WriteLine("            GetConsoleOutputResult");
                    GetConsoleOutputResult  getConsoleOutputResult = response.GetConsoleOutputResult;
                    if (getConsoleOutputResult.IsSetConsoleOutput())
                    {
                        Console.WriteLine("                ConsoleOutput");
                        ConsoleOutput  consoleOutput = getConsoleOutputResult.ConsoleOutput;
                        if (consoleOutput.IsSetInstanceId())
                        {
                            Console.WriteLine("                    InstanceId");
                            Console.WriteLine("                        {0}", consoleOutput.InstanceId);
                        }
                        if (consoleOutput.IsSetTimestamp())
                        {
                            Console.WriteLine("                    Timestamp");
                            Console.WriteLine("                        {0}", consoleOutput.Timestamp);
                        }
                        if (consoleOutput.IsSetOutput())
                        {
                            Console.WriteLine("                    Output");
                            Console.WriteLine("                        {0}", consoleOutput.Output);
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
