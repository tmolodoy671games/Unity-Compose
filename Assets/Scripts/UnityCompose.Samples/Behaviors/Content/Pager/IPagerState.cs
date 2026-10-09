using System;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using StableCollections;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

public interface IPagerState
{
    int CurrentPage { get; }
    int PageCount { get; }
    void ScrollToPage(int page, float offsetFraction = 0f);

    Task AnimateScrollToPage(
        ICoroutineScope scope,
        int page,
        float offsetFraction = 0f,
        Optional<AnimationSpec> animationSpec = default
    );
}

internal interface IMutablePagerState : IPagerState
{
    void SubscribeToValueChange(Action<float> onValueChanged);
    float Value { get; }
    void SyncPageSize(float size);
    void SyncViewportSize(float size);
    void SyncContentPadding(float padding);
    void SyncPageSpacing(float pageSpacing);
}

internal class MutablePagerStateImpl(
    Func<int> pageCount,
    int initialPage
) : IMutablePagerState, IComposeDisposable
{
    private float _viewportSize = float.NaN;
    private float _pageSize = float.NaN;
    private float _padding = float.NaN;
    private float _pageSpacing = float.NaN;

    private readonly IMutableState<float> _value = MutableStateOf(0f);
    private int _animationTarget = initialPage;
    private int _pendingPage = initialPage;
    private CancellationTokenSource _animateScrollTokenSource = new();
    private bool _isDisposed;
    private Action<float> _onValueChanged = _ => { };

    public int CurrentPage { get; private set; }
    public int PageCount => pageCount();

    public void ScrollToPage(int page, float offsetFraction = 0)
    {
        CurrentPage = page;
        var newValue = CalculateOffset(page);
        Debug.Log(newValue);
        if (float.IsNaN(newValue))
        {
            _pendingPage = page;
            return;
        }

        _pendingPage = -1;
        _value.Value = newValue;
        _onValueChanged(newValue);
    }

    public async Task AnimateScrollToPage(
        ICoroutineScope scope,
        int page,
        float offsetFraction = 0f,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        if (_animationTarget == page)
            return;
        CurrentPage = page;
        var newValue = CalculateOffset(page);
        if (float.IsNaN(newValue))
        {
            _pendingPage = page;
            return;
        }

        _animationTarget = page;
        _pendingPage = -1;
        _animateScrollTokenSource.Cancel();
        _animateScrollTokenSource.Dispose();
        _animateScrollTokenSource = CancellationTokenSource.CreateLinkedTokenSource(scope.CancellationToken);
        await Animate(
            scope: scope,
            typeConverter: Mathf.LerpUnclamped,
            initialValue: _value.Value,
            targetValue: newValue,
            animationSpec: animationSpec,
            block: it =>
            {
                _value.Value = it;
                _onValueChanged(it);
            }
        );
    }

    public void SubscribeToValueChange(Action<float> onValueChanged)
    {
        _onValueChanged = onValueChanged;
    }

    public float Value => _value.Value;

    public void SyncPageSize(float size)
    {
        _pageSize = size;
        ExecutePendingScroll();
    }

    public void SyncViewportSize(float size)
    {
        _viewportSize = size;
        ExecutePendingScroll();
    }

    public void SyncContentPadding(float padding)
    {
        _padding = padding;
        ExecutePendingScroll();
    }

    public void SyncPageSpacing(float pageSpacing)
    {
        _pageSpacing = pageSpacing;
        ExecutePendingScroll();
    }

    private float CalculateOffset(int page)
    {
        var result = 0f;
        if (Uninitialized(_pageSize))
            return float.NaN;

        result += _pageSize * (page.ToFloat() + 0.5f);
        
        if (Uninitialized(_pageSpacing))
            return float.NaN;

        result += _pageSpacing * (page).Clamp(0, int.MaxValue);
        
        if (Uninitialized(_viewportSize))
            return float.NaN;

        result -= _viewportSize / 2;

        if (Uninitialized(_padding))
            return float.NaN;

        result += _padding;

        return result;
    }

    private void ExecutePendingScroll()
    {
        if (_pendingPage < 0)
        {
            return;
        }

        ScrollToPage(_pendingPage);
    }

    private static bool Uninitialized(float size)
    {
        return float.IsNaN(size);
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        _animateScrollTokenSource.Cancel();
        _animateScrollTokenSource.Dispose();
    }
}