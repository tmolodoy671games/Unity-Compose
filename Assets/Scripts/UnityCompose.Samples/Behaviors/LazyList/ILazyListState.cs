using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.LazyList;

public interface ILazyListState : IScrollState
{
    void ScrollToItem(int index, float scrollOffset = 0);

    Task AnimateScrollToItem(
        int index,
        CancellationToken token,
        float scrollOffset = 0,
        Optional<AnimationSpec> animationSpec = default
    );
}

internal interface IMutableLazyListState : ILazyListState
{
    IStableList<LazyListItem> Items { get; }

    void AddItem(object? key, ComposableContent content);
    void Clear();
    void SyncPosition(int index, float offset);
}

internal readonly record struct LazyListItem(
    int Index,
    object? Key,
    ComposableContent Content
);

internal class LazyListStateImpl : IMutableLazyListState
{
    private readonly IMutableStateList<LazyListItem> _items = MutableStateListOf<LazyListItem>();
    private readonly IMutableStableList<float> _offsets = MutableStateListOf<float>();
    private readonly IMutableState<float> _value = MutableStateOf(0f);
    private float _viewportSize;
    private float _contentSize;

    public float Value => _value.Value;

    public float ViewportSize
    {
        get => _viewportSize;
        set
        {
            if (_viewportSize.AlmostEquals(value))
                return;
            _viewportSize = value;
            _value.Value = Clamp(_value.Value);
        }
    }

    public float ContentSize
    {
        get => _contentSize;
        set
        {
            if (_contentSize.AlmostEquals(value))
                return;
            _contentSize = value;
            _value.Value = Clamp(_value.Value);
        }
    }

    public IStableList<LazyListItem> Items => _items;

    private float MaxValue => _contentSize > _viewportSize ? _contentSize - _viewportSize : 0f;

    public void ScrollTo(float value)
    {
        // Debug.Log($"{value} vs {Clamp(value)}: {MaxValue}");
        _value.Value = Clamp(value);
    }

    public Task AnimateScrollTo(
        float value,
        CancellationToken token,
        Optional<AnimationSpec> animationSpec
    )
    {
        return Animate(
            typeConverter: Mathf.LerpUnclamped,
            initialValue: _value.Value,
            targetValue: Clamp(value),
            token: token,
            animationSpec: animationSpec,
            block: ScrollTo
        );
    }

    public Task AnimateScrollToItem(
        int index,
        CancellationToken token,
        float scrollOffset = 0,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        var itemOffset = _offsets.GetOrDefault(index, float.NaN);
        if (float.IsNaN(itemOffset))
            return Task.CompletedTask;
        itemOffset += scrollOffset;
        return AnimateScrollTo(itemOffset, token, animationSpec);
    }

    public void ScrollToItem(int index, float scrollOffset)
    {
        var itemOffset = _offsets.GetOrDefault(index, float.NaN);
        if (float.IsNaN(itemOffset))
            return;
        itemOffset += scrollOffset;
        ScrollTo(itemOffset);
    }

    public void AddItem(object? key, ComposableContent content)
    {
        _items.Add(new LazyListItem(_items.Count, key, content));
        _offsets.Add(float.NaN);
    }

    public void Clear()
    {
        _items.Clear();
        _offsets.Clear();
    }

    public void SyncPosition(int index, float offset)
    {
        _offsets[index] = offset;
    }

    private float Clamp(float value)
    {
        return value.Clamp(0, MaxValue);
    }
}