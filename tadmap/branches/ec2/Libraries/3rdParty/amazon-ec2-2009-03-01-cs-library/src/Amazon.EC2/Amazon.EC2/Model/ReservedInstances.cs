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
    public class ReservedInstances
    {
    
        private String reservedInstancesIdField;

        private String instanceTypeField;

        private String availabilityZoneField;

        private Decimal? durationField;

        private String usagePriceField;

        private String fixedPriceField;

        private Decimal? instanceCountField;

        private String productDescriptionField;

        private String purchaseStateField;


        /// <summary>
        /// Gets and sets the ReservedInstancesId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ReservedInstancesId")]
        public String ReservedInstancesId
        {
            get { return this.reservedInstancesIdField ; }
            set { this.reservedInstancesIdField= value; }
        }



        /// <summary>
        /// Sets the ReservedInstancesId property
        /// </summary>
        /// <param name="reservedInstancesId">ReservedInstancesId property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithReservedInstancesId(String reservedInstancesId)
        {
            this.reservedInstancesIdField = reservedInstancesId;
            return this;
        }



        /// <summary>
        /// Checks if ReservedInstancesId property is set
        /// </summary>
        /// <returns>true if ReservedInstancesId property is set</returns>
        public Boolean IsSetReservedInstancesId()
        {
            return  this.reservedInstancesIdField != null;

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
        public ReservedInstances WithInstanceType(String instanceType)
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
        public ReservedInstances WithAvailabilityZone(String availabilityZone)
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
        /// Gets and sets the Duration property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Duration")]
        public Decimal Duration
        {
            get { return this.durationField.GetValueOrDefault() ; }
            set { this.durationField= value; }
        }



        /// <summary>
        /// Sets the Duration property
        /// </summary>
        /// <param name="duration">Duration property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithDuration(Decimal duration)
        {
            this.durationField = duration;
            return this;
        }



        /// <summary>
        /// Checks if Duration property is set
        /// </summary>
        /// <returns>true if Duration property is set</returns>
        public Boolean IsSetDuration()
        {
            return  this.durationField.HasValue;

        }


        /// <summary>
        /// Gets and sets the UsagePrice property.
        /// </summary>
        [XmlElementAttribute(ElementName = "UsagePrice")]
        public String UsagePrice
        {
            get { return this.usagePriceField ; }
            set { this.usagePriceField= value; }
        }



        /// <summary>
        /// Sets the UsagePrice property
        /// </summary>
        /// <param name="usagePrice">UsagePrice property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithUsagePrice(String usagePrice)
        {
            this.usagePriceField = usagePrice;
            return this;
        }



        /// <summary>
        /// Checks if UsagePrice property is set
        /// </summary>
        /// <returns>true if UsagePrice property is set</returns>
        public Boolean IsSetUsagePrice()
        {
            return  this.usagePriceField != null;

        }


        /// <summary>
        /// Gets and sets the FixedPrice property.
        /// </summary>
        [XmlElementAttribute(ElementName = "FixedPrice")]
        public String FixedPrice
        {
            get { return this.fixedPriceField ; }
            set { this.fixedPriceField= value; }
        }



        /// <summary>
        /// Sets the FixedPrice property
        /// </summary>
        /// <param name="fixedPrice">FixedPrice property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithFixedPrice(String fixedPrice)
        {
            this.fixedPriceField = fixedPrice;
            return this;
        }



        /// <summary>
        /// Checks if FixedPrice property is set
        /// </summary>
        /// <returns>true if FixedPrice property is set</returns>
        public Boolean IsSetFixedPrice()
        {
            return  this.fixedPriceField != null;

        }


        /// <summary>
        /// Gets and sets the InstanceCount property.
        /// </summary>
        [XmlElementAttribute(ElementName = "InstanceCount")]
        public Decimal InstanceCount
        {
            get { return this.instanceCountField.GetValueOrDefault() ; }
            set { this.instanceCountField= value; }
        }



        /// <summary>
        /// Sets the InstanceCount property
        /// </summary>
        /// <param name="instanceCount">InstanceCount property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithInstanceCount(Decimal instanceCount)
        {
            this.instanceCountField = instanceCount;
            return this;
        }



        /// <summary>
        /// Checks if InstanceCount property is set
        /// </summary>
        /// <returns>true if InstanceCount property is set</returns>
        public Boolean IsSetInstanceCount()
        {
            return  this.instanceCountField.HasValue;

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
        public ReservedInstances WithProductDescription(String productDescription)
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


        /// <summary>
        /// Gets and sets the PurchaseState property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PurchaseState")]
        public String PurchaseState
        {
            get { return this.purchaseStateField ; }
            set { this.purchaseStateField= value; }
        }



        /// <summary>
        /// Sets the PurchaseState property
        /// </summary>
        /// <param name="purchaseState">PurchaseState property</param>
        /// <returns>this instance</returns>
        public ReservedInstances WithPurchaseState(String purchaseState)
        {
            this.purchaseStateField = purchaseState;
            return this;
        }



        /// <summary>
        /// Checks if PurchaseState property is set
        /// </summary>
        /// <returns>true if PurchaseState property is set</returns>
        public Boolean IsSetPurchaseState()
        {
            return  this.purchaseStateField != null;

        }




        /// <summary>
        /// XML fragment representation of this object
        /// </summary>
        /// <returns>XML fragment for this object.</returns>
        /// <remarks>
        /// Name for outer tag expected to be set by calling method. 
        /// This fragment returns inner properties representation only
        /// </remarks>


        protected internal String ToXMLFragment() {
            StringBuilder xml = new StringBuilder();
            if (IsSetReservedInstancesId()) {
                xml.Append("<ReservedInstancesId>");
                xml.Append(EscapeXML(this.ReservedInstancesId));
                xml.Append("</ReservedInstancesId>");
            }
            if (IsSetInstanceType()) {
                xml.Append("<InstanceType>");
                xml.Append(EscapeXML(this.InstanceType));
                xml.Append("</InstanceType>");
            }
            if (IsSetAvailabilityZone()) {
                xml.Append("<AvailabilityZone>");
                xml.Append(EscapeXML(this.AvailabilityZone));
                xml.Append("</AvailabilityZone>");
            }
            if (IsSetDuration()) {
                xml.Append("<Duration>");
                xml.Append(this.Duration);
                xml.Append("</Duration>");
            }
            if (IsSetUsagePrice()) {
                xml.Append("<UsagePrice>");
                xml.Append(EscapeXML(this.UsagePrice));
                xml.Append("</UsagePrice>");
            }
            if (IsSetFixedPrice()) {
                xml.Append("<FixedPrice>");
                xml.Append(EscapeXML(this.FixedPrice));
                xml.Append("</FixedPrice>");
            }
            if (IsSetInstanceCount()) {
                xml.Append("<InstanceCount>");
                xml.Append(this.InstanceCount);
                xml.Append("</InstanceCount>");
            }
            if (IsSetProductDescription()) {
                xml.Append("<ProductDescription>");
                xml.Append(EscapeXML(this.ProductDescription));
                xml.Append("</ProductDescription>");
            }
            if (IsSetPurchaseState()) {
                xml.Append("<PurchaseState>");
                xml.Append(EscapeXML(this.PurchaseState));
                xml.Append("</PurchaseState>");
            }
            return xml.ToString();
        }

        /**
         * 
         * Escape XML special characters
         */
        private String EscapeXML(String str) {
            StringBuilder sb = new StringBuilder();
            foreach (Char c in str)
            {
                switch (c) {
                case '&':
                    sb.Append("&amp;");
                    break;
                case '<':
                    sb.Append("&lt;");
                    break;
                case '>':
                    sb.Append("&gt;");
                    break;
                case '\'':
                    sb.Append("&#039;");
                    break;
                case '"':
                    sb.Append("&quot;");
                    break;
                default:
                    sb.Append(c);
                    break;
                }
            }
            return sb.ToString();
        }



    }

}