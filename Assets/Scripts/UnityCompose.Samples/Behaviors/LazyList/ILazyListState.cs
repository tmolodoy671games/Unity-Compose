using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.LazyList;

public interface ILazyListState : IScrollState
{
    void ScrollToItem(int index, float scrollOffset = 0);
}

internal partial interface IMutableLazyListState : ILazyListState
{
    IStableList<LazyListItem> Items { get; }

    void AddItem(object? key, ComposableContent content);
    void Clear();
    void SyncPosition(int index, ILayoutCoordinates layout);
}

internal readonly record struct LazyListItem(
    int Index,
    object? Key,
    ComposableContent Content
);

internal partial class LazyListStateImpl : IMutableLazyListState
{
    public float Value { get; }
    public float ViewportSize { get; set; }
    public float ContentSize { get; set; }
    public IStableList<LazyListItem> Items { get; }

    public void ScrollTo(float value)
    {
        throw new System.NotImplementedException();
    }

    public Task AnimateScrollTo(
        float value,
        CancellationToken token,
        Optional<AnimationSpec> animationSpec
    )
    {
        throw new System.NotImplementedException();
    }

    public void ScrollToItem(int index, float scrollOffset)
    {
        throw new System.NotImplementedException();
    }

    public void AddItem(object? key, ComposableContent content)
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public void SyncPosition(int index, ILayoutCoordinates layout)
    {
        throw new System.NotImplementedException();
    }
}