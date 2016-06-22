using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Helper class for a workspace structure.
    /// </summary>
    public static class WorkspaceStructureHelper
    {
        private static readonly ConditionalWeakTable<IWorkspaceNode, ConcurrentDictionary<Type, IWorkspaceNode>> AttachedViews = new ConditionalWeakTable<IWorkspaceNode, ConcurrentDictionary<Type, IWorkspaceNode>>();

        /// <summary>
        /// Get node typed view.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <param name="node">Node.</param>
        /// <returns>Typed view.</returns>
        public static T As<T>(this IWorkspaceNode node) where T : class, IWorkspaceNode
        {
            if (node != null)
            {
                ConcurrentDictionary<Type, IWorkspaceNode> extensions;
                if (AttachedViews.TryGetValue(node, out extensions))
                {
                    IWorkspaceNode view;
                    T typedView = null;
                    if (extensions.TryGetValue(typeof (T), out view))
                    {
                        typedView = view as T;
                    }
                    if (typedView != null)
                    {
                        return typedView;
                    }
                }
            }
            return (node as IWorkspaceNodeViewProvider)?.GetView<T>();
        }

        /// <summary>
        /// Throw exception if view is not supported.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <param name="view">View.</param>
        /// <returns>View.</returns>
        public static T ThrowIfNotSupported<T>(this T view) where T : class, IWorkspaceNode
        {
            if (view == null)
            {
                throw new InvalidOperationException($"Requested node view {typeof(T)} is not supported");
            }
            return view;
        }

        /// <summary>
        /// Attach view (may override existing view).
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <param name="node">Node.</param>
        /// <param name="view">Attached view (removed if null).</param>
        public static void AttachView<T>(this IWorkspaceNode node, T view) where T : class, IWorkspaceNode
        {
            if (node == null)
            {
                return;
            }
            var extensions = AttachedViews.GetOrCreateValue(node);
            if (view == null)
            {
                IWorkspaceNode r;
                extensions.TryRemove(typeof (T), out r);
            }
            else
            {
                extensions.AddOrUpdate(typeof (T), view, (type, workspaceNode) => view);
            }
        }

        /// <summary>
        /// Get child node (if any).
        /// </summary>
        /// <typeparam name="TId">Identifier type.</typeparam>
        /// <param name="node">Parent node.</param>
        /// <param name="id">Child node identifier.</param>
        /// <returns>Child node.</returns>
        public static IWorkspaceNode Child<TId>(this IWorkspaceNode node, TId id)
        {
            return node.As<IWorkspaceNodeGroup<TId>>()?.GetChildNode(id);
        }

        /// <summary>
        /// Get child nodes snapshot.
        /// </summary>
        /// <typeparam name="TId">Identifier type.</typeparam>
        /// <param name="node">Node.</param>
        /// <param name="comparer">Identifier equality comparer.</param>
        /// <returns>Child nodes (empty if not supported).</returns>
        public static IReadOnlyDictionary<TId, IWorkspaceNode> ChildrenSnaphsot<TId>(this IWorkspaceNode node, IEqualityComparer<TId> comparer = null)
        {
            var collection = node.As<IWorkspaceNodeCollection<TId>>();
            comparer = comparer ?? EqualityComparer<TId>.Default;
            return new ReadOnlyDictionary<TId, IWorkspaceNode>(collection?.EnumChildren()
                ?.Distinct(comparer)
                ?.ToDictionary(id => id, id => collection.GetChildNode(id), comparer) ?? new Dictionary<TId, IWorkspaceNode>(comparer));
        }

        /// <summary>
        /// Get node identifier.
        /// </summary>
        /// <typeparam name="TId">Identifier type.</typeparam>
        /// <param name="node">Node.</param>
        /// <param name="defaultId">Default identifier.</param>
        /// <returns>Node identifier.</returns>
        public static Defaultable<TId> Identifier<TId>(this IWorkspaceNode node, TId defaultId = default(TId))
        {
            var idProvider = node.As<IWorkspaceNodeId<TId>>();
            return idProvider != null ? new Defaultable<TId>(idProvider.NodeId, false) : new Defaultable<TId>(defaultId, true);
        }

        /// <summary>
        /// Create wrapped reference.
        /// </summary>
        /// <param name="node">Workspace node.</param>
        /// <returns>Wrapped reference.</returns>
        public static IWorkspaceNode WrapReference(this IWorkspaceNode node)
        {
            if (node == null)
            {
                return null;
            }
            return new WorkspaceNodeWrappedReference(node);
        }
    }
}