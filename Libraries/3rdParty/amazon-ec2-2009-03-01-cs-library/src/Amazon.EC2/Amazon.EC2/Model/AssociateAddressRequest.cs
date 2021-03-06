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
using System.Text;


namespace Amazon.EC2.Model
{
    [XmlTypeAttribute(Namespace = "http://ec2.amazonaws.com/doc/2009-03-01/")]
    [XmlRootAttribute(Namespace = "http://ec2.amazonaws.com/doc/2009-03-01/", IsNullable = false)]
    public class AssociateAddressRequest
    {
    
        private String instanceIdField;

        private String publicIpField;


        /// <summary>
        /// Gets and sets the InstanceId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "InstanceId")]
        public String InstanceId
        {
            get { return this.instanceIdField ; }
            set { this.instanceIdField= value; }
        }



        /// <summary>
        /// Sets the InstanceId property
        /// </summary>
        /// <param name="instanceId">InstanceId property</param>
        /// <returns>this instance</returns>
        public AssociateAddressRequest WithInstanceId(String instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }



        /// <summary>
        /// Checks if InstanceId property is set
        /// </summary>
        /// <returns>true if InstanceId property is set</returns>
        public Boolean IsSetInstanceId()
        {
            return  this.instanceIdField != null;

        }


        /// <summary>
        /// Gets and sets the PublicIp property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PublicIp")]
        public String PublicIp
        {
            get { return this.publicIpField ; }
            set { this.publicIpField= value; }
        }



        /// <summary>
        /// Sets the PublicIp property
        /// </summary>
        /// <param name="publicIp">PublicIp property</param>
        /// <returns>this instance</returns>
        public AssociateAddressRequest WithPublicIp(String publicIp)
        {
            this.publicIpField = publicIp;
            return this;
        }



        /// <summary>
        /// Checks if PublicIp property is set
        /// </summary>
        /// <returns>true if PublicIp property is set</returns>
        public Boolean IsSetPublicIp()
        {
            return  this.publicIpField != null;

        }





    }

}