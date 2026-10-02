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
    private readonly AnimationSpec _animationSpec;

    public AnimateContentSizeModifierImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec;
    }

    public override void Apply(IReusableComposeNode node)
    {
        node.CastTo<UnityReusableComposeNode>().SetContentContainer(new AnimatedSizeContent().Init(_animationSpec));
    }

    public override void Revert(IReusableComposeNode node)
    {
        node.CastTo<UnityReusableComposeNode>().RemoveContentContainer();
    }

    protected override bool Equals(AnimateContentSizeModifierImpl other)
    {
        return _animationSpec.Equals(other._animationSpec);
    }
}

internal class AnimatedSizeContent : VisualElement
{
    private record AnimationRecord(
        ValueAnimation<float> Animation,
        Vector2 TargetSize
    );

    private bool _isInitialized;

    public AnimatedSizeContent Init(AnimationSpec animationSpec)
    {
        RegisterCallback<GeometryChangedEvent>(_ =>
        {
            style.position = Position.Absolute;
            var targetWidth = resolvedStyle.width
                              + resolvedStyle.marginLeft
                              + resolvedStyle.marginRight
                              + parent.resolvedStyle.paddingLeft
                              + parent.resolvedStyle.paddingRight;
            var targetHeight = resolvedStyle.height
                               + resolvedStyle.marginTop
                               + resolvedStyle.marginBottom
                               + parent.resolvedStyle.paddingTop
                               + parent.resolvedStyle.paddingBottom;
            var targetSize = new Vector2(targetWidth, targetHeight);
            var previousRecord = this.UserData().GetOrNull(this)?.CastToOrNull<AnimationRecord>();
            if (previousRecord != null && previousRecord.TargetSize == targetSize)
                return;
            previousRecord?.Animation.Stop();
            var initialWidth = parent.resolvedStyle.width;
            var initialHeight = parent.resolvedStyle.height;
            this.UserData()[this] = new AnimationRecord(
                TargetSize: targetSize,
                Animation: parent.experimental.animation.Start(
                    0,
                    1,
                    animationSpec.TotalDuration.TotalMilliseconds.ToFloat().ToInt(),
                    (_, progress) =>
                    {
                        progress = animationSpec.GetProgress(animationSpec.TotalDuration * progress);
                        parent.style.width = Mathf.LerpUnclamped(
                            initialWidth,
                            targetWidth,
                            progress
                        );

                        parent.style.height = Mathf.LerpUnclamped(
                            initialHeight,
                            targetHeight,
                            progress
                        );
                    }
                ).KeepAlive()
            );
        });
        return this;
    }
}