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
    public class CreateSnapshotResult
    {
    
        private  Snapshot snapshotField;

        /// <summary>
        /// Gets and sets the Snapshot property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Snapshot")]
        public Snapshot Snapshot
        {
            get { return this.snapshotField ; }
            set { this.snapshotField = value; }
        }



        /// <summary>
        /// Sets the Snapshot property
        /// </summary>
        /// <param name="snapshot">Snapshot property</param>
        /// <returns>this instance</returns>
        public CreateSnapshotResult WithSnapshot(Snapshot snapshot)
        {
            this.snapshotField = snapshot;
            return this;
        }



        /// <summary>
        /// Checks if Snapshot property is set
        /// </summary>
        /// <returns>true if Snapshot property is set</returns>
        public Boolean IsSetSnapshot()
        {
            return this.snapshotField != null;
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
            if (IsSetSnapshot()) {
                Snapshot  snapshot = this.Snapshot;
                xml.Append("<Snapshot>");
                xml.Append(snapshot.ToXMLFragment());
                xml.Append("</Snapshot>");
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