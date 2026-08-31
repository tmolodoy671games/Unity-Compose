#nullable enable
using System;
using StableCollections;
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
            var __isCreated = __composer.StartRestartGroup(1224052590);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1224052590, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(418793603);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(418793603, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1657218070);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier, content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("reordarable-list-sample").Padding(top: 100.Dp()).FillMaxHeight().Width(800.Dp()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var items = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableStateList<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableStateList<int>>(MutableStateListOf(1, 2)));
                        __Text(text: "Add Item", color: Color.white, fontSize: 40.Sp(), modifier: Modifier.Name("add-item-button").Align(Alignment.Right).Background(Color.blue).Padding(horizontal: 32.Dp(), vertical: 16.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick((!__composer.Changed<global::UnityCompose.IMutableStateList<int>>(items!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                        {
                            for (var i = 1; i <= items.Count + 1; i++)
                            {
                                if (!items.Contains(i))
                                {
                                    items.Add(i);
                                    return;
                                }
                            }
                        }))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                        var immutableItems = items.ToImmutableStableList();
                        __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.Name("nested-column").FillMaxWidth(), content: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableStateList<int>>(items!).Changed<global::StableCollections.IImmutableStableList<int>>(immutableItems!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(684001779);
                            foreach (var item in immutableItems)
                            {
                                Key(key: item, content: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableStateList<int>>(items!).Changed<int>(item!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                                {
                                    __Item(state: item, onMoveUpClick: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableStateList<int>>(items!).Changed<int>(item!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == 0)
                                            return;
                                        var newIndex = oldIndex - 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    })), onMoveDownClick: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableStateList<int>>(items!).Changed<int>(item!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                                    {
                                        var oldIndex = items.IndexOf(item);
                                        if (oldIndex == items.Count - 1)
                                            return;
                                        var newIndex = oldIndex + 1;
                                        items.RemoveAt(oldIndex);
                                        items.Insert(newIndex, item);
                                    })), onRemoveClick: (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableStateList<int>>(items!).Changed<int>(item!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => items.Remove(item))), __composer: __composer, __changed: 0b_00_00_00_00);
                                })));
                            }

                            __composer.EndReplaceGroup(684001779);
                        })), __composer: __composer, __changed: 0b_01_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1657218070, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick) = (state, onMoveUpClick, onMoveDownClick, onRemoveClick);
            var __isCreated = __composer.StartRestartGroup(881946581);
            var __dirty = __changed;
            if ((__changed & 0b_00_00_00_11) == 0)
                __dirty |= __composer.Changed(state) ? 0b_00_00_00_10 : 0b_00_00_00_01;
            if ((__changed & 0b_00_00_11_00) == 0)
                __dirty |= __composer.Changed(onMoveUpClick) ? 0b_00_00_10_00 : 0b_00_00_01_00;
            if ((__changed & 0b_00_11_00_00) == 0)
                __dirty |= __composer.Changed(onMoveDownClick) ? 0b_00_10_00_00 : 0b_00_01_00_00;
            if ((__changed & 0b_11_00_00_00) == 0)
                __dirty |= __composer.Changed(onRemoveClick) ? 0b_10_00_00_00 : 0b_01_00_00_00;
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
            {
                __composer.StartReplaceGroup(1442143634);
                if (state == 2)
                    __SideEffect("state", static () =>
                    {
                    }, __composer: __composer, __changed: 0b_00_01);
                __composer.EndReplaceGroup(1442143634);
                __Row(verticalAlignment: Alignment.CenterVertically, modifier: Modifier.Name("item-row").Background(Color.cyan).FillMaxWidth().Padding(all: 4.Dp()).Clip(RoundedCornerShape(12.Dp())).Margin(vertical: 4.Dp()), content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00) == 0b_00_00_10_00).ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).ChangedAsFlag((__dirty & 0b_11_00_00_00) == 0b_10_00_00_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Text(text: $"Item no. {state}", color: Color.black, fontSize: 40.Sp(), modifier: Modifier.Name("item-name-label").Margin(left: 32.Dp()), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                    __Spacer(Modifier.Weight(1), __composer: __composer, __changed: 0b_00);
                    var counter = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<int>>(MutableStateOf(0)));
                    __Text(text: counter.Value.ToString(), color: Color.white, fontSize: 40.Sp(), modifier: Modifier.Background(Color.green).Padding(horizontal: 16.Dp()).Clip(RoundedCornerShape(12.Dp())).OnClick((!__composer.Changed<global::UnityCompose.IMutableState<int>>(counter!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => counter.Value++))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                    __Column(content: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11_00) == 0b_00_00_10_00).ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Text(text: "↑", color: Color.white, fontSize: 40.Sp(), fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("up-arrow-button").Background(Color.green).Padding(horizontal: 6.Dp(), vertical: 4.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick(onMoveUpClick), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                        __Text(text: "↓", color: Color.white, fontSize: 40.Sp(), fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("down-arrow-button").Background(Color.green).Padding(horizontal: 6.Dp(), vertical: 4.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick(onMoveDownClick), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                    })), __composer: __composer, __changed: 0b_01_01_01_00);
                    __Text(text: "X", color: Color.white, fontSize: 40.Sp(), fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("remove-button").Background(Color.red).Padding(horizontal: 16.Dp(), vertical: 4.Dp()).Clip(RoundedCornerShape(16.Dp())).OnClick(onRemoveClick), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_01);
                })), __composer: __composer, __changed: 0b_01_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __dirty = 0b_01_01_01_01;
            __composer.EndRestartGroup(881946581, __isRestarted)?.UpdateScope(() => __Item(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick, __composer, __composer.UpdateChangedFlags(__changed)));
        }
    }
}