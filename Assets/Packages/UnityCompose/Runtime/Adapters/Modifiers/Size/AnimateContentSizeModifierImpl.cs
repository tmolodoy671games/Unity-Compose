// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace UnityCompose;

internal record AnimateContentSizeModifierImpl(
    AnimationSpec AnimationSpec
) : ContentContainerUnityModifier
{
    private record AnimationRecord(
        ValueAnimation<float> Animation,
        Vector2 TargetSize
    );

    private readonly EventCallback<GeometryChangedEvent> _callback;

    public AnimateContentSizeModifierImpl(
        AnimationSpec AnimationSpec,
        int _
    ) : this(AnimationSpec)
    {
        var key = new ReferenceKey(this);
        _callback = e =>
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
            var previousRecord = content.UserData().GetOrNull(key)?.CastToOrNull<AnimationRecord>();
            if (previousRecord != null && previousRecord.TargetSize == targetSize)
                return;
            previousRecord?.Animation.Stop();
            var initialWidth = content.parent.resolvedStyle.width;
            var initialHeight = content.parent.resolvedStyle.height;
            content.UserData()[key] = new AnimationRecord(
                TargetSize: targetSize,
                Animation: content.parent.experimental.animation.Start(
                    0,
                    1,
                    AnimationSpec.TotalDuration.TotalMilliseconds.ToFloat().ToInt(),
                    (_, progress) =>
                    {
                        progress = AnimationSpec.GetProgress(AnimationSpec.TotalDuration * progress);
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
        };
    }

    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement contentContainer,
        IStableList<IModifier> newModifiers
    )
    {
        contentContainer.RegisterCallback(_callback);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement contentContainer,
        IStableList<IModifier> newModifiers
    )
    {
        contentContainer.UnregisterCallback(_callback);
    }
}