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
    /// Describe Regions  Samples
    /// </summary>
    public class DescribeRegionsSample
    {
    
                                                                                                                 
        /// <summary>
        /// The DescribeRegions operation describes regions zones that are currently available to the account.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeRegionsRequest request</param>
        public static void InvokeDescribeRegions(AmazonEC2 service, DescribeRegionsRequest request)
        {
            try 
            {
                DescribeRegionsResponse response = service.DescribeRegions(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeRegionsResponse");
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
                if (response.IsSetDescribeRegionsResult())
                {
                    Console.WriteLine("            DescribeRegionsResult");
                    DescribeRegionsResult  describeRegionsResult = response.DescribeRegionsResult;
                    List<Region> regionList = describeRegionsResult.Region;
                    foreach (Region region in regionList)
                    {
                        Console.WriteLine("                Region");
                        if (region.IsSetRegionName())
                        {
                            Console.WriteLine("                    RegionName");
                            Console.WriteLine("                        {0}", region.RegionName);
                        }
                        if (region.IsSetEndpoint())
                        {
                            Console.WriteLine("                    Endpoint");
                            Console.WriteLine("                        {0}", region.Endpoint);
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
