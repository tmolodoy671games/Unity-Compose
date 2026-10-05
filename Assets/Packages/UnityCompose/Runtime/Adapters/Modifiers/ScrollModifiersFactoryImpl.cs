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