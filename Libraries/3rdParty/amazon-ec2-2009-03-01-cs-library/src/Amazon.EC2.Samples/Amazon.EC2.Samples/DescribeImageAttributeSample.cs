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
    /// Describe Image Attribute  Samples
    /// </summary>
    public class DescribeImageAttributeSample
    {
    
                                                                                                     
        /// <summary>
        /// The DescribeImageAttribute operation returns information about an attribute of
        /// an AMI. Only one attribute can be specified per call.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeImageAttributeRequest request</param>
        public static void InvokeDescribeImageAttribute(AmazonEC2 service, DescribeImageAttributeRequest request)
        {
            try 
            {
                DescribeImageAttributeResponse response = service.DescribeImageAttribute(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeImageAttributeResponse");
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
                if (response.IsSetDescribeImageAttributeResult())
                {
                    Console.WriteLine("            DescribeImageAttributeResult");
                    DescribeImageAttributeResult  describeImageAttributeResult = response.DescribeImageAttributeResult;
                    List<ImageAttribute> imageAttributeList = describeImageAttributeResult.ImageAttribute;
                    foreach (ImageAttribute imageAttribute in imageAttributeList)
                    {
                        Console.WriteLine("                ImageAttribute");
                        if (imageAttribute.IsSetImageId())
                        {
                            Console.WriteLine("                    ImageId");
                            Console.WriteLine("                        {0}", imageAttribute.ImageId);
                        }
                        List<LaunchPermission> launchPermissionList = imageAttribute.LaunchPermission;
                        foreach (LaunchPermission launchPermission in launchPermissionList)
                        {
                            Console.WriteLine("                    LaunchPermission");
                            if (launchPermission.IsSetUserId())
                            {
                                Console.WriteLine("                        UserId");
                                Console.WriteLine("                            {0}", launchPermission.UserId);
                            }
                            if (launchPermission.IsSetGroupName())
                            {
                                Console.WriteLine("                        GroupName");
                                Console.WriteLine("                            {0}", launchPermission.GroupName);
                            }
                        }
                        List<String> productCodeList  =  imageAttribute.ProductCode;
                        foreach (String productCode in productCodeList)
                        {
                            Console.WriteLine("                    ProductCode");
                            Console.WriteLine("                        {0}", productCode);
                        }
                        if (imageAttribute.IsSetKernelId())
                        {
                            Console.WriteLine("                    KernelId");
                            Console.WriteLine("                        {0}", imageAttribute.KernelId);
                        }
                        if (imageAttribute.IsSetRamdiskId())
                        {
                            Console.WriteLine("                    RamdiskId");
                            Console.WriteLine("                        {0}", imageAttribute.RamdiskId);
                        }
                        if (imageAttribute.IsSetBlockDeviceMapping())
                        {
                            Console.WriteLine("                    BlockDeviceMapping");
                            BlockDeviceMapping  blockDeviceMapping = imageAttribute.BlockDeviceMapping;
                            if (blockDeviceMapping.IsSetVirtualName())
                            {
                                Console.WriteLine("                        VirtualName");
                                Console.WriteLine("                            {0}", blockDeviceMapping.VirtualName);
                            }
                            if (blockDeviceMapping.IsSetDeviceName())
                            {
                                Console.WriteLine("                        DeviceName");
                                Console.WriteLine("                            {0}", blockDeviceMapping.DeviceName);
                            }
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
