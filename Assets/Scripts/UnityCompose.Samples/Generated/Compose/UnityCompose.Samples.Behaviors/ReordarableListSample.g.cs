using System;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample
    {
        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier, content: RememberComposable<global::System.Action>(null, () =>
                {
                    Column(alignHorizontally: Align.Center, style: Modifier.Name("reordarable-list-sample").NewPadding(top: 100).Height(100.Percent()).Width(800), content: RememberComposable<global::System.Action>(null, () =>
                    {
                        var items = Remember(() => MutableStateListOf(1, 2));
                        Label(text: "Add Item", textColor: Color.white, fontSize: 40, style: Modifier.Name("add-item-button").AlignSelf(Align.FlexEnd).Background(Color.blue).NewPadding(horizontal: 32, vertical: 16).BorderRadius(16).OnClick(Remember<global::System.Action>(items, () =>
                        {
                            for (var i = 1; i <= items.Count + 1; i++)
                            {
                                if (!items.Contains(i))
                                {
                                    items.Add(i);
                                    return;
                                }
                            }
                        })));
                        Column(alignHorizontally: Align.Center, style: Modifier.Name("nested-column").Width(100.Percent()), content: RememberComposable<global::System.Action>(items, () =>
                        {
                            foreach (var item in items)
                            {
                                Key(key: item, content: RememberComposable<global::System.Action>((items, item), () =>
                                {
                                    Item(state: item, onMoveUpClick: Remember<global::System.Action>((items, item), () =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == 0)
                                            return;
                                        var newIndex = oldIndex - 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    }), onMoveDownClick: Remember<global::System.Action>((items, item), () =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == items.Count - 1)
                                            return;
                                        var newIndex = oldIndex + 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    }), onRemoveClick: Remember<global::System.Action>((items, item), () =>
                                    {
                                        items.Remove(item);
                                    }));
                                }));
                            }
                        }));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }

        [Composable]
        [Compiled]
        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick)
        {
            if (CurrentComposer.BeginComposeGroup((state, onMoveUpClick, onMoveDownClick, onRemoveClick)))
                return;
            try
            {
                Row(alignVertically: Align.Center, style: Modifier.Name("item-row").Background(Color.cyan).Width(100.Percent()).NewPadding(all: 4).BorderRadius(12).Margin(vertical: 4).Name(state.ToString()), content: RememberComposable<global::System.Action>((state, onMoveUpClick, onMoveDownClick, onRemoveClick), () =>
                {
                    Label(text: $"Item no. {state}", textColor: Color.black, fontSize: 40, style: Modifier.Name("item-name-label").FlexGrow(1).Margin(left: 32));
                    Column(content: RememberComposable<global::System.Action>((onMoveUpClick, onMoveDownClick), () =>
                    {
                        Label(text: "↑", textColor: Color.white, fontSize: 40, fontStyle: FontStyle.Bold, align: TextAnchor.MiddleCenter, style: Modifier.Name("up-arrow-button").Background(Color.green).NewPadding(horizontal: 6, vertical: 4).BorderRadius(16).OnClick(onMoveUpClick));
                        Label(text: "↓", textColor: Color.white, fontSize: 40, fontStyle: FontStyle.Bold, align: TextAnchor.MiddleCenter, style: Modifier.Name("down-arrow-button").Background(Color.green).NewPadding(horizontal: 6, vertical: 4).BorderRadius(16).OnClick(onMoveDownClick));
                    }));
                    Label(text: "X", textColor: Color.white, fontSize: 40, fontStyle: FontStyle.Bold, align: TextAnchor.MiddleCenter, style: Modifier.Name("remove-button").Background(Color.red).NewPadding(horizontal: 16, vertical: 4).BorderRadius(16).OnClick(onRemoveClick));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Item(state, onMoveUpClick, onMoveDownClick, onRemoveClick));
            }
        }
    }
}