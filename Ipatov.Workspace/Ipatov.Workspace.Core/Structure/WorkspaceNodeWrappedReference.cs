namespace Ipatov.Workspace
{
    /// <summary>
    /// Simple wrapped node reference.
    /// </summary>
    public sealed class WorkspaceNodeWrappedReference : WorkspaceNodeExtenderBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="insideObject">Wrapped node.</param>
        public WorkspaceNodeWrappedReference(IWorkspaceNode insideObject) : base(insideObject)
        {
        }

        /// <summary>
        /// Get typed workspace node view.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <returns>Typed view.</returns>
        public override T GetView<T>()
        {
            return InsideObject.As<T>();
        }
    }
}