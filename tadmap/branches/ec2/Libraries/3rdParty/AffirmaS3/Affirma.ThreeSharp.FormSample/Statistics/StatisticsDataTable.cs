using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Affirma.ThreeSharp.FormSample.Statistics
{
    public class StatisticsDataTable : DataTable
    {
        public StatisticsDataTable()
        {
            this.Columns.Add("ID");
            this.Columns.Add("Method");
            this.Columns.Add("BucketName");
            this.Columns.Add("Key");
            this.Columns.Add("BytesTransferred");
            this.Columns.Add("BytesTotal");

            this.PrimaryKey = new DataColumn[] { this.Columns["ID"] };
        }

        public void AddTransfer(Affirma.ThreeSharp.Model.Transfer transfer)
        {
            String totalBytes = (transfer.BytesTotal == 0) ? "Unknown" : transfer.BytesTotal.ToString();
            if (!this.ContainsTransfer(transfer))
            {
                this.Rows.Add(new object[] { transfer.ID, transfer.Method, transfer.BucketName, transfer.Key, transfer.BytesTransferred, totalBytes });
            }
            else
            {
                DataRow dr = this.Rows.Find(transfer.ID);
                dr["BytesTransferred"] = transfer.BytesTransferred;
                dr["BytesTotal"] = transfer.BytesTotal;
            }
        }

        private bool ContainsTransfer(Affirma.ThreeSharp.Model.Transfer transfer)
        {
            return this.Rows.Contains(transfer.ID);
        }
    }
}
