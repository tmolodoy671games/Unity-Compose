using System;
using Sirenix.Utilities;
using UnityEngine.SocialPlatforms;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample : ComposeUI
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
                alignment: Alignment.Center,
                modifier: Modifier,
                content: () =>
                { 
                    Column(
                        horizontalAlignment: Alignment.CenterHorizontally,
                        modifier: Modifier
                            .Name("reordarable-list-sample")
                            .Padding(top: 100.Dp())
                            .FillMaxHeight()
                            .Width(800.Dp()),
                        content: () =>
                        {
                            var items = Remember(() => MutableStateListOf(1, 2));
                            Text(
                                text: "Add Item",
                                color: Color.white,
                                fontSize: 40.Sp(),
                                modifier: Modifier
                                    .Name("add-item-button")
                                    .Align(Alignment.Right)
                                    .Background(Color.blue)
                                    .Padding(horizontal: 32.Dp(), vertical: 16.Dp())
                                    .Border(radius: 16.Dp())
                                    .OnClick(() =>
                                    {
                                        for (var i = 1; i <= items.Count + 1; i++)
                                        {
                                            if (!items.Contains(i))
                                            {
                                                items.Add(i);
                                                return;
                                            }
                                        }
                                    })
                            );

                            var immutableItems = items.ToImmutableList();
                            Column(
                                horizontalAlignment: Alignment.CenterHorizontally,
                                modifier: Modifier
                                    .Name("nested-column")
                                    .FillMaxWidth(),
                                content: () =>
                                {
                                    foreach (var item in immutableItems)
                                    {
                                        Key(
                                            key: item,
                                            content: () =>
                                            {
                                                Item(
                                                    state: item,
                                                    onMoveUpClick: () =>
                                                    {
                                                        var oldIndex = items.IndexOf(item);
                                                        if (oldIndex == 0) return;
                                                        var newIndex = oldIndex - 1;
                                                        items.RemoveAt(oldIndex);
                                                        items.Insert(newIndex, item);
                                                    },
                                                    onMoveDownClick: () =>
                                                    {
                                                        var oldIndex = items.IndexOf(item);
                                                        if (oldIndex == items.Count - 1) return;
                                                        var newIndex = oldIndex + 1;
                                                        items.RemoveAt(oldIndex);
                                                        items.Insert(newIndex, item);
                                                    },
                                                    onRemoveClick: () => items.Remove(item)
                                                );
                                            }
                                        );
                                    }
                                }
                            );
                        }
                    );
                }
            );
        }

        [Composable]
        private static void Item(
            int state,
            Action onMoveUpClick,
            Action onMoveDownClick,
            Action onRemoveClick
        )
        {
            if (state == 2)
                SideEffect("state", static () => { });
            Row(
                verticalAlignment: Alignment.CenterVertically,
                modifier: Modifier
                    .Name("item-row")
                    .Background(Color.cyan)
                    .FillMaxWidth()
                    .Padding(all: 4.Dp())
                    .Border(radius: 12.Dp())
                    .Margin(vertical: 4.Dp()),
                content: () =>
                {
                    Text(
                        text: $"Item no. {state}",
                        color: Color.black,
                        fontSize: 40.Sp(),
                        modifier: Modifier
                            .Name("item-name-label")
                            .Margin(left: 32.Dp())
                    );
                    Spacer(Modifier.Weight(1));
                    var counter = Remember(() => MutableStateOf(0));
                    Text(
                        text: counter.Value.ToString(),
                        color: Color.white,
                        fontSize: 40.Sp(),
                        modifier: Modifier
                            .Background(Color.green)
                            .Padding(horizontal: 16.Dp())
                            .Border(12.Dp())
                            .OnClick(() => counter.Value++)
                    );
                    Column(
                        content: () =>
                        {
                            Text(
                                text: "↑",
                                color: Color.white,
                                fontSize: 40.Sp(),
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("up-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6.Dp(), vertical: 4.Dp())
                                    .Border(radius: 16.Dp())
                                    .OnClick(onMoveUpClick)
                            );
                            Text(
                                text: "↓",
                                color: Color.white,
                                fontSize: 40.Sp(),
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("down-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6.Dp(), vertical: 4.Dp())
                                    .Border(radius: 16.Dp())
                                    .OnClick(onMoveDownClick)
                            );
                        }
                    );
                    Text(
                        text: "X",
                        color: Color.white,
                        fontSize: 40.Sp(),
                        fontWeight: FontWeight.Bold,
                        textAlign: TextAlign.MiddleCenter,
                        modifier: Modifier
                            .Name("remove-button")
                            .Background(Color.red)
                            .Padding(horizontal: 16.Dp(), vertical: 4.Dp())
                            .Border(radius: 16.Dp())
                            .OnClick(onRemoveClick)
                    );
                }
            );
        }
    }
}