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