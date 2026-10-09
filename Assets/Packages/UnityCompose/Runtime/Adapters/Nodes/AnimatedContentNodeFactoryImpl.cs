using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class AnimatedContentNodeFactoryImpl : IAnimatedContentNodeFactory
{
    public IReusableComposeNode CreateNode() => new UnityReusableComposeNode(new AnimatedContent());

    public void Apply(IReusableComposeNode node, Alignment alignment)
    {
        var element = node.VisualElement();
        element.style.alignItems = alignment.ToAlign();
        element.style.justifyContent = alignment.ToJustify();
    }
}

internal class AnimatedContent : VisualElement
{
}