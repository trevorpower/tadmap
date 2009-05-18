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
using Amazon.EC2.Mock;
using Amazon.EC2.Model;

namespace Amazon.EC2.Samples
{

    /// <summary>
    /// Amazon EC2  Samples
    /// </summary>
    public class AmazonEC2Samples 
    {
    
       /**
        * Samples for Amazon EC2 functionality
        */
        public static void Main(string [] args) 
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Welcome to Amazon EC2 Samples!");
            Console.WriteLine("===========================================");

            Console.WriteLine("To get started:");
            Console.WriteLine("===========================================");
            Console.WriteLine("  - Fill in your AWS credentials");
            Console.WriteLine("  - Uncomment sample you're interested in trying");
            Console.WriteLine("  - Set request with desired parameters");
            Console.WriteLine("  - Hit F5 to run!");
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Samples Output");
            Console.WriteLine("===========================================");
            Console.WriteLine();

           /************************************************************************
            * Access Key ID and Secret Acess Key ID, obtained from:
            * http://aws.amazon.com
            ***********************************************************************/
            String accessKeyId = "<Your Access Key ID>";
            String secretAccessKey = "<Your Secret Access Key>";
        
            /************************************************************************
            * Instantiate  Implementation of Amazon EC2 
            ***********************************************************************/
            AmazonEC2 service = new AmazonEC2Client(accessKeyId, secretAccessKey);
        
            /************************************************************************
            * Uncomment to try advanced configuration options. Available options are:
            *
            *  - Signature Version
            *  - Proxy Host and Proxy Port
            *  - Service URL
            *  - User Agent String to be sent to Amazon EC2  service
            *
            ***********************************************************************/
            // AmazonEC2Config config = new AmazonEC2Config();
            // config.SignatureVersion = "0";
            // AmazonEC2 service = new AmazonEC2Client(accessKeyId, secretAccessKey, config);
        
        
            /************************************************************************
            * Uncomment to try out Mock Service that simulates Amazon EC2 
            * responses without calling Amazon EC2  service.
            *
            * Responses are loaded from local XML files. You can tweak XML files to
            * experiment with various outputs during development
            *
            * XML files available under Amazon\EC2\Mock tree
            *
            ***********************************************************************/
            // AmazonEC2 service = new AmazonEC2Mock();


            /************************************************************************
            * Uncomment to invoke Allocate Address Action
            ***********************************************************************/
            // AllocateAddressRequest request = new AllocateAddressRequest();
            // @TODO: set request parameters here
            // AllocateAddressSample.InvokeAllocateAddress(service, request);
            /************************************************************************
            * Uncomment to invoke Associate Address Action
            ***********************************************************************/
            // AssociateAddressRequest request = new AssociateAddressRequest();
            // @TODO: set request parameters here
            // AssociateAddressSample.InvokeAssociateAddress(service, request);
            /************************************************************************
            * Uncomment to invoke Attach Volume Action
            ***********************************************************************/
            // AttachVolumeRequest request = new AttachVolumeRequest();
            // @TODO: set request parameters here
            // AttachVolumeSample.InvokeAttachVolume(service, request);
            /************************************************************************
            * Uncomment to invoke Authorize Security Group Ingress Action
            ***********************************************************************/
            // AuthorizeSecurityGroupIngressRequest request = new AuthorizeSecurityGroupIngressRequest();
            // @TODO: set request parameters here
            // AuthorizeSecurityGroupIngressSample.InvokeAuthorizeSecurityGroupIngress(service, request);
            /************************************************************************
            * Uncomment to invoke Bundle Instance Action
            ***********************************************************************/
            // BundleInstanceRequest request = new BundleInstanceRequest();
            // @TODO: set request parameters here
            // BundleInstanceSample.InvokeBundleInstance(service, request);
            /************************************************************************
            * Uncomment to invoke Cancel Bundle Task Action
            ***********************************************************************/
            // CancelBundleTaskRequest request = new CancelBundleTaskRequest();
            // @TODO: set request parameters here
            // CancelBundleTaskSample.InvokeCancelBundleTask(service, request);
            /************************************************************************
            * Uncomment to invoke Confirm Product Instance Action
            ***********************************************************************/
            // ConfirmProductInstanceRequest request = new ConfirmProductInstanceRequest();
            // @TODO: set request parameters here
            // ConfirmProductInstanceSample.InvokeConfirmProductInstance(service, request);
            /************************************************************************
            * Uncomment to invoke Create Key Pair Action
            ***********************************************************************/
            // CreateKeyPairRequest request = new CreateKeyPairRequest();
            // @TODO: set request parameters here
            // CreateKeyPairSample.InvokeCreateKeyPair(service, request);
            /************************************************************************
            * Uncomment to invoke Create Security Group Action
            ***********************************************************************/
            // CreateSecurityGroupRequest request = new CreateSecurityGroupRequest();
            // @TODO: set request parameters here
            // CreateSecurityGroupSample.InvokeCreateSecurityGroup(service, request);
            /************************************************************************
            * Uncomment to invoke Create Snapshot Action
            ***********************************************************************/
            // CreateSnapshotRequest request = new CreateSnapshotRequest();
            // @TODO: set request parameters here
            // CreateSnapshotSample.InvokeCreateSnapshot(service, request);
            /************************************************************************
            * Uncomment to invoke Create Volume Action
            ***********************************************************************/
            // CreateVolumeRequest request = new CreateVolumeRequest();
            // @TODO: set request parameters here
            // CreateVolumeSample.InvokeCreateVolume(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Key Pair Action
            ***********************************************************************/
            // DeleteKeyPairRequest request = new DeleteKeyPairRequest();
            // @TODO: set request parameters here
            // DeleteKeyPairSample.InvokeDeleteKeyPair(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Security Group Action
            ***********************************************************************/
            // DeleteSecurityGroupRequest request = new DeleteSecurityGroupRequest();
            // @TODO: set request parameters here
            // DeleteSecurityGroupSample.InvokeDeleteSecurityGroup(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Snapshot Action
            ***********************************************************************/
            // DeleteSnapshotRequest request = new DeleteSnapshotRequest();
            // @TODO: set request parameters here
            // DeleteSnapshotSample.InvokeDeleteSnapshot(service, request);
            /************************************************************************
            * Uncomment to invoke Delete Volume Action
            ***********************************************************************/
            // DeleteVolumeRequest request = new DeleteVolumeRequest();
            // @TODO: set request parameters here
            // DeleteVolumeSample.InvokeDeleteVolume(service, request);
            /************************************************************************
            * Uncomment to invoke Deregister Image Action
            ***********************************************************************/
            // DeregisterImageRequest request = new DeregisterImageRequest();
            // @TODO: set request parameters here
            // DeregisterImageSample.InvokeDeregisterImage(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Addresses Action
            ***********************************************************************/
            // DescribeAddressesRequest request = new DescribeAddressesRequest();
            // @TODO: set request parameters here
            // DescribeAddressesSample.InvokeDescribeAddresses(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Availability Zones Action
            ***********************************************************************/
            // DescribeAvailabilityZonesRequest request = new DescribeAvailabilityZonesRequest();
            // @TODO: set request parameters here
            // DescribeAvailabilityZonesSample.InvokeDescribeAvailabilityZones(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Bundle Tasks Action
            ***********************************************************************/
            // DescribeBundleTasksRequest request = new DescribeBundleTasksRequest();
            // @TODO: set request parameters here
            // DescribeBundleTasksSample.InvokeDescribeBundleTasks(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Image Attribute Action
            ***********************************************************************/
            // DescribeImageAttributeRequest request = new DescribeImageAttributeRequest();
            // @TODO: set request parameters here
            // DescribeImageAttributeSample.InvokeDescribeImageAttribute(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Images Action
            ***********************************************************************/
            // DescribeImagesRequest request = new DescribeImagesRequest();
            // @TODO: set request parameters here
            // DescribeImagesSample.InvokeDescribeImages(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Instances Action
            ***********************************************************************/
            // DescribeInstancesRequest request = new DescribeInstancesRequest();
            // @TODO: set request parameters here
            // DescribeInstancesSample.InvokeDescribeInstances(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Regions Action
            ***********************************************************************/
            // DescribeRegionsRequest request = new DescribeRegionsRequest();
            // @TODO: set request parameters here
            // DescribeRegionsSample.InvokeDescribeRegions(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Reserved Instances Action
            ***********************************************************************/
            // DescribeReservedInstancesRequest request = new DescribeReservedInstancesRequest();
            // @TODO: set request parameters here
            // DescribeReservedInstancesSample.InvokeDescribeReservedInstances(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Reserved Instances Offerings Action
            ***********************************************************************/
            // DescribeReservedInstancesOfferingsRequest request = new DescribeReservedInstancesOfferingsRequest();
            // @TODO: set request parameters here
            // DescribeReservedInstancesOfferingsSample.InvokeDescribeReservedInstancesOfferings(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Key Pairs Action
            ***********************************************************************/
            // DescribeKeyPairsRequest request = new DescribeKeyPairsRequest();
            // @TODO: set request parameters here
            // DescribeKeyPairsSample.InvokeDescribeKeyPairs(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Security Groups Action
            ***********************************************************************/
            // DescribeSecurityGroupsRequest request = new DescribeSecurityGroupsRequest();
            // @TODO: set request parameters here
            // DescribeSecurityGroupsSample.InvokeDescribeSecurityGroups(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Snapshots Action
            ***********************************************************************/
            // DescribeSnapshotsRequest request = new DescribeSnapshotsRequest();
            // @TODO: set request parameters here
            // DescribeSnapshotsSample.InvokeDescribeSnapshots(service, request);
            /************************************************************************
            * Uncomment to invoke Describe Volumes Action
            ***********************************************************************/
            // DescribeVolumesRequest request = new DescribeVolumesRequest();
            // @TODO: set request parameters here
            // DescribeVolumesSample.InvokeDescribeVolumes(service, request);
            /************************************************************************
            * Uncomment to invoke Detach Volume Action
            ***********************************************************************/
            // DetachVolumeRequest request = new DetachVolumeRequest();
            // @TODO: set request parameters here
            // DetachVolumeSample.InvokeDetachVolume(service, request);
            /************************************************************************
            * Uncomment to invoke Disassociate Address Action
            ***********************************************************************/
            // DisassociateAddressRequest request = new DisassociateAddressRequest();
            // @TODO: set request parameters here
            // DisassociateAddressSample.InvokeDisassociateAddress(service, request);
            /************************************************************************
            * Uncomment to invoke Get Console Output Action
            ***********************************************************************/
            // GetConsoleOutputRequest request = new GetConsoleOutputRequest();
            // @TODO: set request parameters here
            // GetConsoleOutputSample.InvokeGetConsoleOutput(service, request);
            /************************************************************************
            * Uncomment to invoke Modify Image Attribute Action
            ***********************************************************************/
            // ModifyImageAttributeRequest request = new ModifyImageAttributeRequest();
            // @TODO: set request parameters here
            // ModifyImageAttributeSample.InvokeModifyImageAttribute(service, request);
            /************************************************************************
            * Uncomment to invoke Purchase Reserved Instances Offering Action
            ***********************************************************************/
            // PurchaseReservedInstancesOfferingRequest request = new PurchaseReservedInstancesOfferingRequest();
            // @TODO: set request parameters here
            // PurchaseReservedInstancesOfferingSample.InvokePurchaseReservedInstancesOffering(service, request);
            /************************************************************************
            * Uncomment to invoke Reboot Instances Action
            ***********************************************************************/
            // RebootInstancesRequest request = new RebootInstancesRequest();
            // @TODO: set request parameters here
            // RebootInstancesSample.InvokeRebootInstances(service, request);
            /************************************************************************
            * Uncomment to invoke Register Image Action
            ***********************************************************************/
            // RegisterImageRequest request = new RegisterImageRequest();
            // @TODO: set request parameters here
            // RegisterImageSample.InvokeRegisterImage(service, request);
            /************************************************************************
            * Uncomment to invoke Release Address Action
            ***********************************************************************/
            // ReleaseAddressRequest request = new ReleaseAddressRequest();
            // @TODO: set request parameters here
            // ReleaseAddressSample.InvokeReleaseAddress(service, request);
            /************************************************************************
            * Uncomment to invoke Reset Image Attribute Action
            ***********************************************************************/
            // ResetImageAttributeRequest request = new ResetImageAttributeRequest();
            // @TODO: set request parameters here
            // ResetImageAttributeSample.InvokeResetImageAttribute(service, request);
            /************************************************************************
            * Uncomment to invoke Revoke Security Group Ingress Action
            ***********************************************************************/
            // RevokeSecurityGroupIngressRequest request = new RevokeSecurityGroupIngressRequest();
            // @TODO: set request parameters here
            // RevokeSecurityGroupIngressSample.InvokeRevokeSecurityGroupIngress(service, request);
            /************************************************************************
            * Uncomment to invoke Run Instances Action
            ***********************************************************************/
            // RunInstancesRequest request = new RunInstancesRequest();
            // @TODO: set request parameters here
            // RunInstancesSample.InvokeRunInstances(service, request);
            /************************************************************************
            * Uncomment to invoke Terminate Instances Action
            ***********************************************************************/
            // TerminateInstancesRequest request = new TerminateInstancesRequest();
            // @TODO: set request parameters here
            // TerminateInstancesSample.InvokeTerminateInstances(service, request);
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine("End of output. You can close this window");
            Console.WriteLine("===========================================");

            System.Threading.Thread.Sleep(50000);
        }

    }
}
