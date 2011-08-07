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
    /// Purchase Reserved Instances Offering  Samples
    /// </summary>
    public class PurchaseReservedInstancesOfferingSample
    {
    
                                                                                                                                                             
        /// <summary>
        /// The PurchaseReservedInstancesOffering operation purchases a
        /// Reserved Instance for use with your account. With Amazon EC2
        /// Reserved Instances, you purchase the right to launch Amazon EC2
        /// instances for a period of time (without getting insufficient
        /// capacity errors) and pay a lower usage rate for the
        /// actual time used.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">PurchaseReservedInstancesOfferingRequest request</param>
        public static void InvokePurchaseReservedInstancesOffering(AmazonEC2 service, PurchaseReservedInstancesOfferingRequest request)
        {
            try 
            {
                PurchaseReservedInstancesOfferingResponse response = service.PurchaseReservedInstancesOffering(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        PurchaseReservedInstancesOfferingResponse");
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
                if (response.IsSetPurchaseReservedInstancesOfferingResult())
                {
                    Console.WriteLine("            PurchaseReservedInstancesOfferingResult");
                    PurchaseReservedInstancesOfferingResult  purchaseReservedInstancesOfferingResult = response.PurchaseReservedInstancesOfferingResult;
                    if (purchaseReservedInstancesOfferingResult.IsSetReservedInstancesId())
                    {
                        Console.WriteLine("                ReservedInstancesId");
                        Console.WriteLine("                    {0}", purchaseReservedInstancesOfferingResult.ReservedInstancesId);
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
