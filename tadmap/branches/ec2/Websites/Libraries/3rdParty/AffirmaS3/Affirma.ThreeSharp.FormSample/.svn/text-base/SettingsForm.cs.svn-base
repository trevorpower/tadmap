/******************************************************************************* 
 *  Licensed under the Apache License, Version 2.0 (the "License"); 
 *  
 *  You may not use this file except in compliance with the License. 
 *  You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0.html 
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 *  specific language governing permissions and limitations under the License.
 * ***************************************************************************** 
 * 
 *  Joel Wetzel
 *  Affirma Consulting
 *  jwetzel@affirmaconsulting.com
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Affirma.ThreeSharp.FormSample
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.textBoxAwsAccessKeyId.Text = Properties.Settings.Default.AwsAccessKeyID;
            this.textBoxAwsSecretAccessKey.Text = Properties.Settings.Default.AwsSecretAccessKey;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AwsAccessKeyID = this.textBoxAwsAccessKeyId.Text;
            Properties.Settings.Default.AwsSecretAccessKey = this.textBoxAwsSecretAccessKey.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

    }
}