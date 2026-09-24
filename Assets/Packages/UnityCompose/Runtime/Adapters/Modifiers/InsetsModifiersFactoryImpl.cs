using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class InsetsModifiersFactoryImpl : IInsetsModifiersFactory
{
    public IModifier Margin(Optional<Dp> top, Optional<Dp> bottom, Optional<Dp> left, Optional<Dp> right)
    {
        return new MarginModifierImpl(top, bottom, left, right);
    }

    public IModifier Padding(Optional<Dp> top, Optional<Dp> bottom, Optional<Dp> left, Optional<Dp> right)
    {
        return new PaddingModifierImpl(top, bottom, left, right);
    }
}