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
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp.Statistics;

namespace Affirma.ThreeSharp.FormSample
{
    public partial class ThreeSharpFormSample : Form
    {
        private ThreeSharpConfig config;
        private IThreeSharp service;
        private String basePath;

        private Thread statsThread;
        private BindingList<Affirma.ThreeSharp.Model.Transfer> transfers;
        private bool isRunning = true;
        private BindStatisticsDelegate bindStatisticsDelegate;
        private ListBucketDelegate listBucketDelegate;
        private ListFolderDelegate listFolderDelegate;
        private UploadFileDelegate uploadFileDelegate;
        private DownloadFileDelegate downloadFileDelegate;

        public ThreeSharpFormSample()
        {
            InitializeComponent();

            this.transfers = new BindingList<Transfer>();
            this.dataGridViewStatistics.DataSource = transfers;

            bindStatisticsDelegate = new BindStatisticsDelegate(this.BindStatistics);
            listBucketDelegate = new ListBucketDelegate(this.ListBucket);
            listFolderDelegate = new ListFolderDelegate(this.ListFolder);
            uploadFileDelegate = new UploadFileDelegate(this.UploadFile);
            downloadFileDelegate = new DownloadFileDelegate(this.DownloadFile);

            this.basePath = "C:\\";

            config = new ThreeSharpConfig();
            config.AwsAccessKeyID = Properties.Settings.Default.AwsAccessKeyID;
            config.AwsSecretAccessKey = Properties.Settings.Default.AwsSecretAccessKey;
            config.ConnectionLimit = 40;
            config.IsSecure = true;

            // It is necessary to use the SUBDOMAIN CallingFormat for accessing EU buckets
            config.Format = CallingFormat.SUBDOMAIN;

            this.service = new ThreeSharpQuery(config);
        }


        /* ----------------  Form Event Handlers ---------------------------- */


        private void ThreeSharpFormSample_Load(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.AwsAccessKeyID.Length == 0 || Properties.Settings.Default.AwsSecretAccessKey.Length == 0)
                {
                    SettingsForm settingsForm = new SettingsForm();
                    if (settingsForm.ShowDialog() == DialogResult.OK)
                    {
                        this.config.AwsAccessKeyID = Properties.Settings.Default.AwsAccessKeyID;
                        this.config.AwsSecretAccessKey = Properties.Settings.Default.AwsSecretAccessKey;
                    }
                }

                BindBucketNames();

                if (Properties.Settings.Default.BucketName.Length > 0 && this.comboBoxBucketNames.Items.Contains(Properties.Settings.Default.BucketName))
                {
                    this.comboBoxBucketNames.SelectedItem = Properties.Settings.Default.BucketName;
                }

                ListFolder(this.basePath);

                statsThread = new Thread(new ThreadStart(this.StatsThreadMethod));
                statsThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void ThreeSharpFormSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isRunning = false;
        }

        private void buttonCreateBucket_Click(object sender, EventArgs e)
        {
            try
            {
                using (BucketAddRequest request = new BucketAddRequest(this.textBoxBucketName.Text.ToLower().Trim().Replace(' ', '_')))
                {

                    if (this.comboBoxLocation.Text != "US")
                    {
                        StringBuilder sbBucketConfig = new StringBuilder();
                        sbBucketConfig.AppendLine("<CreateBucketConfiguration>");
                        sbBucketConfig.AppendLine("<LocationConstraint>" + this.comboBoxLocation.Text + "</LocationConstraint>");
                        sbBucketConfig.AppendLine("</CreateBucketConfiguration>");
                        request.LoadStreamWithString(sbBucketConfig.ToString());
                    }

                    using (BucketAddResponse response = service.BucketAdd(request))
                    { }

                    BindBucketNames();
                    this.comboBoxBucketNames.SelectedItem = this.textBoxBucketName.Text.ToLower().Trim().Replace(' ', '_');
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxBucketNames_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ListBucket((String)this.comboBoxBucketNames.SelectedItem);
                Properties.Settings.Default.BucketName = (String)this.comboBoxBucketNames.SelectedItem;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxFolder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxFolder.SelectedItem != null)
                {
                    String value = this.listBoxFolder.SelectedItem.ToString();
                    if (value.StartsWith("["))
                    {
                        value = value.Substring(1, value.Length - 2);
                        if (value == "..")
                        {
                            this.basePath = this.basePath.Substring(0, this.basePath.Length - 1);
                            int index = this.basePath.LastIndexOf('\\');
                            this.basePath = this.basePath.Substring(0, index + 1);
                        }
                        else
                        {
                            this.basePath += value + "\\";
                        }
                        ListFolder(this.basePath);
                    }
                    else if (this.comboBoxBucketNames.SelectedItem != null)
                    {
                        this.uploadFileDelegate.BeginInvoke(this.comboBoxBucketNames.SelectedItem.ToString(), value, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxObjects_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxObjects.SelectedItem != null)
                {
                    String value = this.listBoxObjects.SelectedItem.ToString();
                    this.downloadFileDelegate.BeginInvoke(this.comboBoxBucketNames.SelectedItem.ToString(), value, null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxFolder.SelectedItem != null)
                {
                    String value = this.listBoxFolder.SelectedItem.ToString();
                    if (!value.StartsWith("[") && this.comboBoxBucketNames.SelectedItem != null)
                    {
                        this.uploadFileDelegate.BeginInvoke(this.comboBoxBucketNames.SelectedItem.ToString(), value, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxObjects.SelectedItem != null)
                {
                    String value = this.listBoxObjects.SelectedItem.ToString();
                    this.downloadFileDelegate.BeginInvoke(this.comboBoxBucketNames.SelectedItem.ToString(), value, null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDeleteObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxObjects.SelectedItem == null)
                    return;

                ConfirmDeleteForm confirmDeleteForm = new ConfirmDeleteForm(this.listBoxObjects.SelectedItems.Count.ToString());
                if (confirmDeleteForm.ShowDialog() != DialogResult.OK)
                    return;

                foreach (object item in listBoxObjects.SelectedItems)
                {
                    // The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
                    // minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to delete
                    // the file from the redirect URL

                    String redirectUrl = null;
                    using (BucketListRequest testRequest = new BucketListRequest(this.comboBoxBucketNames.SelectedItem.ToString()))
                    {
                        testRequest.Method = "HEAD";
                        using (BucketListResponse testResponse = service.BucketList(testRequest))
                        {
                            if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                            {
                                redirectUrl = testResponse.Headers["Location"].ToString() + item.ToString();
                            }
                        }
                    }

                    using (ObjectDeleteRequest request = new ObjectDeleteRequest(this.comboBoxBucketNames.SelectedItem.ToString(), item.ToString()))
                    {
                        request.RedirectUrl = redirectUrl;
                        using (ObjectDeleteResponse response = service.ObjectDelete(request))
                        { }
                    }
                }
                this.ListBucket(this.comboBoxBucketNames.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aWSAccessSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsForm settingsForm = new SettingsForm();
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    this.config.AwsAccessKeyID = Properties.Settings.Default.AwsAccessKeyID;
                    this.config.AwsSecretAccessKey = Properties.Settings.Default.AwsSecretAccessKey;
                }
                BindBucketNames();
                if (Properties.Settings.Default.BucketName.Length > 0 && this.comboBoxBucketNames.Items.Contains(Properties.Settings.Default.BucketName))
                {
                    this.comboBoxBucketNames.SelectedItem = Properties.Settings.Default.BucketName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /* ----------------------------- Form Processing Methods ------------------------ */


        private void StatsThreadMethod()
        {
            while (this.isRunning)
            {
                this.dataGridViewStatistics.Invoke(this.bindStatisticsDelegate);
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void BindBucketNames()
        {
            this.comboBoxBucketNames.Items.Clear();

            using (BucketListRequest request = new BucketListRequest(null))
            using (BucketListResponse response = service.BucketList(request))
            {
                XmlDocument bucketXml = response.StreamResponseToXmlDocument();

                XmlNodeList buckets = bucketXml.SelectNodes("//*[local-name()='Name']");
                foreach (XmlNode bucket in buckets)
                {
                    this.comboBoxBucketNames.Items.Add(bucket.InnerXml);
                }
            }
        }

        private void BindStatistics()
        {
            try
            {
                Transfer[] grabbedTransfers = this.service.GetTransfers();

                foreach (Transfer transfer in grabbedTransfers)
                {
                    if (!String.IsNullOrEmpty(transfer.BucketName) && !String.IsNullOrEmpty(transfer.Key))
                    {
                        int matchIndex = -1;
                        for (int i = 0; i < this.transfers.Count; i++)
                        {
                            if (this.transfers[i].ID == transfer.ID)
                            {
                                matchIndex = i;
                                break;
                            }
                        }
                        if (matchIndex > -1)
                        {
                            // Update the values
                            Transfer t = this.transfers[matchIndex];
                            t.BytesTransferred = transfer.BytesTransferred;
                            t.BytesTotal = transfer.BytesTotal;
                        }
                        else
                        {
                            // Add a new one to the DataGridView
                            this.transfers.Add(transfer);
                        }
                    }
                }

                if (this.dataGridViewStatistics.Rows.Count > 0)
                {
                    this.dataGridViewStatistics.UpdateRowHeightInfo(0, true);
                    this.dataGridViewStatistics.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListBucket(String bucketName)
        {
            try
            {
                this.listBoxObjects.Items.Clear();

                // The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
                // minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to pull
                // the bucket listing from the redirect URL

                String redirectUrl = null;
                using (BucketListRequest testRequest = new BucketListRequest(bucketName))
                {
                    testRequest.Method = "HEAD";
                    using (BucketListResponse testResponse = service.BucketList(testRequest))
                    {
                        if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                        {
                            redirectUrl = testResponse.Headers["Location"].ToString();
                        }
                    }
                }

                bool isTruncated = true;
                string marker = string.Empty;

                // The while-loop is here because S3 will only return a maximum of 1000 items at a time, so if the list
                // was truncated, we need to make another call and get the rest
                while (isTruncated)
                {
                    using (BucketListRequest request = new BucketListRequest(bucketName))
                    {
                        request.RedirectUrl = redirectUrl;
                        if (!string.IsNullOrEmpty(marker))
                        {
                            request.QueryList.Add("marker", marker);
                        }
                        using (BucketListResponse response = service.BucketList(request))
                        {
                            XmlDocument bucketXml = response.StreamResponseToXmlDocument();

                            XmlNodeList objects = bucketXml.SelectNodes("//*[local-name()='Key']");
                            foreach (XmlNode obj in objects)
                            {
                                marker = obj.InnerText;
                                this.listBoxObjects.Items.Add(marker);
                            }

                            isTruncated = bool.Parse(bucketXml.SelectSingleNode("//*[local-name()='IsTruncated']").InnerXml);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListFolder(String path)
        {
            try
            {
                this.textBoxBasePath.Text = path;

                this.listBoxFolder.Items.Clear();

                if (path != "C:\\")
                {
                    this.listBoxFolder.Items.Add("[..]");
                }

                String[] folders = Directory.GetDirectories(path);
                foreach (String folder in folders)
                {
                    int index = folder.LastIndexOf('\\') + 1;
                    this.listBoxFolder.Items.Add("[" + folder.Substring(index) + "]");
                }

                String[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    int index = file.LastIndexOf('\\') + 1;
                    this.listBoxFolder.Items.Add(file.Substring(index));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UploadFile(String bucketName, String filename)
        {
            // The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
            // minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to upload
            // the file to the redirect URL

            String redirectUrl = null;
            using (BucketListRequest testRequest = new BucketListRequest(bucketName))
            {
                testRequest.Method = "HEAD";
                using (BucketListResponse testResponse = service.BucketList(testRequest))
                {
                    if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                    {
                        redirectUrl = testResponse.Headers["Location"].ToString() + filename.Replace(' ', '_');
                    }
                }
            }

            using (ObjectAddRequest request = new ObjectAddRequest(bucketName, filename.Replace(' ', '_')))
            {
                request.Headers.Add("x-amz-acl", Properties.Settings.Default.AclType);
                request.LoadStreamWithFile(this.basePath + filename);

                if (redirectUrl != null)
                {
                    request.RedirectUrl = redirectUrl;
                }

                using (ObjectAddResponse response = service.ObjectAdd(request))
                { }
            }

            this.listBoxObjects.Invoke(listBucketDelegate, new object[] { bucketName });
        }

        private void DownloadFile(String bucketName, String key)
        {
            // The first thing we need to do is check for the presence of a Temporary Redirect.  These occur for a few
            // minutes after an EU bucket is created, while S3 creates the DNS entries.  If we get one, we need to download
            // the file from the redirect URL

            String redirectUrl = null;
            using (BucketListRequest testRequest = new BucketListRequest(bucketName))
            {
                testRequest.Method = "HEAD";
                using (BucketListResponse testResponse = service.BucketList(testRequest))
                {
                    if (testResponse.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                    {
                        redirectUrl = testResponse.Headers["Location"].ToString() + key;
                    }
                }
            }

            using (ObjectGetRequest request = new ObjectGetRequest(bucketName, key))
            {
                request.RedirectUrl = redirectUrl;
                using (ObjectGetResponse response = service.ObjectGet(request))
                {
                    response.StreamResponseToFile(this.basePath + key);
                }
            }

            this.listBoxFolder.Invoke(listFolderDelegate, new object[] { this.basePath });
        }


    }

    public delegate void BindStatisticsDelegate();
    public delegate void ListBucketDelegate(String bucketName);
    public delegate void ListFolderDelegate(String path);
    public delegate void UploadFileDelegate(String bucketName, String filename);
    public delegate void DownloadFileDelegate(String bucketName, String key);
}