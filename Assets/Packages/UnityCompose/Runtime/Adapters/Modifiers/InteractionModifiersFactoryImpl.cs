using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class InteractionModifiersFactoryImpl : IInteractionModifiersFactory
{
    public IModifier Pressable(IMutableInteractionSource interactionSource)
    {
        return new PressableModifierImpl(interactionSource);
    }

    public IModifier Hoverable(IMutableInteractionSource interactionSource)
    {
        return new HoverableModiferImpl(interactionSource);
    }
}