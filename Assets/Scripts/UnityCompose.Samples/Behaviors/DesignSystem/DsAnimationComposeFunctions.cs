using System;
using SharpExtensions;
using UI.DesignSystem.Compose.Players;
using UnityCompose;

namespace UI.DesignSystem.Compose;

public static partial class DesignSystemComposeFunctions
{
    [Composable]
    public static ISingleAnimationPlayer RememberSingleAnimation(
        Optional<AnimationSpec> animationSpec = default,
        bool debuggable = false
    )
    {
        return Remember(() =>
        {
            var result = new SingleAnimationPlayerImpl(animationSpec.GetOrDefault(), debuggable);
            return result;
        });
    }
    
    [Composable]
    public static IState<float> VisibilityProgress(
        Optional<AnimationSpec> appearAnimationSpec = default,
        Optional<AnimationSpec> disappearAnimationSpec = default
    )
    {
        var defaultDuration = 0.5f;
        var resolvedAnimationSpec = appearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var resolvedBackwardAnimationSpec = disappearAnimationSpec.GetOrDefault(Tween(duration: defaultDuration));
        var isVisible = Remember(() => MutableStateOf(false));
        var forwardState = AnimateFloatAsState(
            isVisible.Value.ToInt(),
            isVisible.Value ? resolvedAnimationSpec : resolvedBackwardAnimationSpec
        );
        var state = LocalTransitionState.Current;
        isVisible.Value = state switch
        {
            TransitionState.Entering => true,
            TransitionState.Idle => true,
            TransitionState.Exiting => false,
            _ => throw new ArgumentOutOfRangeException()
        };;
        return forwardState;
    }
}