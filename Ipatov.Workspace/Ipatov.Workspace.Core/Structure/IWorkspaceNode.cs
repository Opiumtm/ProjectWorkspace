namespace Ipatov.Workspace
{
    /// <summary>
    /// Workspace hierarchy node.
    /// </summary>
    public interface IWorkspaceNode
    {
        /// <summary>
        /// Parent node.
        /// </summary>
        IWorkspaceNode Parent { get; }
    }
}