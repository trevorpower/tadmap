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
    /// Run Instances  Samples
    /// </summary>
    public class RunInstancesSample
    {
    
                                                                                                                                                                                     
        /// <summary>
        /// The RunInstances operation launches a specified number of instances.
        /// If Amazon EC2 cannot launch the minimum number AMIs you request, no instances
        /// launch. If there is insufficient capacity to launch the maximum number of AMIs
        /// you request, Amazon EC2 launches as many as possible to satisfy the requested
        /// maximum values.
        /// Every instance is launched in a security group. If you do not specify a
        /// security group at launch, the instances start in your default security group.
        /// For more information on creating security groups, see CreateSecurityGroup.
        /// An optional instance type can be specified. For information about instance
        /// types, see Instance Types.
        /// You can provide an optional key pair ID for each image in the launch request
        /// (for more information, see CreateKeyPair). All instances that are created from
        /// images that use this key pair will have access to the associated public key at
        /// boot. You can use this key to provide secure access to an instance of an image
        /// on a per-instance basis. Amazon EC2 public images use this feature to provide
        /// secure access without passwords.
        /// Important:
        /// Launching public images without a key pair ID will leave them inaccessible.
        /// The public key material is made available to the instance at boot time by
        /// placing it in the openssh_id.pub file on a logical device that is exposed to
        /// the instance as /dev/sda2 (the ephemeral store). The format of this file is
        /// suitable for use as an entry within ~/.ssh/authorized_keys (the OpenSSH
        /// format). This can be done at boot (e.g., as part of rc.local) allowing for
        /// secure access without passwords.
        /// Optional user data can be provided in the launch request. All instances that
        /// collectively comprise the launch request have access to this data For more
        /// information, see Instance Metadata.
        /// Note:
        /// If any of the AMIs have a product code attached for which the user has not
        /// subscribed, the RunInstances call will fail.
        /// Important:
        /// We strongly recommend using the 2.6.18 Xen stock kernel with the c1.medium and
        /// c1.xlarge instances. Although the default Amazon EC2 kernels will work, the new
        /// kernels provide greater stability and performance for these instance types. For
        /// more information about kernels, see Kernels, RAM Disks, and Block Device
        /// Mappings.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">RunInstancesRequest request</param>
        public static void InvokeRunInstances(AmazonEC2 service, RunInstancesRequest request)
        {
            try 
            {
                RunInstancesResponse response = service.RunInstances(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        RunInstancesResponse");
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
                if (response.IsSetRunInstancesResult())
                {
                    Console.WriteLine("            RunInstancesResult");
                    RunInstancesResult  runInstancesResult = response.RunInstancesResult;
                    if (runInstancesResult.IsSetReservation())
                    {
                        Console.WriteLine("                Reservation");
                        Reservation  reservation = runInstancesResult.Reservation;
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
