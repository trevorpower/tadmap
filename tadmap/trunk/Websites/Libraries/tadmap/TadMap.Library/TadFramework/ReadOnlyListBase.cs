using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Tad
{
    public abstract class ReadOnlyListBase<T, C> : Collection<C>,  Core.IReadOnlyCollection
        where T : ReadOnlyListBase<T, C>
    {
        protected bool IsReadOnly { get; set; }
    }
}
