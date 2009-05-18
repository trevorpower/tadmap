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
    /// Describe Instances  Samples
    /// </summary>
    public class DescribeInstancesSample
    {
    
                                                                                                             
        /// <summary>
        /// The DescribeInstances operation returns information about instances that you
        /// own.
        /// If you specify one or more instance IDs, Amazon EC2 returns information for
        /// those instances. If you do not specify instance IDs, Amazon EC2 returns
        /// information for all relevant instances. If you specify an invalid instance ID,
        /// a fault is returned. If you specify an instance that you do not own, it will
        /// not be included in the returned results.
        /// Recently terminated instances might appear in the returned results. This
        /// interval is usually less than one hour.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeInstancesRequest request</param>
        public static void InvokeDescribeInstances(AmazonEC2 service, DescribeInstancesRequest request)
        {
            try 
            {
                DescribeInstancesResponse response = service.DescribeInstances(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeInstancesResponse");
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
                if (response.IsSetDescribeInstancesResult())
                {
                    Console.WriteLine("            DescribeInstancesResult");
                    DescribeInstancesResult  describeInstancesResult = response.DescribeInstancesResult;
                    List<Reservation> reservationList = describeInstancesResult.Reservation;
                    foreach (Reservation reservation in reservationList)
                    {
                        Console.WriteLine("                Reservation");
                        if (reservation.IsSetReservationId())
                        {
                            Console.WriteLine("                    ReservationId");
                            Console.WriteLine("                        {0}", reservation.ReservationId);
                        }
                        if (reservation.IsSetOwnerId())
                        {
                            Console.WriteLine("                    OwnerId");
                            Console.WriteLine("                        {0}", reservation.OwnerId);
                        }
                        if (reservation.IsSetRequesterId())
                        {
                            Console.WriteLine("                    RequesterId");
                            Console.WriteLine("                        {0}", reservation.RequesterId);
                        }
                        List<String> groupNameList  =  reservation.GroupName;
                        foreach (String groupName in groupNameList)
                        {
                            Console.WriteLine("                    GroupName");
                            Console.WriteLine("                        {0}", groupName);
                        }
                        List<RunningInstance> runningInstanceList = reservation.RunningInstance;
                        foreach (RunningInstance runningInstance in runningInstanceList)
                        {
                            Console.WriteLine("                    RunningInstance");
                            if (runningInstance.IsSetInstanceId())
                            {
                                Console.WriteLine("                        InstanceId");
                                Console.WriteLine("                            {0}", runningInstance.InstanceId);
                            }
                            if (runningInstance.IsSetImageId())
                            {
                                Console.WriteLine("                        ImageId");
                                Console.WriteLine("                            {0}", runningInstance.ImageId);
                            }
                            if (runningInstance.IsSetInstanceState())
                            {
                                Console.WriteLine("                        InstanceState");
                                InstanceState  instanceState = runningInstance.InstanceState;
                                if (instanceState.IsSetCode())
                                {
                                    Console.WriteLine("                            Code");
                                    Console.WriteLine("                                {0}", instanceState.Code);
                                }
                                if (instanceState.IsSetName())
                                {
                                    Console.WriteLine("                            Name");
                                    Console.WriteLine("                                {0}", instanceState.Name);
                                }
                            }
                            if (runningInstance.IsSetPrivateDnsName())
                            {
                                Console.WriteLine("                        PrivateDnsName");
                                Console.WriteLine("                            {0}", runningInstance.PrivateDnsName);
                            }
                            if (runningInstance.IsSetPublicDnsName())
                            {
                                Console.WriteLine("                        PublicDnsName");
                                Console.WriteLine("                            {0}", runningInstance.PublicDnsName);
                            }
                            if (runningInstance.IsSetStateTransitionReason())
                            {
                                Console.WriteLine("                        StateTransitionReason");
                                Console.WriteLine("                            {0}", runningInstance.StateTransitionReason);
                            }
                            if (runningInstance.IsSetKeyName())
                            {
                                Console.WriteLine("                        KeyName");
                                Console.WriteLine("                            {0}", runningInstance.KeyName);
                            }
                            if (runningInstance.IsSetAmiLaunchIndex())
                            {
                                Console.WriteLine("                        AmiLaunchIndex");
                                Console.WriteLine("                            {0}", runningInstance.AmiLaunchIndex);
                            }
                            List<String> productCodeList  =  runningInstance.ProductCode;
                            foreach (String productCode in productCodeList)
                            {
                                Console.WriteLine("                        ProductCode");
                                Console.WriteLine("                            {0}", productCode);
                            }
                            if (runningInstance.IsSetInstanceType())
                            {
                                Console.WriteLine("                        InstanceType");
                                Console.WriteLine("                            {0}", runningInstance.InstanceType);
                            }
                            if (runningInstance.IsSetLaunchTime())
                            {
                                Console.WriteLine("                        LaunchTime");
                                Console.WriteLine("                            {0}", runningInstance.LaunchTime);
                            }
                            if (runningInstance.IsSetPlacement())
                            {
                                Console.WriteLine("                        Placement");
                                Placement  placement = runningInstance.Placement;
                                if (placement.IsSetAvailabilityZone())
                                {
                                    Console.WriteLine("                            AvailabilityZone");
                                    Console.WriteLine("                                {0}", placement.AvailabilityZone);
                                }
                            }
                            if (runningInstance.IsSetKernelId())
                            {
                                Console.WriteLine("                        KernelId");
                                Console.WriteLine("                            {0}", runningInstance.KernelId);
                            }
                            if (runningInstance.IsSetRamdiskId())
                            {
                                Console.WriteLine("                        RamdiskId");
                                Console.WriteLine("                            {0}", runningInstance.RamdiskId);
                            }
                            if (runningInstance.IsSetPlatform())
                            {
                                Console.WriteLine("                        Platform");
                                Console.WriteLine("                            {0}", runningInstance.Platform);
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
