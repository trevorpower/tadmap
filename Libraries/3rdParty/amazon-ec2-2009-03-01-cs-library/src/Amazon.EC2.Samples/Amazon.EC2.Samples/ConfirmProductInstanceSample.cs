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
    /// Confirm Product Instance  Samples
    /// </summary>
    public class ConfirmProductInstanceSample
    {
    
                                                 
        /// <summary>
        /// The ConfirmProductInstance operation returns true if the specified product code
        /// is attached to the specified instance. The operation returns false if the
        /// product code is not attached to the instance.
        /// The ConfirmProductInstance operation can only be executed by the owner of the
        /// AMI. This feature is useful when an AMI owner is providing support and wants to
        /// verify whether a user's instance is eligible.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">ConfirmProductInstanceRequest request</param>
        public static void InvokeConfirmProductInstance(AmazonEC2 service, ConfirmProductInstanceRequest request)
        {
            try 
            {
                ConfirmProductInstanceResponse response = service.ConfirmProductInstance(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        ConfirmProductInstanceResponse");
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
                if (response.IsSetConfirmProductInstanceResult())
                {
                    Console.WriteLine("            ConfirmProductInstanceResult");
                    ConfirmProductInstanceResult  confirmProductInstanceResult = response.ConfirmProductInstanceResult;
                    if (confirmProductInstanceResult.IsSetOwnerId())
                    {
                        Console.WriteLine("                OwnerId");
                        Console.WriteLine("                    {0}", confirmProductInstanceResult.OwnerId);
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
