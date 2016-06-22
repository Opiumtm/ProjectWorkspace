namespace Ipatov.Workspace
{
    /// <summary>
    /// Node group editor.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public interface IWorkspaceNodeGroupEditor<in TId> : IWorkspaceNodeGroup<TId>
    {
        /// <summary>
        /// Add child node.
        /// </summary>
        /// <param name="node">Node.</param>
        void Add(IWorkspaceNode node);

        /// <summary>
        /// Add child node with identifier.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="id">Identifier.</param>
        void Add(IWorkspaceNode node, TId id);

        /// <summary>
        /// Remove child node.
        /// </summary>
        /// <param name="id">Identifier.</param>
        void Remove(TId id);

        /// <summary>
        /// Clear collection.
        /// </summary>
        void Clear();
    }
}