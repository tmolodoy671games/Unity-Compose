using System;
using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class ScrollModifiersFactoryImpl : IScrollModifiersFactory
{
    public IModifier OnScroll(Action<Offset> onScroll)
    {
        return new OnScrollModifierImpl(onScroll, null, null);
    }

    public IModifier OnVerticalScroll(Action<float> onVerticalScroll)
    {
        return new OnScrollModifierImpl(null, null, onVerticalScroll);
    }

    public IModifier OnHorizontalScroll(Action<float> onHorizontalScroll)
    {
        return new OnScrollModifierImpl(null, onHorizontalScroll, null);
    }

    public IModifier VerticalScroll(
        IScrollState state,
        float scrollMultiplier,
        bool reverseScrolling,
        bool userScrollEnabled,
        IMutableInteractionSource? interactionSource
    )
    {
        return new VerticalScrollModifierImpl(state, scrollMultiplier, reverseScrolling, userScrollEnabled,
            interactionSource);
    }

    public IModifier HorizontalScroll(
        IScrollState state,
        float scrollMultiplier,
        bool reverseScrolling,
        bool userScrollEnabled,
        IMutableInteractionSource? interactionSource
    )
    {
        return new HorizontalScrollModifierImpl(state, scrollMultiplier, reverseScrolling, userScrollEnabled,
            interactionSource);
    }
}