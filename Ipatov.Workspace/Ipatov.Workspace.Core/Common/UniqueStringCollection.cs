using System;
using System.Collections.ObjectModel;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Collection of unique-keyed strings.
    /// </summary>
    public sealed class UniqueStringCollection : KeyedCollection<Guid, UniqueString>
    {
        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <returns>
        /// The key for the specified element.
        /// </returns>
        /// <param name="item">The element from which to extract the key.</param>
        protected override Guid GetKeyForItem(UniqueString item)
        {
            return item;
        }
    }
}