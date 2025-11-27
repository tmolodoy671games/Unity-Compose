// ReSharper disable ArrangeNamespaceBody

using System;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositioned2Sample : ComposeUI
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
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    var positions = Remember(static () => MutableStateDictionaryOf<int, Vector2>());

                    Row(() =>
                    {
                        var selectionIndex = Remember(static () => MutableStateOf(0));
                        Tab(
                            selected: selectionIndex.Value == 0,
                            modifier: Modifier
                                .OnClick(() => selectionIndex.Value = 0)
                                .OnGloballyPositioned(it => positions[0] = it.GlobalPosition),
                            content: () => Text(text: "First")
                        );
                        Tab(
                            selected: selectionIndex.Value == 1,
                            modifier: Modifier
                                .OnClick(() => selectionIndex.Value = 1)
                                .OnGloballyPositioned(it => positions[1] = it.GlobalPosition),
                            content: () => Text(text: "Second")
                        );
                        Tab(
                            selected: selectionIndex.Value == 2,
                            modifier: Modifier
                                .OnClick(() => selectionIndex.Value = 2)
                                .OnGloballyPositioned(it => positions[2] = it.GlobalPosition),
                            content: () => Text(text: "Third")
                        );
                        Tab(
                            selected: selectionIndex.Value == 3,
                            modifier: Modifier
                                .OnClick(() => selectionIndex.Value = 3)
                                .OnGloballyPositioned(it => positions[3] = it.GlobalPosition),
                            content: () => Text(text: "Fourth")
                        );
                    });

                    foreach (var position in positions.Values)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(
                            modifier: Modifier
                                .Background(Color.red)
                                .Size(16)
                                .Border(4, topLeftRadius: 0)
                                .Float()
                                .Position(
                                    left: measurer.GlobalToLocal(position).x,
                                    top: measurer.GlobalToLocal(position).y
                                )
                        );
                    }
                }
            );
        }

        [Composable]
        private static void Tab(
            bool selected,
            ComposableContent content,
            IModifier? modifier = null
        )
        {
            Box(
                modifier: modifier.OrEmpty()
                    .Background(Color.grey)
                    .Padding(
                        vertical: 8,
                        horizontal: AnimateFloatAsState(selected ? 160 : 20).Value
                    )
                    .Margin(horizontal: 2)
                    .Border(16, topLeftRadius: 0)
                    .Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value),
                content: () =>
                {
                    CompositionLocalProvider(
                        LocalContentColor.Provides(Color.white),
                        LocalTextStyle.Provides(
                            new TextStyle(
                                Color: Color.white,
                                FontSize: 32
                            )
                        ),
                        content: content
                    );
                }
            );
        }
    }
}