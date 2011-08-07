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
    public class DescribeReservedInstancesOfferingsRequest
    {
    
        private List<String> reservedInstancesIdField;

        private String instanceTypeField;

        private String availabilityZoneField;

        private String productDescriptionField;


        /// <summary>
        /// Gets and sets the ReservedInstancesId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ReservedInstancesId")]
        public List<String> ReservedInstancesId
        {
            get
            {
                if (this.reservedInstancesIdField == null)
                {
                    this.reservedInstancesIdField = new List<String>();
                }
                return this.reservedInstancesIdField;
            }
            set { this.reservedInstancesIdField =  value; }
        }



        /// <summary>
        /// Sets the ReservedInstancesId property
        /// </summary>
        /// <param name="list">ReservedInstancesId property</param>
        /// <returns>this instance</returns>
        public DescribeReservedInstancesOfferingsRequest WithReservedInstancesId(params String[] list)
        {
            foreach (String item in list)
            {
                ReservedInstancesId.Add(item);
            }
            return this;
        }          
 


        /// <summary>
        /// Checks of ReservedInstancesId property is set
        /// </summary>
        /// <returns>true if ReservedInstancesId property is set</returns>
        public Boolean IsSetReservedInstancesId()
        {
            return (ReservedInstancesId.Count > 0);
        }




        /// <summary>
        /// Gets and sets the InstanceType property.
        /// </summary>
        [XmlElementAttribute(ElementName = "InstanceType")]
        public String InstanceType
        {
            get { return this.instanceTypeField ; }
            set { this.instanceTypeField= value; }
        }



        /// <summary>
        /// Sets the InstanceType property
        /// </summary>
        /// <param name="instanceType">InstanceType property</param>
        /// <returns>this instance</returns>
        public DescribeReservedInstancesOfferingsRequest WithInstanceType(String instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }



        /// <summary>
        /// Checks if InstanceType property is set
        /// </summary>
        /// <returns>true if InstanceType property is set</returns>
        public Boolean IsSetInstanceType()
        {
            return  this.instanceTypeField != null;

        }


        /// <summary>
        /// Gets and sets the AvailabilityZone property.
        /// </summary>
        [XmlElementAttribute(ElementName = "AvailabilityZone")]
        public String AvailabilityZone
        {
            get { return this.availabilityZoneField ; }
            set { this.availabilityZoneField= value; }
        }



        /// <summary>
        /// Sets the AvailabilityZone property
        /// </summary>
        /// <param name="availabilityZone">AvailabilityZone property</param>
        /// <returns>this instance</returns>
        public DescribeReservedInstancesOfferingsRequest WithAvailabilityZone(String availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }



        /// <summary>
        /// Checks if AvailabilityZone property is set
        /// </summary>
        /// <returns>true if AvailabilityZone property is set</returns>
        public Boolean IsSetAvailabilityZone()
        {
            return  this.availabilityZoneField != null;

        }


        /// <summary>
        /// Gets and sets the ProductDescription property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ProductDescription")]
        public String ProductDescription
        {
            get { return this.productDescriptionField ; }
            set { this.productDescriptionField= value; }
        }



        /// <summary>
        /// Sets the ProductDescription property
        /// </summary>
        /// <param name="productDescription">ProductDescription property</param>
        /// <returns>this instance</returns>
        public DescribeReservedInstancesOfferingsRequest WithProductDescription(String productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }



        /// <summary>
        /// Checks if ProductDescription property is set
        /// </summary>
        /// <returns>true if ProductDescription property is set</returns>
        public Boolean IsSetProductDescription()
        {
            return  this.productDescriptionField != null;

        }





    }

}