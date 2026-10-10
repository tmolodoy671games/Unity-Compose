using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class WrapperNodeFactoryImpl : IWrapperNodeFactory
{
    public IReusableComposeNode CreateContentNode()
    {
        return new UnityReusableComposeNode(new Content());
    }

    public IReusableComposeNode CreateItemNode()
    {
        return new UnityReusableComposeNode(new Item());
    }
}

internal class Content : VisualElement
{
}

internal class Item : VisualElement {}