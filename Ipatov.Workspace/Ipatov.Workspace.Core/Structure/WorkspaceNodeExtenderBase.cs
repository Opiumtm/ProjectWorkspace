using System;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Node extender base (can override wrapped node views or extend it with new views).
    /// </summary>
    public abstract class WorkspaceNodeExtenderBase : WorkspaceNodeWrapperBase<IWorkspaceNode>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="insideObject">Wrapped node.</param>
        protected WorkspaceNodeExtenderBase(IWorkspaceNode insideObject) : base(insideObject)
        {
            if (insideObject == null) throw new ArgumentNullException(nameof(insideObject));
        }

        /// <summary>
        /// Create node parent from wrapped object's parent or get a static well-known parent.
        /// </summary>
        /// <returns>Node parent.</returns>
        protected override IWorkspaceNode GetParent()
        {
            return InsideObject.Parent;
        }

        /// <summary>
        /// Get typed workspace node view.
        /// </summary>
        /// <typeparam name="T">View type.</typeparam>
        /// <returns>Typed view.</returns>
        public override T GetView<T>()
        {
            return base.GetView<T>() ?? InsideObject.As<T>();
        }
    }
}