// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace UnityCompose;

internal class AnimateContentSizeModifierImpl : UnityModifier<AnimateContentSizeModifierImpl>
{
    private record AnimationRecord(
        ValueAnimation<float> Animation,
        Vector2 TargetSize
    );

    private readonly AnimationSpec _animationSpec;
    private readonly EventCallback<GeometryChangedEvent> _callback;
    private readonly ReferenceKey _key;

    public AnimateContentSizeModifierImpl(AnimationSpec animationSpec)
    {
        _key = new ReferenceKey(this);
        _animationSpec = animationSpec;
        _callback = OnGeometryChanged;
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var contentContainer = node.CastTo<UnityReusableComposeNode>().SetupContentContainer();
        contentContainer.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
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

    public override int GetHashCode()
    {
        return HashCode.Combine(_animationSpec);
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
        var previousRecord = content.UserData().GetOrNull(_key)?.CastToOrNull<AnimationRecord>();
        if (previousRecord != null && previousRecord.TargetSize == targetSize)
            return;
        previousRecord?.Animation.Stop();
        var initialWidth = content.parent.resolvedStyle.width;
        var initialHeight = content.parent.resolvedStyle.height;
        content.UserData()[_key] = new AnimationRecord(
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