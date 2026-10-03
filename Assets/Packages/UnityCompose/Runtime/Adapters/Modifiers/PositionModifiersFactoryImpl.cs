using System;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class PositionModifiersFactoryImpl : IPositionModifiersFactory
{
    public IModifier OnGloballyPositioned(Action<ILayoutCoordinates> onGloballyPositioned)
    {
        return new OnGloballyPositionedModifierImpl(onGloballyPositioned);
    }

    public IModifier OnLocallyPositioned(Action<ILayoutCoordinates> onLocallyPositioned)
    {
        return new OnLocallyPositionedModifierImpl(onLocallyPositioned);
    }
}