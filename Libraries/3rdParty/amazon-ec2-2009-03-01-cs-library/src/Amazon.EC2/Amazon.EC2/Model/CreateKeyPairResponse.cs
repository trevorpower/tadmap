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
    public class CreateKeyPairResponse
    {
    
        private  ResponseMetadata responseMetadataField;
        private  CreateKeyPairResult createKeyPairResultField;

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ResponseMetadata")]
        public ResponseMetadata ResponseMetadata
        {
            get { return this.responseMetadataField ; }
            set { this.responseMetadataField = value; }
        }



        /// <summary>
        /// Sets the ResponseMetadata property
        /// </summary>
        /// <param name="responseMetadata">ResponseMetadata property</param>
        /// <returns>this instance</returns>
        public CreateKeyPairResponse WithResponseMetadata(ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }



        /// <summary>
        /// Checks if ResponseMetadata property is set
        /// </summary>
        /// <returns>true if ResponseMetadata property is set</returns>
        public Boolean IsSetResponseMetadata()
        {
            return this.responseMetadataField != null;
        }




        /// <summary>
        /// Gets and sets the CreateKeyPairResult property.
        /// </summary>
        [XmlElementAttribute(ElementName = "CreateKeyPairResult")]
        public CreateKeyPairResult CreateKeyPairResult
        {
            get { return this.createKeyPairResultField ; }
            set { this.createKeyPairResultField = value; }
        }



        /// <summary>
        /// Sets the CreateKeyPairResult property
        /// </summary>
        /// <param name="createKeyPairResult">CreateKeyPairResult property</param>
        /// <returns>this instance</returns>
        public CreateKeyPairResponse WithCreateKeyPairResult(CreateKeyPairResult createKeyPairResult)
        {
            this.createKeyPairResultField = createKeyPairResult;
            return this;
        }



        /// <summary>
        /// Checks if CreateKeyPairResult property is set
        /// </summary>
        /// <returns>true if CreateKeyPairResult property is set</returns>
        public Boolean IsSetCreateKeyPairResult()
        {
            return this.createKeyPairResultField != null;
        }






        /// <summary>
        /// XML Representation for this object
        /// </summary>
        /// <returns>XML String</returns>

        public String ToXML() {
            StringBuilder xml = new StringBuilder();
            xml.Append("<CreateKeyPairResponse xmlns=\"http://ec2.amazonaws.com/doc/2009-03-01/\">");
            if (IsSetResponseMetadata()) {
                ResponseMetadata  responseMetadata = this.ResponseMetadata;
                xml.Append("<ResponseMetadata>");
                xml.Append(responseMetadata.ToXMLFragment());
                xml.Append("</ResponseMetadata>");
            } 
            if (IsSetCreateKeyPairResult()) {
                CreateKeyPairResult  createKeyPairResult = this.CreateKeyPairResult;
                xml.Append("<CreateKeyPairResult>");
                xml.Append(createKeyPairResult.ToXMLFragment());
                xml.Append("</CreateKeyPairResult>");
            } 
            xml.Append("</CreateKeyPairResponse>");
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