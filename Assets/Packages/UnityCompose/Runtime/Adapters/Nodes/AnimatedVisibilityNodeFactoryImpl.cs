using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class AnimatedVisibilityNodeFactoryImpl : IAnimatedVisibilityNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new AnimatedVisibility());
    }

    public void Apply(IReusableComposeNode node, IEnterTransition enterTransition, TimeSpan timeElapsed)
    {
        var element = node.VisualElement();
        var childNode = element.contentContainer.GetReusableComposeNode()!;
        enterTransition.Apply(childNode, timeElapsed);
    }

    public void Apply(IReusableComposeNode node, IExitTransition exitTransition, TimeSpan timeElapsed)
    {
        var element = node.VisualElement();
        var childNode = element.contentContainer.GetReusableComposeNode()!;
        exitTransition.Apply(childNode, timeElapsed);
    }
}

internal class AnimatedVisibility : VisualElement
{
    private readonly Content _wrapper;
    private readonly UnityReusableComposeNode _wrapperNode;

    public AnimatedVisibility()
    {
        _wrapper = new Content();
        _wrapperNode = new UnityReusableComposeNode(_wrapper);
        hierarchy.Add(_wrapper);
    }

    public override VisualElement contentContainer => _wrapper;

    public void Apply(IEnterTransition transition, TimeSpan timeElapsed)
    {
        transition.Apply(_wrapperNode, timeElapsed);
    }

    public void Apply(IExitTransition transition, TimeSpan timeElapsed)
    {
        transition.Apply(_wrapperNode, timeElapsed);
    }
}