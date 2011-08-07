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
    /// Create Key Pair  Samples
    /// </summary>
    public class CreateKeyPairSample
    {
    
                                                     
        /// <summary>
        /// The CreateKeyPair operation creates a new 2048 bit RSA key pair and returns a
        /// unique ID that can be used to reference this key pair when launching new
        /// instances. For more information, see RunInstances.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">CreateKeyPairRequest request</param>
        public static void InvokeCreateKeyPair(AmazonEC2 service, CreateKeyPairRequest request)
        {
            try 
            {
                CreateKeyPairResponse response = service.CreateKeyPair(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        CreateKeyPairResponse");
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
                if (response.IsSetCreateKeyPairResult())
                {
                    Console.WriteLine("            CreateKeyPairResult");
                    CreateKeyPairResult  createKeyPairResult = response.CreateKeyPairResult;
                    if (createKeyPairResult.IsSetKeyPair())
                    {
                        Console.WriteLine("                KeyPair");
                        KeyPair  keyPair = createKeyPairResult.KeyPair;
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
