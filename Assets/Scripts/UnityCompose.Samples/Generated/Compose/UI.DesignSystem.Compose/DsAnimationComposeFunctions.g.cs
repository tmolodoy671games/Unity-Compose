#nullable enable
using SharpExtensions;
using UI.DesignSystem.Compose.Players;
using UnityCompose;
using System;
using static UnityCompose.ComposeFunctions;

namespace UI.DesignSystem.Compose;
public static partial class DesignSystemComposeFunctions
{
    [Composable]
    private static ISingleAnimationPlayer __RememberSingleAnimation(Optional<AnimationSpec> animationSpec = default, bool debuggable = false)
    {
        var __composer = CurrentComposer;
        return !__composer.Changed() ? __composer.RememberedValue<UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>() : __composer.UpdateRememberedValue<UI.DesignSystem.Compose.Players.SingleAnimationPlayerImpl>(() =>
        {
            var result = new SingleAnimationPlayerImpl(animationSpec.GetOrDefault(), debuggable);
            return result;
        });
    }
}