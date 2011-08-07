using System;
using System.Collections.Generic;
using System.Text;

namespace com.flajaxian
{
    public class ConfirmUploadEventArgs : EventArgs
    {
        private string _name;
        private string _changedName;
        private long _bytes;
        private int _httpStatus;
        private bool _isError;
        private bool _isLast;
        private int _index;

        public ConfirmUploadEventArgs(string name, string changedName, long bytes, int httpStatus, bool error, bool isLast, int index)
        {
            this._name = name;
            this._changedName = changedName;
            this._bytes = bytes;
            this._httpStatus = httpStatus;
            this._isError = error;
            this._isLast = isLast;
            this._index = index;
        }

        public string Name { get{ return this._name; } }
        public string ChangedName { get { return this._changedName; } }
        public long Bytes { get { return this._bytes; } }
        public int HttpStatus { get { return this._httpStatus; } }
        public bool IsError { get { return this._isError; } }
        public bool IsLast { get { return this._isLast; } }
        public int Index { get { return this._index; } }
    }
}
