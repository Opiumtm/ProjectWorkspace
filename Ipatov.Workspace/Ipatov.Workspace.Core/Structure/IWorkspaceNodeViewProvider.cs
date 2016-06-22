namespace Ipatov.Workspace
{
    /// <summary>
    /// Node view provider.
    /// </summary>
    public interface IWorkspaceNodeViewProvider : IWorkspaceNode
    {
        /// <summary>
        /// Get typed workspace node view.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <returns>Typed view.</returns>
        T GetView<T>() where T : class, IWorkspaceNode;
    }
}