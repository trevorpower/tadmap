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
    /// Describe Key Pairs  Samples
    /// </summary>
    public class DescribeKeyPairsSample
    {
    
                                                                                                                             
        /// <summary>
        /// The DescribeKeyPairs operation returns information about key pairs available to
        /// you. If you specify key pairs, information about those key pairs is returned.
        /// Otherwise, information for all registered key pairs is returned.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeKeyPairsRequest request</param>
        public static void InvokeDescribeKeyPairs(AmazonEC2 service, DescribeKeyPairsRequest request)
        {
            try 
            {
                DescribeKeyPairsResponse response = service.DescribeKeyPairs(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeKeyPairsResponse");
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
                if (response.IsSetDescribeKeyPairsResult())
                {
                    Console.WriteLine("            DescribeKeyPairsResult");
                    DescribeKeyPairsResult  describeKeyPairsResult = response.DescribeKeyPairsResult;
                    List<KeyPair> keyPairList = describeKeyPairsResult.KeyPair;
                    foreach (KeyPair keyPair in keyPairList)
                    {
                        Console.WriteLine("                KeyPair");
                        if (keyPair.IsSetKeyName())
                        {
                            Console.WriteLine("                    KeyName");
                            Console.WriteLine("                        {0}", keyPair.KeyName);
                        }
                        if (keyPair.IsSetKeyFingerprint())
                        {
                            Console.WriteLine("                    KeyFingerprint");
                            Console.WriteLine("                        {0}", keyPair.KeyFingerprint);
                        }
                        if (keyPair.IsSetKeyMaterial())
                        {
                            Console.WriteLine("                    KeyMaterial");
                            Console.WriteLine("                        {0}", keyPair.KeyMaterial);
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
