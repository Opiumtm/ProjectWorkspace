using System;

namespace Ipatov.Workspace.ServiceHelpers
{
    /// <summary>
    /// Global services container.
    /// </summary>
    public interface IGlobalServices : IWorkspaceNodeId<string>, IWorkspaceNodeGroupEditor<Type>
    {         
    }
}