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
    /// Describe Reserved Instances Offerings  Samples
    /// </summary>
    public class DescribeReservedInstancesOfferingsSample
    {
    
                                                                                                                         
        /// <summary>
        /// The DescribeReservedInstancesOfferings operation describes Reserved
        /// Instance offerings that are available for purchase. With Amazon EC2
        /// Reserved Instances, you purchase the right to launch Amazon EC2 instances
        /// for a period of time (without getting insufficient capacity errors) and
        /// pay a lower usage rate for the actual time used.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeReservedInstancesOfferingsRequest request</param>
        public static void InvokeDescribeReservedInstancesOfferings(AmazonEC2 service, DescribeReservedInstancesOfferingsRequest request)
        {
            try 
            {
                DescribeReservedInstancesOfferingsResponse response = service.DescribeReservedInstancesOfferings(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeReservedInstancesOfferingsResponse");
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
                if (response.IsSetDescribeReservedInstancesOfferingsResult())
                {
                    Console.WriteLine("            DescribeReservedInstancesOfferingsResult");
                    DescribeReservedInstancesOfferingsResult  describeReservedInstancesOfferingsResult = response.DescribeReservedInstancesOfferingsResult;
                    List<ReservedInstancesOffering> reservedInstancesOfferingList = describeReservedInstancesOfferingsResult.ReservedInstancesOffering;
                    foreach (ReservedInstancesOffering reservedInstancesOffering in reservedInstancesOfferingList)
                    {
                        Console.WriteLine("                ReservedInstancesOffering");
                        if (reservedInstancesOffering.IsSetReservedInstancesOfferingId())
                        {
                            Console.WriteLine("                    ReservedInstancesOfferingId");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.ReservedInstancesOfferingId);
                        }
                        if (reservedInstancesOffering.IsSetInstanceType())
                        {
                            Console.WriteLine("                    InstanceType");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.InstanceType);
                        }
                        if (reservedInstancesOffering.IsSetAvailabilityZone())
                        {
                            Console.WriteLine("                    AvailabilityZone");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.AvailabilityZone);
                        }
                        if (reservedInstancesOffering.IsSetDuration())
                        {
                            Console.WriteLine("                    Duration");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.Duration);
                        }
                        if (reservedInstancesOffering.IsSetUsagePrice())
                        {
                            Console.WriteLine("                    UsagePrice");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.UsagePrice);
                        }
                        if (reservedInstancesOffering.IsSetFixedPrice())
                        {
                            Console.WriteLine("                    FixedPrice");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.FixedPrice);
                        }
                        if (reservedInstancesOffering.IsSetProductDescription())
                        {
                            Console.WriteLine("                    ProductDescription");
                            Console.WriteLine("                        {0}", reservedInstancesOffering.ProductDescription);
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
