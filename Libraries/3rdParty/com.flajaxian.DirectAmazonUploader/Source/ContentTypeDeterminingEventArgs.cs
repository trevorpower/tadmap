using System;
using System.Collections.Generic;
using System.Text;

namespace com.flajaxian
{
    public class ContentTypeDeterminingEventArgs : EventArgs
    {
        private string _fileName;
        private string _contentType;

        public string FileName
        {
            get { return this._fileName; }
            set { this._fileName = value; }
        }

        public string ContentType
        {
            get { return this._contentType; }
            set { this._contentType = value; }
        }
    }
}
