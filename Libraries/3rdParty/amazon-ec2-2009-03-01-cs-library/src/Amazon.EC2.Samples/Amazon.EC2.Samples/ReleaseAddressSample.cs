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
    /// Release Address  Samples
    /// </summary>
    public class ReleaseAddressSample
    {
    
                                                                                                                                                                         
        /// <summary>
        /// The ReleaseAddress operation releases an elastic IP address associated with
        /// your account.
        /// Note:
        /// Releasing an IP address automatically disassociates it from any instance with
        /// which it is associated. For more information, see DisassociateAddress.
        /// Important:
        /// After releasing an elastic IP address, it is released to the IP address pool
        /// and might no longer be available to your account. Make sure to update your DNS
        /// records and any servers or devices that communicate with the address.
        /// If you run this operation on an elastic IP address that is already released,
        /// the address might be assigned to another account which will cause Amazon EC2 to
        /// return an error.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">ReleaseAddressRequest request</param>
        public static void InvokeReleaseAddress(AmazonEC2 service, ReleaseAddressRequest request)
        {
            try 
            {
                ReleaseAddressResponse response = service.ReleaseAddress(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        ReleaseAddressResponse");
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
