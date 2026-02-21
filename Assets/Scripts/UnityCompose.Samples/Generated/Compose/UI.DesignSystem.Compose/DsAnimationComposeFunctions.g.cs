#nullable enable
using System;
using SharpExtensions;
using UI.DesignSystem.Compose.Players;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UI.DesignSystem.Compose;
public static partial class DesignSystemComposeFunctions
{
    public static ISingleAnimationPlayer __RememberSingleAnimation(Optional<AnimationSpec> animationSpec = default, bool debuggable = false, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(debuggable) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        return (!__composer.Changed() ? __composer.RememberedValue<global::UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>() : __composer.UpdateRememberedValue<global::UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>(() =>
        {
            var result = new SingleAnimationPlayerImpl(animationSpec.GetOrDefault(), debuggable);
            return result;
        }));
    }

    public static IState<float> __VisibilityProgress(Optional<AnimationSpec> appearAnimationSpec = default, Optional<AnimationSpec> disappearAnimationSpec = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.Changed(appearAnimationSpec) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(disappearAnimationSpec) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var defaultDuration = 0.5f;
        var resolvedAnimationSpec = appearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var resolvedBackwardAnimationSpec = disappearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var isVisible = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
        var forwardState = __AnimateFloatAsState(isVisible.Value.ToInt(), isVisible.Value ? resolvedAnimationSpec : resolvedBackwardAnimationSpec, __composer: __composer, __changed: 0b_01_00_00);
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
}