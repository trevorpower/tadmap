using System;
using System.Collections.Generic;
using System.Text;

namespace Tad.Core
{
    /// <summary>
    /// Specifies that the object can save
    /// itself.
    /// </summary>
    public interface ISavable
    {
        /// <summary>
        /// Saves the object to the database.
        /// </summary>
        /// <returns>A new object containing the saved values.</returns>
        object Save();
        /// <summary>
        /// INTERNAL CSLA .NET USE ONLY.
        /// </summary>
        /// <param name="newObject">
        /// The new object returned as a result of the save.
        /// </param>
        void SaveComplete(object newObject);

    }
}
