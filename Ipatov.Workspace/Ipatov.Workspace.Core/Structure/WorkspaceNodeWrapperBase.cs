namespace Ipatov.Workspace
{
    /// <summary>
    /// Wrapper node base.
    /// </summary>
    public abstract class WorkspaceNodeWrapperBase<TInside> : IWorkspaceNodeViewProvider
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="insideObject">Wrapped object.</param>
        protected WorkspaceNodeWrapperBase(TInside insideObject)
        {
            InsideObject = insideObject;
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public IWorkspaceNode Parent => GetParent();

        /// <summary>
        /// Wrapped object.
        /// </summary>
        protected TInside InsideObject { get; }

        /// <summary>
        /// Create node parent from wrapped object's parent or get a static well-known parent.
        /// </summary>
        /// <returns>Node parent.</returns>
        protected abstract IWorkspaceNode GetParent();

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