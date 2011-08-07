using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }

        public ValidationException(string strMessage) : base(strMessage) { }

        public ValidationException(string strMessage, Exception oInnerException) :
            base(strMessage, oInnerException)
        { }
    }
}
