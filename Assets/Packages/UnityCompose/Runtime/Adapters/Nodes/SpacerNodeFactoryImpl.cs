using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class SpacerNodeFactoryImpl : ISpacerNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Spacer());
    }
}