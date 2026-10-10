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
        mutableState.SyncPageSpacing(pageSpacing.Value);
        mutableState.SyncContentPadding(contentPadding.Left.Value, contentPadding.Right.Value);
        if (pageSize is PageSize.Fixed fixedPageSize)
            mutableState.SyncPageSize(fixedPageSize.PageSize.Value - contentPadding.Left.Value -
                                      contentPadding.Right.Value);
        var viewportSize = Remember(() => MutableStateOf(0f));
        Box(
            contentAlignment: Alignment.CenterLeft,
            modifier: modifier.OrEmpty()
                .OnSizeChanged(it =>
                {
                    mutableState.SyncViewportSize(it.Width);
                    if (pageSize == PageSize.Fill)
                        mutableState.SyncPageSize(it.Width - contentPadding.Left.Value - contentPadding.Right.Value);
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
                        .FillMaxHeight()
                        .Padding(
                            top: contentPadding.Top,
                            bottom: contentPadding.Bottom
                        ),
                    content: () =>
                    {
                        var pageCount = mutableState.PageCount;
                        for (var i = 0; i < pageCount; i++)
                        {
                            if (i > 0)
                            {
                                Spacer(Modifier.Width(pageSpacing / 2));
                            }

                            var pageSizeValue = pageSize is PageSize.Fixed fixedSize
                                ? fixedSize.PageSize
                                : viewportSize.Value.Dp();
                            pageSizeValue -= contentPadding.Left;
                            pageSizeValue -= contentPadding.Right;
                            PagerPage(
                                page: i,
                                pageContent: pageContent,
                                pageSize: pageSizeValue
                            );

                            if (i < pageCount - 1)
                            {
                                Spacer(Modifier.Width(pageSpacing / 2));
                            }
                        }
                    }
                );
            }
        );
    }

    [Composable]
    private static void PagerPage(
        int page,
        ComposableContent<int> pageContent,
        Dp pageSize,
        IModifier? modifier = null
    )
    {
        Box(
            contentAlignment: Alignment.Center,
            modifier: modifier.OrEmpty()
                .FillMaxHeight()
                .Width(pageSize),
            content: () => pageContent(page)
        );
    }
}