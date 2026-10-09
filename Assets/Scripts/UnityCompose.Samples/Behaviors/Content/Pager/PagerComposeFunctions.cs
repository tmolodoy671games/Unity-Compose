using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

internal static partial class PagerComposeFunctions
{
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
        var mutableState = state as IMutablePagerState;
        pageSize ??= PageSize.Fill;
        if (mutableState == null)
            return;
        mutableState.SyncContentPadding(contentPadding.Left.Value);
        mutableState.SyncPageSpacing(pageSpacing.Value);
        if (pageSize is PageSize.Fixed fixedPageSize)
            mutableState.SyncPageSize(fixedPageSize.PageSize.Value);
        var viewportSize = Remember(() => MutableStateOf(0f));
        Box(
            contentAlignment: Alignment.CenterLeft,
            modifier: modifier.OrEmpty()
                .OnSizeChanged(it =>
                {
                    mutableState.SyncViewportSize(it.Width);
                    if (pageSize == PageSize.Fill)
                        mutableState.SyncPageSize(it.Width);
                    viewportSize.Value = it.Width;
                }),
            content: () =>
            {
                Row(
                    modifier: Modifier
                        .Custom(
                            apply: it =>
                            {
                                it.VisualElement().style.flexShrink = 0;
                                mutableState.SubscribeToValueChange(value =>
                                    it.VisualElement().style.translate = new Translate(-value, 0f));
                            },
                            revert: it => { }
                        )
                        // .Offset(x: -mutableState.Value.Dp())
                        .FillMaxHeight()
                        .Padding(contentPadding),
                    content: () =>
                    {
                        var pageCount = mutableState.PageCount;
                        for (var i = 0; i < pageCount; i++)
                        {
                            PagerPage(
                                page: i,
                                pageCount: pageCount,
                                pageSpacing: pageSpacing,
                                pageContent: pageContent,
                                viewportSize: viewportSize.Value,
                                pageSize: pageSize
                            );
                        }
                    }
                );
            }
        );
    }

    [Composable]
    private static void PagerPage(
        int page,
        int pageCount,
        ComposableContent<int> pageContent,
        Dp pageSpacing,
        PageSize pageSize,
        float viewportSize,
        IModifier? modifier = null
    )
    {
        var leftPadding = page > 0 ? pageSpacing / 2 : 0.Dp();
        var rightPadding = page < pageCount - 1 ? pageSpacing / 2 : 0.Dp();
        Box(
            contentAlignment: Alignment.Center,
            modifier: modifier.OrEmpty()
                .Padding(
                    left: leftPadding,
                    right: rightPadding
                )
                .FillMaxHeight()
                .Then(
                    pageSize is PageSize.Fixed fixedSize
                        ? Modifier.Width(fixedSize.PageSize)
                        : Modifier.Width(viewportSize.Dp() - leftPadding - rightPadding)
                ),
            content: () => pageContent(page)
        );
    }
}