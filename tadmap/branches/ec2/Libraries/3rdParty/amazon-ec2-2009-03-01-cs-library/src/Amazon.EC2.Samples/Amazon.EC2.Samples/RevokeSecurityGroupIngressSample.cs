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
    /// Revoke Security Group Ingress  Samples
    /// </summary>
    public class RevokeSecurityGroupIngressSample
    {
    
                                                                                                                                                                                 
        /// <summary>
        /// The RevokeSecurityGroupIngress operation revokes permissions from a security
        /// group. The permissions used to revoke must be specified using the same values
        /// used to grant the permissions.
        /// Permissions are specified by IP protocol (TCP, UDP, or ICMP), the source of the
        /// request (by IP range or an Amazon EC2 user-group pair), the source and
        /// destination port ranges (for TCP and UDP), and the ICMP codes and types (for
        /// ICMP).
        /// Permission changes are quickly propagated to instances within the security
        /// group. However, depending on the number of instances in the group, a small
        /// delay is might occur, .
        /// When revoking a user/group pair permission, GroupName, SourceSecurityGroupName
        /// and SourceSecurityGroupOwnerId must be specified. When authorizing a CIDR IP
        /// permission, GroupName, IpProtocol, FromPort, ToPort and CidrIp must be
        /// specified. Mixing these two types of parameters is not allowed.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">RevokeSecurityGroupIngressRequest request</param>
        public static void InvokeRevokeSecurityGroupIngress(AmazonEC2 service, RevokeSecurityGroupIngressRequest request)
        {
            try 
            {
                RevokeSecurityGroupIngressResponse response = service.RevokeSecurityGroupIngress(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        RevokeSecurityGroupIngressResponse");
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
