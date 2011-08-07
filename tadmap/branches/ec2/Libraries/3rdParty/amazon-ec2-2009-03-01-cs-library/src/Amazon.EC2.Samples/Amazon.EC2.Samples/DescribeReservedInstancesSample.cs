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
    /// Describe Reserved Instances  Samples
    /// </summary>
    public class DescribeReservedInstancesSample
    {
    
                                                                                                                     
        /// <summary>
        /// The DescribeReservedInstances operation describes Reserved Instances that were purchased for use with your account.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeReservedInstancesRequest request</param>
        public static void InvokeDescribeReservedInstances(AmazonEC2 service, DescribeReservedInstancesRequest request)
        {
            try 
            {
                DescribeReservedInstancesResponse response = service.DescribeReservedInstances(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeReservedInstancesResponse");
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
                if (response.IsSetDescribeReservedInstancesResult())
                {
                    Console.WriteLine("            DescribeReservedInstancesResult");
                    DescribeReservedInstancesResult  describeReservedInstancesResult = response.DescribeReservedInstancesResult;
                    List<ReservedInstances> reservedInstancesList = describeReservedInstancesResult.ReservedInstances;
                    foreach (ReservedInstances reservedInstances in reservedInstancesList)
                    {
                        Console.WriteLine("                ReservedInstances");
                        if (reservedInstances.IsSetReservedInstancesId())
                        {
                            Console.WriteLine("                    ReservedInstancesId");
                            Console.WriteLine("                        {0}", reservedInstances.ReservedInstancesId);
                        }
                        if (reservedInstances.IsSetInstanceType())
                        {
                            Console.WriteLine("                    InstanceType");
                            Console.WriteLine("                        {0}", reservedInstances.InstanceType);
                        }
                        if (reservedInstances.IsSetAvailabilityZone())
                        {
                            Console.WriteLine("                    AvailabilityZone");
                            Console.WriteLine("                        {0}", reservedInstances.AvailabilityZone);
                        }
                        if (reservedInstances.IsSetDuration())
                        {
                            Console.WriteLine("                    Duration");
                            Console.WriteLine("                        {0}", reservedInstances.Duration);
                        }
                        if (reservedInstances.IsSetUsagePrice())
                        {
                            Console.WriteLine("                    UsagePrice");
                            Console.WriteLine("                        {0}", reservedInstances.UsagePrice);
                        }
                        if (reservedInstances.IsSetFixedPrice())
                        {
                            Console.WriteLine("                    FixedPrice");
                            Console.WriteLine("                        {0}", reservedInstances.FixedPrice);
                        }
                        if (reservedInstances.IsSetInstanceCount())
                        {
                            Console.WriteLine("                    InstanceCount");
                            Console.WriteLine("                        {0}", reservedInstances.InstanceCount);
                        }
                        if (reservedInstances.IsSetProductDescription())
                        {
                            Console.WriteLine("                    ProductDescription");
                            Console.WriteLine("                        {0}", reservedInstances.ProductDescription);
                        }
                        if (reservedInstances.IsSetPurchaseState())
                        {
                            Console.WriteLine("                    PurchaseState");
                            Console.WriteLine("                        {0}", reservedInstances.PurchaseState);
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
