#nullable enable
using System;
using Sirenix.Utilities;
using UnityEngine.SocialPlatforms;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(260152039);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(260152039, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1592429396);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1592429396, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(35404226);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier, content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("reordarable-list-sample").Padding(top: 100.Px()).FillMaxHeight().Width(800.Px()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var items = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableStateList<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableStateList<int>>(MutableStateListOf(1, 2)));
                        __Text(text: "Add Item", color: Color.white, fontSize: 40, modifier: Modifier.Name("add-item-button").Align(Alignment.Right).Background(Color.blue).Padding(horizontal: 32.Px(), vertical: 16.Px()).Border(radius: 16.Px()).OnClick((!__composer.Changed(items) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                        {
                            for (var i = 1; i <= items.Count + 1; i++)
                            {
                                if (!items.Contains(i))
                                {
                                    items.Add(i);
                                    return;
                                }
                            }
                        }))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                        var immutableItems = items.ToImmutableList();
                        __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("nested-column").FillMaxWidth(), content: (!__composer.ChangedAsStruct((items, immutableItems)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(214651979);
                            foreach (var item in immutableItems)
                            {
                                Key(key: item, content: (!__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                                {
                                    __Item(state: item, onMoveUpClick: (!__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == 0)
                                            return;
                                        var newIndex = oldIndex - 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    })), onMoveDownClick: (!__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == items.Count - 1)
                                            return;
                                        var newIndex = oldIndex + 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    })), onRemoveClick: (!__composer.ChangedAsStruct((items, item)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                                    {
                                        items.Remove(item);
                                    })), __composer: __composer, __changed: 0);
                                })));
                            }

                            __composer.EndReplaceGroup(214651979);
                        })), __composer: __composer, __changed: 0b_01_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00_00);
                })), __composer: __composer, __changed: 0);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(35404226, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick) = (state, onMoveUpClick, onMoveDownClick, onRemoveClick);
            var __isCreated = __composer.StartRestartGroup(1268661115);
            var __dirty = __changed;
            var __dirtyRestart = 0;
            if ((__changed & 0b_00_00_00_11) == 0)
            {
                __dirty |= __composer.ChangedAsStruct(state) ? 0b_00_00_00_10 : 0b_00_00_00_01;
            }
            else
            {
                __dirtyRestart |= 0b_00_00_00_01;
            }

            if ((__changed & 0b_00_00_11_00) == 0)
            {
                __dirty |= __composer.Changed(onMoveUpClick) ? 0b_00_00_10_00 : 0b_00_00_01_00;
            }
            else
            {
                __dirtyRestart |= 0b_00_00_01_00;
            }

            if ((__changed & 0b_00_11_00_00) == 0)
            {
                __dirty |= __composer.Changed(onMoveDownClick) ? 0b_00_10_00_00 : 0b_00_01_00_00;
            }
            else
            {
                __dirtyRestart |= 0b_00_01_00_00;
            }

            if ((__changed & 0b_11_00_00_00) == 0)
            {
                __dirty |= __composer.Changed(onRemoveClick) ? 0b_10_00_00_00 : 0b_01_00_00_00;
            }
            else
            {
                __dirtyRestart |= 0b_01_00_00_00;
            }

            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
            {
                __composer.StartReplaceGroup(496051479);
                if (state == 2)
                    __LaunchedEffect("state", static () =>
                    {
                    }, __composer: __composer, __changed: 0b_00_01);
                __composer.EndReplaceGroup(496051479);
                __Row(verticalAlignment: Alignment.CenterVertically, modifier: Modifier.Name("item-row").Background(Color.cyan).FillMaxWidth().Padding(all: 4.Px()).Border(radius: 12.Px()).Margin(vertical: 4.Px()), content: (!__composer.ChangedAsStruct((state, onMoveUpClick, onMoveDownClick, onRemoveClick)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Text(text: $"Item no. {state}", color: Color.black, fontSize: 40, modifier: Modifier.Name("item-name-label").Margin(left: 32.Px()), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                    __Spacer(Modifier.Weight(1), __composer: __composer, __changed: 0);
                    var counter = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                    __Text(text: counter.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Background(Color.green).Padding(horizontal: 16.Px()).Border(12.Px()).OnClick((!__composer.Changed(counter) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => counter.Value++))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                    __Column(content: (!__composer.ChangedAsStruct((onMoveUpClick, onMoveDownClick)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Text(text: "↑", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("up-arrow-button").Background(Color.green).Padding(horizontal: 6.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onMoveUpClick), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                        __Text(text: "↓", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("down-arrow-button").Background(Color.green).Padding(horizontal: 6.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onMoveDownClick), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                    __Text(text: "X", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("remove-button").Background(Color.red).Padding(horizontal: 16.Px(), vertical: 4.Px()).Border(radius: 16.Px()).OnClick(onRemoveClick), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1268661115, __isRestarted)?.UpdateScope(() => __Item(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick, __composer, __dirtyRestart));
        }
    }
}