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
    /// Describe Security Groups  Samples
    /// </summary>
    public class DescribeSecurityGroupsSample
    {
    
                                                                                                                                 
        /// <summary>
        /// The DescribeSecurityGroups operation returns information about security groups
        /// that you own.
        /// If you specify security group names, information about those security group is
        /// returned. Otherwise, information for all security group is returned. If you
        /// specify a group that does not exist, a fault is returned.
        /// 
        /// </summary>
        /// <param name="service">Instance of AmazonEC2 service</param>
        /// <param name="request">DescribeSecurityGroupsRequest request</param>
        public static void InvokeDescribeSecurityGroups(AmazonEC2 service, DescribeSecurityGroupsRequest request)
        {
            try 
            {
                DescribeSecurityGroupsResponse response = service.DescribeSecurityGroups(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        DescribeSecurityGroupsResponse");
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
                if (response.IsSetDescribeSecurityGroupsResult())
                {
                    Console.WriteLine("            DescribeSecurityGroupsResult");
                    DescribeSecurityGroupsResult  describeSecurityGroupsResult = response.DescribeSecurityGroupsResult;
                    List<SecurityGroup> securityGroupList = describeSecurityGroupsResult.SecurityGroup;
                    foreach (SecurityGroup securityGroup in securityGroupList)
                    {
                        Console.WriteLine("                SecurityGroup");
                        if (securityGroup.IsSetOwnerId())
                        {
                            Console.WriteLine("                    OwnerId");
                            Console.WriteLine("                        {0}", securityGroup.OwnerId);
                        }
                        if (securityGroup.IsSetGroupName())
                        {
                            Console.WriteLine("                    GroupName");
                            Console.WriteLine("                        {0}", securityGroup.GroupName);
                        }
                        if (securityGroup.IsSetGroupDescription())
                        {
                            Console.WriteLine("                    GroupDescription");
                            Console.WriteLine("                        {0}", securityGroup.GroupDescription);
                        }
                        List<IpPermission> ipPermissionList = securityGroup.IpPermission;
                        foreach (IpPermission ipPermission in ipPermissionList)
                        {
                            Console.WriteLine("                    IpPermission");
                            if (ipPermission.IsSetIpProtocol())
                            {
                                Console.WriteLine("                        IpProtocol");
                                Console.WriteLine("                            {0}", ipPermission.IpProtocol);
                            }
                            if (ipPermission.IsSetFromPort())
                            {
                                Console.WriteLine("                        FromPort");
                                Console.WriteLine("                            {0}", ipPermission.FromPort);
                            }
                            if (ipPermission.IsSetToPort())
                            {
                                Console.WriteLine("                        ToPort");
                                Console.WriteLine("                            {0}", ipPermission.ToPort);
                            }
                            List<UserIdGroupPair> userIdGroupPairList = ipPermission.UserIdGroupPair;
                            foreach (UserIdGroupPair userIdGroupPair in userIdGroupPairList)
                            {
                                Console.WriteLine("                        UserIdGroupPair");
                                if (userIdGroupPair.IsSetUserId())
                                {
                                    Console.WriteLine("                            UserId");
                                    Console.WriteLine("                                {0}", userIdGroupPair.UserId);
                                }
                                if (userIdGroupPair.IsSetGroupName())
                                {
                                    Console.WriteLine("                            GroupName");
                                    Console.WriteLine("                                {0}", userIdGroupPair.GroupName);
                                }
                            }
                            List<String> ipRangeList  =  ipPermission.IpRange;
                            foreach (String ipRange in ipRangeList)
                            {
                                Console.WriteLine("                        IpRange");
                                Console.WriteLine("                            {0}", ipRange);
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
