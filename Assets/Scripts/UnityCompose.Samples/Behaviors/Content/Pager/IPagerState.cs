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
        int page,
        CancellationToken token,
        float offsetFraction = 0f,
        Optional<AnimationSpec> animationSpec = default
    );
}

internal interface IMutablePagerState : IPagerState
{
    float Value { get; }
    void SyncSize(int page, float size);
    void SyncViewportSize(float size);
    void SyncContentPadding(float padding);
}

internal class MutablePagerStateImpl(
    Func<int> pageCount,
    int initialPage
) : IMutablePagerState, IComposeDisposable
{
    private readonly IMutableStableList<float> _pageSizes = MutableStableListOf<float>().Also(it =>
    {
        var count = pageCount();
        for (var i = 0; i < count; i++)
            it.Add(-1);
    });

    private float _viewportSize = -1;

    private readonly IMutableState<float> _value = MutableStateOf<float>(0f);
    private int _animationTarget = initialPage;
    private int _pendingPage = initialPage;
    private float _padding = -1;
    private CancellationTokenSource _animateScrollTokenSource = new();

    public int CurrentPage { get; private set; }
    public int PageCount => pageCount();

    public void ScrollToPage(int page, float offsetFraction = 0)
    {
        CurrentPage = page;
        var newValue = CalculateOffset(page);
        if (float.IsNaN(newValue))
        {
            _pendingPage = page;
            return;
        }

        _pendingPage = -1;
        _value.Value = newValue;
    }

    public async Task AnimateScrollToPage(
        int page,
        CancellationToken token,
        float offsetFraction = 0,
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
        _animateScrollTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        await Animate(
            typeConverter: Mathf.LerpUnclamped,
            initialValue: _value.Value,
            targetValue: newValue,
            animationSpec: animationSpec,
            token: _animateScrollTokenSource.Token,
            block: it => _value.Value = it
        );
    }

    public float Value => _value.Value;

    public void SyncSize(int page, float size)
    {
        EnsurePageSizes();
        _pageSizes[page] = size;
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

    private void EnsurePageSizes()
    {
        var count = pageCount();
        var pagesToAdd = (count - _pageSizes.Count).Clamp(0, int.MaxValue);
        for (var i = 0; i < pagesToAdd; i++)
            _pageSizes.Add(-1);
        var pagesToRemove = (_pageSizes.Count - count).Clamp(0, int.MaxValue);
        for (var i = 0; i < pagesToRemove; i++)
            _pageSizes.RemoveAt(_pageSizes.LastIndex);
    }

    private float CalculateOffset(int page)
    {
        EnsurePageSizes();
        var result = 0f;
        for (var i = 0; i < page; i++)
        {
            var pageSize = _pageSizes[i];
            if (Uninitialized(pageSize))
            {
                return float.NaN;
            }

            result += pageSize;
        }

        var currentPageSize = _pageSizes[page];
        if (Uninitialized(currentPageSize))
        {
            return float.NaN;
        }

        result += currentPageSize / 2;
        if (Uninitialized(_viewportSize))
        {
            return float.NaN;
        }

        result -= _viewportSize / 2;

        if (Uninitialized(_padding))
        {
            return float.NaN;
        }

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
        return size <= 0 || float.IsNaN(size);
    }

    public void Dispose()
    {
        _animateScrollTokenSource.Cancel();
        _animateScrollTokenSource.Dispose();
    }
}