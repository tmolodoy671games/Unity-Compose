using System;
using UnityEngine.SocialPlatforms;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample : ComposeUI
    {
        private static readonly ICompositionLocal<string> LocalTest = CompositionLocalOf("LocalTest", () => "Default");

        protected override void Content()
        {
            Layout();
        }

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
                            .Padding(top: 100.Px())
                            .FillMaxHeight()
                            .Width(800.Px()),
                        content: () =>
                        {
                            var items = Remember(() => MutableStateListOf(1, 2));
                            Text(
                                text: "Add Item",
                                color: Color.white,
                                fontSize: 40,
                                modifier: Modifier
                                    .Name("add-item-button")
                                    .Align(Alignment.Right)
                                    .Background(Color.blue)
                                    .Padding(horizontal: 32.Px(), vertical: 16.Px())
                                    .Border(radius: 16.Px())
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

                            Column(
                                horizontalAlignment: Alignment.CenterHorizontally,
                                modifier: Modifier
                                    .Name("nested-column")
                                    .FillMaxWidth(),
                                content: () =>
                                {
                                    foreach (var item in items)
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
                                                    onRemoveClick: () => { items.Remove(item); }
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
            Row(
                verticalAlignment: Alignment.CenterVertically,
                modifier: Modifier
                    .Name("item-row")
                    .Background(Color.cyan)
                    .FillMaxWidth()
                    .Padding(all: 4.Px())
                    .Border(radius: 12.Px())
                    .Margin(vertical: 4.Px()),
                content: () =>
                {
                    Text(
                        text: $"Item no. {state}",
                        color: Color.black,
                        fontSize: 40,
                        modifier: Modifier
                            .Name("item-name-label")
                            .Margin(left: 32.Px())
                    );
                    Spacer(Modifier.Weight(1));
                    var counter = Remember(() => MutableStateOf(0));
                    Text(
                        text: counter.Value.ToString(),
                        color: Color.white,
                        fontSize: 40,
                        modifier: Modifier
                            .Background(Color.green)
                            .Padding(horizontal: 16.Px())
                            .Border(12.Px())
                            .OnClick(() => counter.Value++)
                    );
                    Column(
                        content: () =>
                        {
                            Text(
                                text: "↑",
                                color: Color.white,
                                fontSize: 40,
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("up-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6.Px(), vertical: 4.Px())
                                    .Border(radius: 16.Px())
                                    .OnClick(onMoveUpClick)
                            );
                            Text(
                                text: "↓",
                                color: Color.white,
                                fontSize: 40,
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("down-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6.Px(), vertical: 4.Px())
                                    .Border(radius: 16.Px())
                                    .OnClick(onMoveDownClick)
                            );
                        }
                    );
                    Text(
                        text: "X",
                        color: Color.white,
                        fontSize: 40,
                        fontWeight: FontWeight.Bold,
                        textAlign: TextAlign.MiddleCenter,
                        modifier: Modifier
                            .Name("remove-button")
                            .Background(Color.red)
                            .Padding(horizontal: 16.Px(), vertical: 4.Px())
                            .Border(radius: 16.Px())
                            .OnClick(onRemoveClick)
                    );
                }
            );
        }
    }
}