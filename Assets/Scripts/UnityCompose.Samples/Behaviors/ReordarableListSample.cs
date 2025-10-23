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
                style: IModifier.Empty,
                content: () =>
                {
                    Column(
                        alignHorizontally: Align.Center,
                        style: IModifier.Empty
                            .Name("reordarable-list-sample")
                            .PaddingTop(100)
                            .Height(100.Percent())
                            .Width(800),
                        content: () =>
                        {
                            var items = Remember(() => MutableStateListOf(1, 2));
                            Label(
                                text: "Add Item",
                                textColor: Color.white,
                                fontSize: 40,
                                style: IModifier.Empty
                                    .Name("add-item-button")
                                    .AlignSelf(Align.FlexEnd)
                                    .BackgroundColor(Color.blue)
                                    .Padding(32, 16)
                                    .BorderRadius(16)
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
                                style: IModifier.Empty
                                    .Name("nested-column")
                                    .Width(100.Percent()),
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
                                                    onRemoveClick: () =>
                                                    {
                                                        items.Remove(item);
                                                    }
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
                style: IModifier.Empty
                    .Name("item-row")
                    .BackgroundColor(Color.cyan)
                    .Width(100.Percent())
                    .Padding(4)
                    .BorderRadius(12)
                    .MarginVertical(4)
                    .Name(state.ToString()),
                content: () =>
                {
                    Label(
                        text: $"Item no. {state}",
                        textColor: Color.black,
                        fontSize: 40,
                        style: IModifier.Empty
                            .Name("item-name-label")
                            .FlexGrow(1)
                            .MarginLeft(32)
                    );
                    Column(
                        content: () =>
                        {
                            Label(
                                text: "↑",
                                textColor: Color.white,
                                fontSize: 40,
                                fontStyle: FontStyle.Bold,
                                align: TextAnchor.MiddleCenter,
                                style: IModifier.Empty
                                    .Name("up-arrow-button")
                                    .BackgroundColor(Color.green)
                                    .Padding(6, 4)
                                    .BorderRadius(16)
                                    .OnClick(onMoveUpClick)
                            );
                            Label(
                                text: "↓",
                                textColor: Color.white,
                                fontSize: 40,
                                fontStyle: FontStyle.Bold,
                                align: TextAnchor.MiddleCenter,
                                style: IModifier.Empty
                                    .Name("down-arrow-button")
                                    .BackgroundColor(Color.green)
                                    .Padding(6, 4)
                                    .BorderRadius(16)
                                    .OnClick(onMoveDownClick)
                            );
                        }
                    );
                    Label(
                        text: "X",
                        textColor: Color.white,
                        fontSize: 40,
                        fontStyle: FontStyle.Bold,
                        align: TextAnchor.MiddleCenter,
                        style: IModifier.Empty
                            .Name("remove-button")
                            .BackgroundColor(Color.red)
                            .Padding(16, 4)
                            .BorderRadius(16)
                            .OnClick(onRemoveClick)
                    );
                }
            );
        }
    }
}