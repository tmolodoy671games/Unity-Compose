using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class InteractionModifiersFactoryImpl : IInteractionModifiersFactory
{
    public IModifier Clickable(IMutableInteractionSource interactionSource)
    {
        return new ClickableModifierImpl(interactionSource);
    }

    public IModifier Hoverable(IMutableInteractionSource interactionSource)
    {
        return new HoverableModiferImpl(interactionSource);
    }
}