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
    /// Describe Images  Samples
    /// </summary>
    public class DescribeImagesSample
    {
    
                                                                                                         
        /// <summary>
        /// The DescribeImages operation returns information about AMIs, AKIs, and ARIs
        /// available to the user. Information returned includes image type, product codes,
        /// architecture, and kernel and RAM disk IDs. Images available to the user include
        /// public images available for any user to launch, private images owned by the
        /// user making the request, and private images owned by other users for which the
        /// user has explicit launch permissions.
        /// Launch permissions fall into three categories:
        /// Public:
        /// The owner of the AMI granted launch permissions for the AMI to the all group.
        /// All users have launch permissions for these AMIs.
        /// Explicit:
        /// The owner of the AMI granted launch permissions to a specific user.
        /// Implicit:
        /// A user has implicit launch permissions for all AMIs he or she owns.
        /// The list of AMIs returned can be modified by specifying AMI IDs, AMI owners, or
        /// users with launch permissions. If no options are specified, Amazon EC2 returns
        /// all AMIs for which the user has launch permissions.
        /// If you specify one or more AMI IDs, only AMIs that have the specified IDs are
        /// returned. If you specify an invalid AMI ID, a fault is returned. If you specify
        /// an AMI ID for which you do not have access, it will not be included in the
        /// returned results.
        /// If you specify one or more AMI owners, only AMIs from the specified owners and
        /// for which you have access are returned. The results can include the account IDs
        /// of the specified owners, amazon for AMIs owned by Amazon or self for AMIs that
        /// you own.
        /// If you specify a list of executable users, only users that have launch
        /// permissions for the AMIs are returned. You can specify account IDs (if you own
        /// the AMI(s)), self for AMIs for which you own or have explicit permissions, or
        /// all for public AMIs.
        /// Note:
        /// Deregistered images are included in the returned results for an unspecified
        /// interval after deregistration.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeImagesRequest request</param>
        public static void InvokeDescribeImages(AmazonEC2 service, DescribeImagesRequest request)
        {
            try 
            {
                DescribeImagesResponse response = service.DescribeImages(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeImagesResponse");
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
                if (response.IsSetDescribeImagesResult())
                {
                    Console.WriteLine("            DescribeImagesResult");
                    DescribeImagesResult  describeImagesResult = response.DescribeImagesResult;
                    List<Image> imageList = describeImagesResult.Image;
                    foreach (Image image in imageList)
                    {
                        Console.WriteLine("                Image");
                        if (image.IsSetImageId())
                        {
                            Console.WriteLine("                    ImageId");
                            Console.WriteLine("                        {0}", image.ImageId);
                        }
                        if (image.IsSetImageLocation())
                        {
                            Console.WriteLine("                    ImageLocation");
                            Console.WriteLine("                        {0}", image.ImageLocation);
                        }
                        if (image.IsSetImageState())
                        {
                            Console.WriteLine("                    ImageState");
                            Console.WriteLine("                        {0}", image.ImageState);
                        }
                        if (image.IsSetOwnerId())
                        {
                            Console.WriteLine("                    OwnerId");
                            Console.WriteLine("                        {0}", image.OwnerId);
                        }
                        if (image.IsSetVisibility())
                        {
                            Console.WriteLine("                    Visibility");
                            Console.WriteLine("                        {0}", image.Visibility);
                        }
                        List<String> productCodeList  =  image.ProductCode;
                        foreach (String productCode in productCodeList)
                        {
                            Console.WriteLine("                    ProductCode");
                            Console.WriteLine("                        {0}", productCode);
                        }
                        if (image.IsSetArchitecture())
                        {
                            Console.WriteLine("                    Architecture");
                            Console.WriteLine("                        {0}", image.Architecture);
                        }
                        if (image.IsSetImageType())
                        {
                            Console.WriteLine("                    ImageType");
                            Console.WriteLine("                        {0}", image.ImageType);
                        }
                        if (image.IsSetKernelId())
                        {
                            Console.WriteLine("                    KernelId");
                            Console.WriteLine("                        {0}", image.KernelId);
                        }
                        if (image.IsSetRamdiskId())
                        {
                            Console.WriteLine("                    RamdiskId");
                            Console.WriteLine("                        {0}", image.RamdiskId);
                        }
                        if (image.IsSetPlatform())
                        {
                            Console.WriteLine("                    Platform");
                            Console.WriteLine("                        {0}", image.Platform);
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
