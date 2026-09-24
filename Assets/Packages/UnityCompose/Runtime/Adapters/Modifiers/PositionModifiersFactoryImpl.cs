using System;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class PositionModifiersFactoryImpl : IPositionModifiersFactory
{
    public IModifier OnGloballyPositioned(Action<LayoutCoordinates> onGloballyPositioned)
    {
        return new OnGloballyPositionedModifierImpl(onGloballyPositioned);
    }

    public IModifier OnLocallyPositioned(Action<LayoutCoordinates> onLocallyPositioned)
    {
        return new OnLocallyPositionedModifierImpl(onLocallyPositioned);
    }
}