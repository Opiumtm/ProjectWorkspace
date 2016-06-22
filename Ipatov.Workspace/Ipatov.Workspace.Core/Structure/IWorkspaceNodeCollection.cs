using System.Collections.Generic;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Workspace node collection with well-known children.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public interface IWorkspaceNodeCollection<TId> : IWorkspaceNodeGroup<TId>
    {
        /// <summary>
        /// Enumerate children.
        /// </summary>
        /// <returns>Children id's collection.</returns>
        IEnumerable<TId> EnumChildren();
    }
}