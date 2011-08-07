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
    /// Describe Snapshots  Samples
    /// </summary>
    public class DescribeSnapshotsSample
    {
    
                                                                                                                                     
        /// <summary>
        /// Describes the indicated snapshots, or in lieu of that, all snapshots owned by the caller.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeSnapshotsRequest request</param>
        public static void InvokeDescribeSnapshots(AmazonEC2 service, DescribeSnapshotsRequest request)
        {
            try 
            {
                DescribeSnapshotsResponse response = service.DescribeSnapshots(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeSnapshotsResponse");
                if (response.IsSetDescribeSnapshotsResult())
                {
                    Console.WriteLine("            DescribeSnapshotsResult");
                    DescribeSnapshotsResult  describeSnapshotsResult = response.DescribeSnapshotsResult;
                    List<Snapshot> snapshotList = describeSnapshotsResult.Snapshot;
                    foreach (Snapshot snapshot in snapshotList)
                    {
                        Console.WriteLine("                Snapshot");
                        if (snapshot.IsSetSnapshotId())
                        {
                            Console.WriteLine("                    SnapshotId");
                            Console.WriteLine("                        {0}", snapshot.SnapshotId);
                        }
                        if (snapshot.IsSetVolumeId())
                        {
                            Console.WriteLine("                    VolumeId");
                            Console.WriteLine("                        {0}", snapshot.VolumeId);
                        }
                        if (snapshot.IsSetStatus())
                        {
                            Console.WriteLine("                    Status");
                            Console.WriteLine("                        {0}", snapshot.Status);
                        }
                        if (snapshot.IsSetStartTime())
                        {
                            Console.WriteLine("                    StartTime");
                            Console.WriteLine("                        {0}", snapshot.StartTime);
                        }
                        if (snapshot.IsSetProgress())
                        {
                            Console.WriteLine("                    Progress");
                            Console.WriteLine("                        {0}", snapshot.Progress);
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
