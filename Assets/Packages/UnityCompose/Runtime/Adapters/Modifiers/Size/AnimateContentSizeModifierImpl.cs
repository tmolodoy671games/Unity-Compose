// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace UnityCompose;

internal class AnimateContentSizeModifierImpl : BaseModifier<AnimateContentSizeModifierImpl>
{
    private record AnimationRecord(
        ValueAnimation<float> Animation,
        Vector2 TargetSize
    );
    
    private readonly AnimationSpec _animationSpec;
    private readonly EventCallback<GeometryChangedEvent> _callback;

    public AnimateContentSizeModifierImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec;
        _callback = OnGeometryChanged;
    }

    public override void Apply(IReusableComposeNode node)
    {
        var contentContainer = node.CastTo<UnityReusableComposeNode>().SetupContentContainer();
        contentContainer.RegisterCallback(_callback);
    }

    public override void Revert(IReusableComposeNode node)
    {
        var unityNode = node.CastTo<UnityReusableComposeNode>();
        var contentContainer = unityNode.SetupContentContainer();
        contentContainer.UnregisterCallback(_callback);
        unityNode.RemoveContentContainer();
    }

    protected override bool Equals(AnimateContentSizeModifierImpl other)
    {
        return _animationSpec.Equals(other._animationSpec);
    }
    
    private void OnGeometryChanged(GeometryChangedEvent e)
    {
        var content = e.VisualElement();
        content.style.position = Position.Absolute;
            var targetWidth = content.resolvedStyle.width
                              + content.resolvedStyle.marginLeft
                              + content.resolvedStyle.marginRight
                              + content.parent.resolvedStyle.paddingLeft
                              + content.parent.resolvedStyle.paddingRight;
            var targetHeight = content.resolvedStyle.height
                               + content.resolvedStyle.marginTop
                               + content.resolvedStyle.marginBottom
                               + content.parent.resolvedStyle.paddingTop
                               + content.parent.resolvedStyle.paddingBottom;
            var targetSize = new Vector2(targetWidth, targetHeight);
            var previousRecord = content.UserData().GetOrNull(this)?.CastToOrNull<AnimationRecord>();
            if (previousRecord != null && previousRecord.TargetSize == targetSize)
                return;
            previousRecord?.Animation.Stop();
            var initialWidth = content.parent.resolvedStyle.width;
            var initialHeight = content.parent.resolvedStyle.height;
            content.UserData()[this] = new AnimationRecord(
                TargetSize: targetSize,
                Animation: content.parent.experimental.animation.Start(
                    0,
                    1,
                    _animationSpec.TotalDuration.TotalMilliseconds.ToFloat().ToInt(),
                    (_, progress) =>
                    {
                        progress = _animationSpec.GetProgress(_animationSpec.TotalDuration * progress);
                        content.parent.style.width = Mathf.LerpUnclamped(
                            initialWidth,
                            targetWidth,
                            progress
                        );

                        content.parent.style.height = Mathf.LerpUnclamped(
                            initialHeight,
                            targetHeight,
                            progress
                        );
                    }
                ).KeepAlive()
            );
    }
}
