using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Core
{
    public interface IReadOnlyObject
    {
        bool CanReadProperty(string properytName);
    }
}
