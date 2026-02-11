#nullable enable
using System;
using SharpExtensions;
using UI.DesignSystem.Compose.Players;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UI.DesignSystem.Compose;
public static partial class DesignSystemComposeFunctions
{
    private static ISingleAnimationPlayer __RememberSingleAnimation(Optional<AnimationSpec> animationSpec = default, bool debuggable = false, global::UnityCompose.Composer __composer = null !)
    {
        return (!__composer.Changed() ? __composer.RememberedValue<UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>() : __composer.UpdateRememberedValue<UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>(() =>
        {
            var result = new SingleAnimationPlayerImpl(animationSpec.GetOrDefault(), debuggable);
            return result;
        }));
    }

    private static ISingleAnimationPlayer __RememberSingleAnimation(Optional<AnimationSpec> animationSpec = default, bool debuggable = false)
    {
        return __RememberSingleAnimation(animationSpec, debuggable, CurrentComposer);
    }

    private static IState<float> __VisibilityProgress(Optional<AnimationSpec> appearAnimationSpec = default, Optional<AnimationSpec> disappearAnimationSpec = default, global::UnityCompose.Composer __composer = null !)
    {
        var defaultDuration = 0.5f;
        var resolvedAnimationSpec = appearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var resolvedBackwardAnimationSpec = disappearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var isVisible = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
        var forwardState = AnimateFloatAsState(isVisible.Value.ToInt(), isVisible.Value ? resolvedAnimationSpec : resolvedBackwardAnimationSpec);
        var state = LocalTransitionState.Current;
        isVisible.Value = state switch
        {
            TransitionState.Entering => true,
            TransitionState.Idle => true,
            TransitionState.Exiting => false,
            _ => throw new ArgumentOutOfRangeException()};
        ;
        return forwardState;
    }

    private static IState<float> __VisibilityProgress(Optional<AnimationSpec> appearAnimationSpec = default, Optional<AnimationSpec> disappearAnimationSpec = default)
    {
        return __VisibilityProgress(appearAnimationSpec, disappearAnimationSpec, CurrentComposer);
    }
}