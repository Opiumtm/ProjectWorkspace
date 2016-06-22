namespace Ipatov.Workspace
{
    /// <summary>
    /// Simple workspace node base class.
    /// </summary>
    public abstract class WorkspaceNodeBase<TParent> : IWorkspaceNodeViewProvider where TParent : class, IWorkspaceNode
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">Parent node (may be null).</param>
        protected WorkspaceNodeBase(TParent parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public IWorkspaceNode Parent { get; }

        /// <summary>
        /// Get typed workspace node view.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <returns>Typed view.</returns>
        public virtual T GetView<T>() where T : class, IWorkspaceNode
        {
            return this as T;
        }
    }
}