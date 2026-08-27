#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LazyLayoutSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1681023109);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1681023109, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(570972521);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(570972521, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(761465125);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __ColumnSample(__composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Height(100.Px()), __composer: __composer, __changed: 0b_00);
                        __RowSample(__composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_00_01_00);
                    __Spacer(Modifier.Float().Size(100.Percent()).IgnoreInput(), __composer: __composer, __changed: 0b_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(761465125, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __ColumnSample(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(760182512);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var state = __RememberLazyListState(__composer: __composer, __changed: 0b_00);
                __Row((!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.Height(400.Px()), content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __LazyColumn(state: state, content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ILazyListScope>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ILazyListScope>>(scope =>
                        {
                            scope.Items(count: 20, key: it => it, content: it =>
                            {
                                __Text(text: it.ToString(), color: Color.white, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Background(Color.red).Size(100.Px()).Margin(vertical: 4.Px()).OnClick((!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(state!).Changed<int>(it!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollToItem(it)))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                            });
                        })), __composer: __composer, __changed: 0b_01_01_01_01_00_00);
                        __Box(modifier: Modifier.Height(100.Percent()).Float().Position(right: 0.Px()), content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            var scrollerSize = state.ViewportSize / state.ContentSize;
                            __Spacer(Modifier.Background(Color.cadetBlue).Width(32.Px()).Border(16.Px()).Height(scrollerSize * 100.Percent()).Position(top: (state.Value / state.ContentSize) * 100.Percent()), __composer: __composer, __changed: 0b_00);
                        })), __composer: __composer, __changed: 0b_01_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(760182512, __isRestarted)?.UpdateScope(() => __ColumnSample(__composer, 0));
        }

        private static void __RowSample(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(463258108);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var state = __RememberLazyListState(__composer: __composer, __changed: 0b_00);
                __Column((!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __LazyRow(state: state, modifier: Modifier.Width(400.Px()), content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.ILazyListScope>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.ILazyListScope>>(scope =>
                    {
                        __Row(modifier: Modifier.Background(Color.white), content: () =>
                        {
                            scope.Items(count: 20, key: (!__composer.Changed() ? __composer.RememberedValue<global::System.Func<int, object>>() : __composer.UpdateRememberedValue<global::System.Func<int, object>>(it => it)), content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent<int>>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent<int>>(it =>
                            {
                                __Text(text: it.ToString(), color: Color.white, fontSize: 32, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Background(Color.red).Size(100.Px()).Margin(horizontal: 4.Px()).OnClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("Glick")))).OnClick((!__composer.BuildChanged().Changed<global::UnityCompose.LazyListState>(state!).Changed<int>(it!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollToItem(it)))), __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_00);
                            })));
                        }, __composer: __composer, __changed: 0b_01_01_00_00);
                    })), __composer: __composer, __changed: 0b_01_01_01_00_00);
                    const float width = 32;
                    __Row(modifier: Modifier.FillMaxWidth().Height(width.Px()), content: (!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Spacer(Modifier.Size(width.Px()).Background(Color.forestGreen).OnClick((!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollToItem(0)))), __composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Weight(1).Background(Color.indianRed), __composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Size(width.Px()).Background(Color.forestGreen).OnClick((!__composer.Changed<global::UnityCompose.LazyListState>(state!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollToItem(19)))), __composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_01_00_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(463258108, __isRestarted)?.UpdateScope(() => __RowSample(__composer, 0));
        }
    }
}