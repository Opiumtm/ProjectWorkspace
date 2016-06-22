using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Simple editable node collection.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public class WorkspaceNodeCollection<TId> : WorkspaceNodeBase<IWorkspaceNode>, IWorkspaceNodeCollection<TId>, IWorkspaceNodeGroupEditor<TId>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">Node parent.</param>
        /// <param name="comparer">Equality comparer.</param>
        public WorkspaceNodeCollection(IWorkspaceNode parent, IEqualityComparer<TId> comparer = null) : base(parent)
        {
            _nodes = new ConcurrentDictionary<TId, IWorkspaceNode>(comparer ?? EqualityComparer<TId>.Default);
        }

        private readonly ConcurrentDictionary<TId, IWorkspaceNode> _nodes;

        /// <summary>
        /// Get child node.
        /// </summary>
        /// <param name="id">Child node identifier.</param>
        /// <returns>Child node (null if not found).</returns>
        public IWorkspaceNode GetChildNode(TId id)
        {
            IWorkspaceNode result;
            if (_nodes.TryGetValue(id, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Enumerate children.
        /// </summary>
        /// <returns>Children id's collection.</returns>
        public IEnumerable<TId> EnumChildren()
        {
            return _nodes.Keys;
        }

        /// <summary>
        /// Add child node.
        /// </summary>
        /// <param name="node">Node.</param>
        public void Add(IWorkspaceNode node)
        {
            if (node == null)
            {
                return;
            }
            TId id = node.Identifier<TId>().ThrowIfDefault("Object does not have required identifier");
            Add(node, id);
        }

        /// <summary>
        /// Add child node with identifier.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="id">Identifier.</param>
        public void Add(IWorkspaceNode node, TId id)
        {
            if (node == null)
            {
                return;
            }
            _nodes.AddOrUpdate(id, node, (id1, workspaceNode) => node);
        }

        /// <summary>
        /// Remove child node.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Remove(TId id)
        {
            IWorkspaceNode v;
            _nodes.TryRemove(id, out v);
        }

        /// <summary>
        /// Clear collection.
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }
    }
}