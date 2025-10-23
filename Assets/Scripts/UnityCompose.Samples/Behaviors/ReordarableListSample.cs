using System;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample : ComposeUI
    {
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
                alignHorizontally: Align.Center,
                alignVertically: Justify.Center,
                style: Modifier,
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: Modifier
                            .Name("reordarable-list-sample")
                            .NewPadding(top: 100)
                            .FillMaxHeight()
                            .Width(800),
                        content: scope =>
                        {
                            var items = Remember(() => MutableStateListOf(1, 2));
                            Text(
                                text: "Add Item",
                                textColor: Color.white,
                                fontSize: 40,
                                style: Modifier
                                    .Name("add-item-button")
                                    .Then(scope.Align(HorizontalAlign.Right))
                                    .Background(Color.blue)
                                    .NewPadding(horizontal: 32, vertical: 16)
                                    .Border(radius: 16)
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
                                alignHorizontally: Align.Center,
                                style: Modifier
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
                alignVertically: Align.Center,
                style: Modifier
                    .Name("item-row")
                    .Background(Color.cyan)
                    .FillMaxWidth()
                    .NewPadding(all: 4)
                    .Border(radius: 12)
                    .Margin(vertical: 4)
                    .Name(state.ToString()),
                content: scope =>
                {
                    Text(
                        text: $"Item no. {state}",
                        textColor: Color.black,
                        fontSize: 40,
                        style: Modifier
                            .Name("item-name-label")
                            .Then(scope.Weight(1))
                            .Margin(left: 32)
                    );
                    Column(
                        content: () =>
                        {
                            Text(
                                text: "↑",
                                textColor: Color.white,
                                fontSize: 40,
                                fontStyle: FontStyle.Bold,
                                align: TextAnchor.MiddleCenter,
                                style: Modifier
                                    .Name("up-arrow-button")
                                    .Background(Color.green)
                                    .NewPadding(horizontal: 6, vertical: 4)
                                    .Border(radius: 16)
                                    .OnClick(onMoveUpClick)
                            );
                            Text(
                                text: "↓",
                                textColor: Color.white,
                                fontSize: 40,
                                fontStyle: FontStyle.Bold,
                                align: TextAnchor.MiddleCenter,
                                style: Modifier
                                    .Name("down-arrow-button")
                                    .Background(Color.green)
                                    .NewPadding(horizontal: 6, vertical: 4)
                                    .Border(radius: 16)
                                    .OnClick(onMoveDownClick)
                            );
                        }
                    );
                    Text(
                        text: "X",
                        textColor: Color.white,
                        fontSize: 40,
                        fontStyle: FontStyle.Bold,
                        align: TextAnchor.MiddleCenter,
                        style: Modifier
                            .Name("remove-button")
                            .Background(Color.red)
                            .NewPadding(horizontal: 16, vertical: 4)
                            .Border(radius: 16)
                            .OnClick(onRemoveClick)
                    );
                }
            );
        }
    }
}