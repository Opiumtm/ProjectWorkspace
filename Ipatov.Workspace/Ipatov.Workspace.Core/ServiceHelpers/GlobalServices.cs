using System;

namespace Ipatov.Workspace.ServiceHelpers
{
    /// <summary>
    /// Global services container.
    /// </summary>
    public sealed class GlobalServices : WorkspaceNodeCollection<Type>, IWorkspaceNodeId<string>
    {
        /// <summary>
        /// Global node ID.
        /// </summary>
        public const string NodeId = "GlobalServices";

        /// <summary>
        /// Constructor.
        /// </summary>
        public GlobalServices() : base(RootWorkspace.Current)
        {
        }

        /// <summary>
        /// Node identifier.
        /// </summary>
        string IWorkspaceNodeId<string>.NodeId => GlobalServices.NodeId;

        /// <summary>
        /// Current instance.
        /// </summary>
        public static IGlobalServices Current => RootWorkspace
            .EnsureNode<GlobalServices>(NodeId)
            .As<IGlobalServices>();

        /// <summary>
        /// Register global service.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public static void Register<T>(T service) where T : class, IWorkspaceNode
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            Current.Add(service, typeof(T));
        }

        /// <summary>
        /// Get global service.
        /// </summary>
        /// <typeparam name="T">Global service type.</typeparam>
        /// <returns>Global servie.</returns>
        public static T Get<T>() where T : class, IWorkspaceNode
        {
            return Current.GetChildNode(typeof (T)).As<T>();
        }

        /// <summary>
        /// Get global service.
        /// </summary>
        /// <typeparam name="T">Global service type.</typeparam>
        /// <returns>Global servie.</returns>
        public static T GetOrThrow<T>() where T : class, IWorkspaceNode
        {
            return Get<T>().ThrowIfNotSupported();
        }
    }
}