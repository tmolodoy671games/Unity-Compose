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
}