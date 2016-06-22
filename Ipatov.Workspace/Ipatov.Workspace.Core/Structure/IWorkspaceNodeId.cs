namespace Ipatov.Workspace
{
    /// <summary>
    /// Workspace node identifier.
    /// </summary>
    /// <typeparam name="TId">Node identifier type.</typeparam>
    public interface IWorkspaceNodeId<out TId> : IWorkspaceNode
    {
        /// <summary>
        /// Node identifier.
        /// </summary>
        TId NodeId { get; }
    }
}