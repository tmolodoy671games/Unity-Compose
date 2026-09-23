using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class BoxNodeFactoryImpl : IBoxNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Box());
    }

    public void Apply(IReusableComposeNode node, Alignment alignment)
    {
        var box = node.VisualElement<Box>();
        box.style.alignItems = alignment.ToAlign();
        box.style.justifyContent = alignment.ToJustify();
    }
}