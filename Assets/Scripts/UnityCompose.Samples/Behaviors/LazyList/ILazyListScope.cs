using System;
using Compose.Net;

namespace UnityCompose.Samples.Behaviors.LazyList;

public partial interface ILazyListScope
{
    void Item(
        ComposableContent content,
        object? key = null
    );

    void Items(
        int count,
        ComposableContent<int> content,
        Func<int, object?>? key = null
    );
}

internal partial class LazyListScopeImpl : ILazyListScope
{
    private readonly IMutableLazyListState _state;

    public LazyListScopeImpl(IMutableLazyListState state)
    {
        _state = state;
    }

    public void Item(ComposableContent content, object? key = null)
    {
        _state.AddItem(key, content);
    }

    public void Items(int count, ComposableContent<int> content, Func<int, object?>? key = null)
    {
        for (var i = 0; i < count; i++)
        {
            var j = i;
            _state.AddItem(key?.Invoke(i), () => content(j));
        }
    }
}