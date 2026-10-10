using Compose.Net;
using UnityEngine.UIElements;
// ReSharper disable CheckNamespace

namespace UnityCompose;

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

public class Content : VisualElement
{
}

public class Item : VisualElement {}