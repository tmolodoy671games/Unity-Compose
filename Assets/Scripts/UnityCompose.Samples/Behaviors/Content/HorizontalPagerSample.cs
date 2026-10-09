// ReSharper disable ArrangeNamespaceBody

using System;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityCompose.Samples.Behaviors.Content.Pager;
using UnityEngine.UIElements;
using static UnityCompose.Samples.Behaviors.Content.Pager.PagerComposeFunctions;

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
                                                coroutineScope.Launch(async scope =>
                                                {
                                                    await pagerState.AnimateScrollToPage(scope, page);
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
    }
}