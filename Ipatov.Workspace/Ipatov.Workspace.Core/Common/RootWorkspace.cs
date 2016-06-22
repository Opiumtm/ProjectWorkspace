using System;
using System.Threading;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Root workspace.
    /// </summary>
    public class RootWorkspace : WorkspaceNodeCollection<string>
    {
        private static IWorkspaceNode _rootWorkspace = new RootWorkspace();

        /// <summary>
        /// Current workspace root (can be changed for unit testing).
        /// </summary>
        public static IWorkspaceNode Current
        {
            get { return Interlocked.CompareExchange(ref _rootWorkspace, null, null); }
            set { Interlocked.Exchange(ref _rootWorkspace, value); }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RootWorkspace() : base(null, StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Make sure global node is present.
        /// </summary>
        /// <param name="node">Factory func.</param>
        /// <param name="id">Node id.</param>
        /// <returns>Node.</returns>
        public static IWorkspaceNode EnsureNode(Func<IWorkspaceNode> node, string id)
        {
            return Current.As<IWorkspaceNodeGroupEditor<string>>().AddOrGet(node, id);
        }

        /// <summary>
        /// Make sure global node is present.
        /// </summary>
        /// <param name="id">Node id.</param>
        /// <returns>Node.</returns>
        public static IWorkspaceNode EnsureNode<T>(string id) where T : IWorkspaceNode, new()
        {
            return Current.As<IWorkspaceNodeGroupEditor<string>>().AddOrGet(() => new T(), id);
        }
    }
}