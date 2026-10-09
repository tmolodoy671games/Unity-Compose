// ReSharper disable ArrangeNamespaceBody

using System;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class HorizontalPagerSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Layout();
        }

        [Composable]
        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Column(
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    var pagerState = RememberPagerState(() => 3);
                    var coroutineScope = RememberCoroutineScope();
                    HorizontalPager(
                        state: pagerState,
                        pageSize: new PageSize.Fixed(400.Dp()),
                        contentPadding: PaddingValues(16.Dp()),
                        pageSpacing: 60.Dp(),
                        modifier: Modifier
                            .FillMaxWidth()
                            .Weight(1f),
                        pageContent: it =>
                        {
                            Spacer(
                                Modifier
                                    .FillMaxSize()
                                    .Background(
                                        it switch
                                        {
                                            0 => Color.RedColor,
                                            1 => Color.GreenColor,
                                            2 => Color.BlueColor,
                                            _ => throw new ArgumentOutOfRangeException(nameof(it), it, null)
                                        }
                                    )
                            );
                        }
                    );
                    Row(
                        horizontalArrangement: Arrangement.Center,
                        modifier: Modifier
                            .Padding(vertical: 8.Dp())
                            .FillMaxWidth(),
                        content: () =>
                        {
                            for (var i = 0; i < 3; i++)
                            {
                                var page = i;
                                Box(
                                    modifier: Modifier
                                        .Padding(horizontal: 8.Dp())
                                        .Background(Color.CadetBlue)
                                        .Padding(horizontal: 16.Dp(), vertical: 8.Dp())
                                        .Clip(RoundedCornerShape(4.Dp()))
                                        .Clickable(() =>
                                            {
                                                coroutineScope.Launch(async token =>
                                                {
                                                    await pagerState.AnimateScrollToPage(page, token);
                                                });
                                            }
                                        ),
                                    content: () =>
                                    {
                                        Text(
                                            text: page.ToString(),
                                            fontSize: 32.Sp(),
                                            color: Color.White
                                        );
                                    }
                                );
                            }
                        }
                    );
                }
            );
        }

        [Composable]
        public static IPagerState RememberPagerState(
            Func<int> pageCount,
            int initialPage = 0,
            float initialPageOffsetFraction = 0f
        )
        {
            return Remember(() =>
            {
                var result = new MutablePagerStateImpl(pageCount, initialPage);
                result.ScrollToPage(initialPage);
                return result;
            });
        }

        [Composable]
        public static void HorizontalPager(
            IPagerState state,
            ComposableContent<int> pageContent,
            PaddingValues contentPadding = default,
            Dp pageSpacing = default,
            PageSize? pageSize = null,
            bool reverseLayout = false,
            IModifier? modifier = null
        )
        {
            Pager(
                nodeFactory: () => new UnityReusableComposeNode(new HorizontalPager()),
                state: state,
                content: pageContent,
                orientation: Orientation.Horizontal,
                contentPadding: contentPadding,
                pageSize: pageSize ?? PageSize.Fill,
                pageSpacing: pageSpacing,
                reverseLayout: reverseLayout,
                horizontalAlignment: Alignment.CenterHorizontally,
                verticalAlignment: Alignment.CenterVertically,
                modifier: modifier
            );
        }

        [Composable]
        private static void Pager(
            Func<IReusableComposeNode> nodeFactory,
            IPagerState state,
            ComposableContent<int> content,
            Orientation orientation,
            bool reverseLayout,
            PaddingValues contentPadding,
            Dp pageSpacing,
            PageSize pageSize,
            Alignment.Horizontal horizontalAlignment,
            Alignment.Vertical verticalAlignment,
            IModifier? modifier
        )
        {
            var mutableState = state as IMutablePagerState;
            if (mutableState == null)
                return;
            mutableState.SyncContentPadding(orientation.ToPadding(reverseLayout, contentPadding));
            var viewportSize = Remember(() => MutableStateOf(-1f));
            ReusableComposeNode(
                nodeFactory: nodeFactory,
                modifier: modifier.OrEmpty()
                    .OnGloballyPositioned(it =>
                    {
                        var newViewportSize = orientation switch
                        {
                            Orientation.Horizontal => it.Size.Width,
                            Orientation.Vertical => it.Size.Height,
                            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
                        };
                        mutableState.SyncViewportSize(newViewportSize);
                        viewportSize.Value = newViewportSize;
                    }),
                content: () =>
                {
                    ReusableComposeNode(
                        nodeFactory: () => new UnityReusableComposeNode(new VisualElement()),
                        initializer: it =>
                        {
                            var element = it.VisualElement();
                            element.style.flexDirection = orientation.ToFlexDirection(reverseLayout);
                            element.style.flexShrink = 0;
                            switch (orientation)
                            {
                                case Orientation.Horizontal:
                                    element.style.alignItems = verticalAlignment.ToAlign();
                                    element.style.height = new Length(100, LengthUnit.Percent);
                                    break;
                                case Orientation.Vertical:
                                    element.style.alignItems = horizontalAlignment.ToAlign();
                                    element.style.width = new Length(100, LengthUnit.Percent);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                            }
                        },
                        modifier: Modifier
                            .Float()
                            .Offset(
                                x: orientation == Orientation.Horizontal ? -mutableState.Value.Dp() : 0.Dp(),
                                y: orientation == Orientation.Vertical ? -mutableState.Value.Dp() : 0.Dp()
                            )
                            .Padding(contentPadding),
                        content: () =>
                        {
                            var pageCount = state.PageCount;
                            for (var i = 0; i < pageCount; i++)
                            {
                                var currentI = i;
                                Box(
                                    contentAlignment: Alignment.Center,
                                    modifier: Modifier
                                        .Then(PageSizeModifier(orientation, pageSize, viewportSize.Value))
                                        .OnSizeChanged(it => mutableState.SyncSize(currentI, orientation switch
                                        {
                                            Orientation.Horizontal => it.Width,
                                            Orientation.Vertical => it.Height,
                                            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation,
                                                null)
                                        }))
                                        .Padding(
                                            horizontal: orientation == Orientation.Horizontal
                                                ? pageSpacing / 2
                                                : 0.Dp(),
                                            vertical: orientation == Orientation.Vertical ? pageSpacing / 2 : 0.Dp()
                                        ),
                                    content: () => content(currentI)
                                );
                            }
                        }
                    );
                }
            );
        }

        private static IModifier PageSizeModifier(Orientation orientation, PageSize pageSize, float viewportSize)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    if (pageSize is PageSize.Fixed fixedPageSize)
                        return Modifier
                            .FillMaxHeight()
                            .Width(fixedPageSize.PageSize);
                    return Modifier
                        .FillMaxHeight()
                        .Width(viewportSize.Dp());
                case Orientation.Vertical:
                    if (pageSize is PageSize.Fixed fixedVerticalSize)
                        return Modifier
                            .FillMaxWidth()
                            .Height(fixedVerticalSize.PageSize);
                    return Modifier
                        .FillMaxWidth()
                        .Height(viewportSize.Dp());
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }
    }


    internal class HorizontalPager : VisualElement
    {
    }

    internal class VerticalPager : VisualElement
    {
    }

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

    public enum Orientation
    {
        Horizontal,
        Vertical,
    }

    public record PageSize
    {
        public static readonly PageSize Fill = new();

        public record Fixed(Dp PageSize) : PageSize;

        private PageSize()
        {
        }
    }

    internal static class OrientationExtensions
    {
        public static FlexDirection ToFlexDirection(this Orientation orientation, bool reverseLayout)
        {
            return orientation switch
            {
                Orientation.Horizontal => reverseLayout ? FlexDirection.RowReverse : FlexDirection.Row,
                Orientation.Vertical => reverseLayout ? FlexDirection.ColumnReverse : FlexDirection.Column,
                _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
            };
        }

        public static float ToPadding(this Orientation orientation, bool reverseLayout, PaddingValues paddingValues)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    return reverseLayout ? paddingValues.Right.Value : paddingValues.Left.Value;
                    break;
                case Orientation.Vertical:
                    return reverseLayout ? paddingValues.Bottom.Value : paddingValues.Top.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }
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
}