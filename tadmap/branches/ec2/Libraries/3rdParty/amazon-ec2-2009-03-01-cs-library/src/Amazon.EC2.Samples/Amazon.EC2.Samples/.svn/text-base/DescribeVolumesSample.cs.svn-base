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
    /// Describe Volumes  Samples
    /// </summary>
    public class DescribeVolumesSample
    {
    
                                                                                                                                         
        /// <summary>
        /// Describes the status of the indicated or, in lieu of any specified,  all volumes belonging to the caller. Volumes that have been deleted are not described.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeVolumesRequest request</param>
        public static void InvokeDescribeVolumes(AmazonEC2 service, DescribeVolumesRequest request)
        {
            try 
            {
                DescribeVolumesResponse response = service.DescribeVolumes(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeVolumesResponse");
                if (response.IsSetDescribeVolumesResult())
                {
                    Console.WriteLine("            DescribeVolumesResult");
                    DescribeVolumesResult  describeVolumesResult = response.DescribeVolumesResult;
                    List<Volume> volumeList = describeVolumesResult.Volume;
                    foreach (Volume volume in volumeList)
                    {
                        Console.WriteLine("                Volume");
                        if (volume.IsSetVolumeId())
                        {
                            Console.WriteLine("                    VolumeId");
                            Console.WriteLine("                        {0}", volume.VolumeId);
                        }
                        if (volume.IsSetSize())
                        {
                            Console.WriteLine("                    Size");
                            Console.WriteLine("                        {0}", volume.Size);
                        }
                        if (volume.IsSetSnapshotId())
                        {
                            Console.WriteLine("                    SnapshotId");
                            Console.WriteLine("                        {0}", volume.SnapshotId);
                        }
                        if (volume.IsSetAvailabilityZone())
                        {
                            Console.WriteLine("                    AvailabilityZone");
                            Console.WriteLine("                        {0}", volume.AvailabilityZone);
                        }
                        if (volume.IsSetStatus())
                        {
                            Console.WriteLine("                    Status");
                            Console.WriteLine("                        {0}", volume.Status);
                        }
                        if (volume.IsSetCreateTime())
                        {
                            Console.WriteLine("                    CreateTime");
                            Console.WriteLine("                        {0}", volume.CreateTime);
                        }
                        List<Attachment> attachmentList = volume.Attachment;
                        foreach (Attachment attachment in attachmentList)
                        {
                            Console.WriteLine("                    Attachment");
                            if (attachment.IsSetVolumeId())
                            {
                                Console.WriteLine("                        VolumeId");
                                Console.WriteLine("                            {0}", attachment.VolumeId);
                            }
                            if (attachment.IsSetInstanceId())
                            {
                                Console.WriteLine("                        InstanceId");
                                Console.WriteLine("                            {0}", attachment.InstanceId);
                            }
                            if (attachment.IsSetDevice())
                            {
                                Console.WriteLine("                        Device");
                                Console.WriteLine("                            {0}", attachment.Device);
                            }
                            if (attachment.IsSetStatus())
                            {
                                Console.WriteLine("                        Status");
                                Console.WriteLine("                            {0}", attachment.Status);
                            }
                            if (attachment.IsSetAttachTime())
                            {
                                Console.WriteLine("                        AttachTime");
                                Console.WriteLine("                            {0}", attachment.AttachTime);
                            }
                        }
                    }
                }
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
