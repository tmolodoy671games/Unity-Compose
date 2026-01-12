#nullable enable
using System;
using UnityEngine.SocialPlatforms;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample
    {
        [Composable]
        private static void __Layout()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1657218070);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Box(alignment: Alignment.Center, modifier: Modifier, content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("reordarable-list-sample").Padding(top: 100.Px()).FillMaxHeight().Width(800.Px()), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var items = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableStateList<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateList<int>>(MutableStateListOf(1, 2));
                        Text(text: "Add Item", color: Color.white, fontSize: 40, modifier: Modifier.Name("add-item-button").Align(Alignment.Right).Background(Color.blue).Padding(horizontal: 32.Px(), vertical: 16.Px()).Border(radius: 16.Px()).OnClick(!__composer.Changed(items) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
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
                        Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("nested-column").FillMaxWidth(), content: !__composer.Changed(items) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(-1727535959);
                            foreach (var item in items)
                            {
                                Key(key: item, content: !__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                                {
                                    Item(state: item, onMoveUpClick: !__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == 0)
                                            return;
                                        var newIndex = oldIndex - 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    }), onMoveDownClick: !__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == items.Count - 1)
                                            return;
                                        var newIndex = oldIndex + 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    }), onRemoveClick: !__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                                    {
                                        items.Remove(item);
                                    }));
                                }));
                            }

                            __composer.EndReplaceGroup(-1727535959);
                        }));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1657218070, __isRestarted)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick)
        {
            var(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick) = (state, onMoveUpClick, onMoveDownClick, onRemoveClick);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(1890718061);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecuteAsStruct((__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick)))
            {
                Row(verticalAlignment: Alignment.CenterVertically, modifier: Modifier.Name("item-row").Background(Color.cyan).FillMaxWidth().Padding(all: 4.Px()).Border(radius: 12.Px()).Margin(vertical: 4.Px()), content: !__composer.ChangedAsStruct((state, onMoveUpClick, onMoveDownClick, onRemoveClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Text(text: $"Item no. {state}", color: Color.black, fontSize: 40, modifier: Modifier.Name("item-name-label").Margin(left: 32.Px()));
                    Spacer(Modifier.Weight(1));
                    var counter = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<int>>(MutableStateOf(0));
                    Text(text: counter.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Background(Color.green).Padding(horizontal: 16.Px()).Border(12.Px()).OnClick(!__composer.Changed(counter) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => counter.Value++)));
                    Column(content: !__composer.ChangedAsStruct((onMoveUpClick, onMoveDownClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        Text(text: "↑", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("up-arrow-button").Background(Color.green).Padding(horizontal: 6.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onMoveUpClick));
                        Text(text: "↓", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("down-arrow-button").Background(Color.green).Padding(horizontal: 6.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onMoveDownClick));
                    }));
                    Text(text: "X", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("remove-button").Background(Color.red).Padding(horizontal: 16.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onRemoveClick));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1890718061, __isRestarted)?.UpdateScope(() => __Item(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick));
        }
    }
}