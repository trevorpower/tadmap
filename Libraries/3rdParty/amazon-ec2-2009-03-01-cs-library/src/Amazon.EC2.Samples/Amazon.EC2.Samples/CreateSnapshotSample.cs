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
    /// Create Snapshot  Samples
    /// </summary>
    public class CreateSnapshotSample
    {
    
                                                             
        /// <summary>
        /// Create a snapshot of the volume identified by volume ID. A volume does not have to be detached
        /// at the time the snapshot is taken.
        /// Important Note:
        /// Snapshot creation requires that the system is in a consistent state.
        /// For instance, this means that if taking a snapshot of a database, the tables must
        /// be read-only locked to ensure that the snapshot will not contain a corrupted
        /// version of the database.  Therefore, be careful when using this API to ensure that
        /// the system remains in the consistent state until the create snapshot status
        /// has returned.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">CreateSnapshotRequest request</param>
        public static void InvokeCreateSnapshot(AmazonEC2 service, CreateSnapshotRequest request)
        {
            try 
            {
                CreateSnapshotResponse response = service.CreateSnapshot(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        CreateSnapshotResponse");
                if (response.IsSetCreateSnapshotResult())
                {
                    Console.WriteLine("            CreateSnapshotResult");
                    CreateSnapshotResult  createSnapshotResult = response.CreateSnapshotResult;
                    if (createSnapshotResult.IsSetSnapshot())
                    {
                        Console.WriteLine("                Snapshot");
                        Snapshot  snapshot = createSnapshotResult.Snapshot;
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
