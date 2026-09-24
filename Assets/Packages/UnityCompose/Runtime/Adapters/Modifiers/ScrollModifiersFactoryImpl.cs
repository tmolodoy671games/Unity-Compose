using System;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class ScrollModifiersFactoryImpl : IScrollModifiersFactory
{
    public IModifier OnScroll(Action<Offset> onScroll)
    {
        return new OnScrollModifierImpl(onScroll: onScroll);
    }

    public IModifier OnVerticalScroll(Action<float> onVerticalScroll)
    {
        return new OnScrollModifierImpl(onVerticalScroll: onVerticalScroll);
    }

    public IModifier OnHorizontalScroll(Action<float> onHorizontalScroll)
    {
        return new OnScrollModifierImpl(onHorizontalScroll: onHorizontalScroll);
    }
}