using System.Collections.Generic;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Simple workspace node group.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public interface IWorkspaceNodeGroup<in TId> : IWorkspaceNode
    {
        /// <summary>
        /// Get child node.
        /// </summary>
        /// <param name="id">Child node identifier.</param>
        /// <returns>Child node (null if not found).</returns>
        IWorkspaceNode GetChildNode(TId id);
    }
}