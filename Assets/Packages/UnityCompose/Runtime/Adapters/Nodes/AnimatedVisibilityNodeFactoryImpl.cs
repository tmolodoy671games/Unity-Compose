using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class AnimatedVisibilityNodeFactoryImpl : IAnimatedVisibilityNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new AnimatedVisibility());
    }

    public IReusableComposeNode CreateContentWrapperNode()
    {
        return new UnityReusableComposeNode(new TransitionContent());
    }
}

internal class TransitionContent : VisualElement
{
    public TransitionContent()
    {
        RegisterCallback<AttachToPanelEvent>(_ =>
        {
            if (contentRect.height > 0 || contentRect.width > 0)
                SyncParentSize();
        });
        RegisterCallback<GeometryChangedEvent>(_ => SyncParentSize());
    }

    private void SyncParentSize()
    {
        parent.style.width = contentRect.width;
        parent.style.height = contentRect.height;
        style.position = Position.Absolute;
    }
}

internal class AnimatedVisibility : VisualElement
{
}