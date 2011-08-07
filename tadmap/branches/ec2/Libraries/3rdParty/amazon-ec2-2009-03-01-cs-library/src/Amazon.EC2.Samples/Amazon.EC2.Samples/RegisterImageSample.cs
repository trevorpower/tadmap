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
    /// Register Image  Samples
    /// </summary>
    public class RegisterImageSample
    {
    
                                                                                                                                                                     
        /// <summary>
        /// The RegisterImage operation registers an AMI with Amazon EC2. Images must be
        /// registered before they can be launched. For more information, see RunInstances.
        /// Each AMI is associated with an unique ID which is provided by the Amazon EC2
        /// service through the RegisterImage operation. During registration, Amazon EC2
        /// retrieves the specified image manifest from Amazon S3 and verifies that the
        /// image is owned by the user registering the image.
        /// The image manifest is retrieved once and stored within the Amazon EC2. Any
        /// modifications to an image in Amazon S3 invalidates this registration. If you
        /// make changes to an image, deregister the previous image and register the new
        /// image. For more information, see DeregisterImage.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">RegisterImageRequest request</param>
        public static void InvokeRegisterImage(AmazonEC2 service, RegisterImageRequest request)
        {
            try 
            {
                RegisterImageResponse response = service.RegisterImage(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        RegisterImageResponse");
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
                if (response.IsSetRegisterImageResult())
                {
                    Console.WriteLine("            RegisterImageResult");
                    RegisterImageResult  registerImageResult = response.RegisterImageResult;
                    if (registerImageResult.IsSetImageId())
                    {
                        Console.WriteLine("                ImageId");
                        Console.WriteLine("                    {0}", registerImageResult.ImageId);
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
