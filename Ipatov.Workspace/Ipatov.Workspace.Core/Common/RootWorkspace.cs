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
    }
}