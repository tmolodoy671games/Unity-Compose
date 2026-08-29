// ReSharper disable ArrangeNamespaceBody

using System;
using SharpExtensions;
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
            var layoutCoordinates = Remember(() => MutableStateOf(Optional.Empty<LayoutCoordinates>()));
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize()
                    .OnGloballyPositioned(it => layoutCoordinates.Value = it),
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

                    if (!layoutCoordinates.Value.HasValue)
                        return;
                    foreach (var position in positions.Values)
                    {
                        var coordinates = layoutCoordinates.Value.Value;
                        Spacer(
                            modifier: Modifier
                                .Background(Color.red)
                                .Size(16.Dp())
                                .Border(4.Dp(), topLeftRadius: 0.Dp())
                                .Float()
                                .Position(
                                    left: coordinates.GlobalToLocal(position).x.Dp(),
                                    top: coordinates.GlobalToLocal(position).y.Dp()
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
            var animationSpec = Tween();
            Box(
                modifier: modifier.OrEmpty()
                    .Background(Color.grey)
                    .Padding(
                        vertical: 8.Dp(),
                        horizontal: AnimateFloatAsState(selected ? 160 : 20, animationSpec: animationSpec).Value.Dp()
                    )
                    .Margin(horizontal: 2.Dp())
                    .Border(16.Dp(), topLeftRadius: 0.Dp())
                    .Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value),
                content: () =>
                {
                    CompositionLocalProvider(
                        LocalContentColor.Provides(Color.white),
                        LocalTextStyle.Provides(
                            new TextStyle(
                                Color: Color.white,
                                FontSize: 32.Sp()
                            )
                        ),
                        content: content
                    );
                }
            );
        }
    }
}