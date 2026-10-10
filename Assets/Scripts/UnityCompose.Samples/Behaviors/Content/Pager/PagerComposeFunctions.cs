using System;
using Compose.Net;

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
            result.ScrollToPage(initialPage, initialPageOffsetFraction);
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
        IModifier? modifier = null
    )
    {
        var mutableState = state as IMutablePagerState;
        pageSize ??= PageSize.Fill;
        if (mutableState == null)
            return;
        mutableState.SyncPageSpacing(pageSpacing.Value);
        if (pageSize is PageSize.Fixed fixedPageSize)
            mutableState.SyncPageSize(fixedPageSize.PageSize.Value - contentPadding.Left.Value -
                                      contentPadding.Right.Value);
        var viewportSize = Remember(() => MutableStateOf(0f));
        ReusableComposeNode(
            nodeFactory: () => new UnityReusableComposeNode(new HorizontalPager()),
            initializer: it =>
            {
                it.VisualElement<HorizontalPager>().Init(
                    pagerState: mutableState,
                    contentPadding: contentPadding
                );
            },
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
                var pageCount = mutableState.PageCount;
                for (var i = 0; i < pageCount; i++)
                {
                    if (i > 0)
                        Spacer(Modifier.Width(pageSpacing / 2));

                    var pageSizeValue = pageSize is PageSize.Fixed fixedSize
                        ? fixedSize.PageSize
                        : viewportSize.Value.Dp();
                    pageSizeValue -= contentPadding.Left;
                    pageSizeValue -= contentPadding.Right;

                    var page = i;
                    ReusableComposeNode(
                        nodeFactory: () => new UnityReusableComposeNode(new Item()),
                        modifier: modifier.OrEmpty()
                            .FillMaxHeight()
                            .Width(pageSizeValue),
                        content: () => pageContent(page)
                    );

                    if (i < pageCount - 1)
                        Spacer(Modifier.Width(pageSpacing / 2));
                }
            }
        );
    }

    [Composable]
    public static void VerticalPager(
        IPagerState state,
        ComposableContent<int> pageContent,
        PaddingValues contentPadding = default,
        Dp pageSpacing = default,
        PageSize? pageSize = null,
        IModifier? modifier = null
    )
    {
        var mutableState = state as IMutablePagerState;
        pageSize ??= PageSize.Fill;
        if (mutableState == null)
            return;
        mutableState.SyncPageSpacing(pageSpacing.Value);
        if (pageSize is PageSize.Fixed fixedPageSize)
            mutableState.SyncPageSize(fixedPageSize.PageSize.Value - contentPadding.Top.Value -
                                      contentPadding.Bottom.Value);
        var viewportSize = Remember(() => MutableStateOf(0f));
        ReusableComposeNode(
            nodeFactory: () => new UnityReusableComposeNode(new VerticalPager()),
            initializer: it =>
            {
                it.VisualElement<VerticalPager>().Init(
                    pagerState: mutableState,
                    contentPadding: contentPadding
                );
            },
            modifier: modifier.OrEmpty()
                .OnSizeChanged(it =>
                {
                    mutableState.SyncViewportSize(it.Height);
                    if (pageSize == PageSize.Fill)
                        mutableState.SyncPageSize(it.Height - contentPadding.Top.Value - contentPadding.Bottom.Value);
                    viewportSize.Value = it.Height;
                }),
            content: () =>
            {
                var pageCount = mutableState.PageCount;
                for (var i = 0; i < pageCount; i++)
                {
                    if (i > 0)
                        Spacer(Modifier.Height(pageSpacing / 2));

                    var pageSizeValue = pageSize is PageSize.Fixed fixedSize
                        ? fixedSize.PageSize
                        : viewportSize.Value.Dp();
                    pageSizeValue -= contentPadding.Top;
                    pageSizeValue -= contentPadding.Bottom;

                    var page = i;
                    ReusableComposeNode(
                        nodeFactory: () => new UnityReusableComposeNode(new Item()),
                        modifier: modifier.OrEmpty()
                            .FillMaxWidth()
                            .Height(pageSizeValue),
                        content: () => pageContent(page)
                    );

                    if (i < pageCount - 1)
                        Spacer(Modifier.Height(pageSpacing / 2));
                }
            }
        );
    }
}