#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ScrollableLayoutSample
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(226225244);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(226225244, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1196602079);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1196602079, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1710746034);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Column(horizontalAlignment: Alignment.CenterHorizontally, content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __ColumnSample(__composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Height(100.Dp()), __composer: __composer, __changed: 0b_00);
                        __RowSample(__composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_00_01_00);
                    __Spacer(Modifier.Float().Size(100.Percent()).IgnoreInput(), __composer: __composer, __changed: 0b_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1710746034, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }

        private static void __ColumnSample(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(717473453);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var state = __RememberScrollState(__composer: __composer, __changed: 0b_01);
                __Row((!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __Box(modifier: Modifier.Height(400.Dp()), content: (!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __ScrollableColumn(state: state, content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Column(modifier: Modifier.Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                            {
                                __composer.StartReplaceGroup(1700970714);
                                for (var i = 0; i < 20; i++)
                                {
                                    __Text(text: i.ToString(), color: Color.white, fontSize: 32.Sp(), textAlign: TextAlign.MiddleCenter, modifier: Modifier.Background(Color.red).Size(100.Dp()).Margin(vertical: 4.Dp()).OnClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("Glick")))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                                }

                                __composer.EndReplaceGroup(1700970714);
                            })), __composer: __composer, __changed: 0b_01_01_00_00);
                        })), __composer: __composer, __changed: 0b_01_01_00_00);
                        __Box(modifier: Modifier.Height(100.Percent()).Float().Position(right: 0.Dp()), content: (!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            var scrollerSize = state.ViewportSize / state.ContentSize;
                            __Spacer(Modifier.Background(Color.cadetBlue).Width(32.Dp()).Clip(RoundedCornerShape(16.Dp())).Height(scrollerSize * 100.Percent()).Position(top: (state.Value / state.ContentSize) * 100.Percent()), __composer: __composer, __changed: 0b_00);
                        })), __composer: __composer, __changed: 0b_01_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(717473453, __isRestarted)?.UpdateScope(() => __ColumnSample(__composer, 0));
        }

        private static void __RowSample(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(468642304);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var state = __RememberScrollState(__composer: __composer, __changed: 0b_01);
                __Column((!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    __ScrollableRow(state: state, modifier: Modifier.Width(400.Dp()), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Row(modifier: Modifier.Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __composer.StartReplaceGroup(2011896446);
                            for (var i = 0; i < 20; i++)
                            {
                                __Text(text: i.ToString(), color: Color.white, fontSize: 32.Sp(), textAlign: TextAlign.MiddleCenter, modifier: Modifier.Background(Color.red).Size(100.Dp()).Margin(horizontal: 4.Dp()).OnClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("Glick")))), __composer: __composer, __changed: 0b_01_01_01_01_01_00_00_00);
                            }

                            __composer.EndReplaceGroup(2011896446);
                        })), __composer: __composer, __changed: 0b_01_01_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                    const float width = 32;
                    __Row(modifier: Modifier.FillMaxWidth().Height(width.Dp()), content: (!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __Spacer(Modifier.Size(width.Dp()).Background(Color.forestGreen).OnClick((!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollBy(-200)))), __composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Weight(1).Background(Color.indianRed), __composer: __composer, __changed: 0b_00);
                        __Spacer(Modifier.Size(width.Dp()).Background(Color.forestGreen).OnClick((!__composer.Changed<global::UnityCompose.ScrollState>(state!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => state.AnimateScrollBy(200)))), __composer: __composer, __changed: 0b_00);
                    })), __composer: __composer, __changed: 0b_01_01_00_00);
                })), __composer: __composer, __changed: 0b_01_01_01_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(468642304, __isRestarted)?.UpdateScope(() => __RowSample(__composer, 0));
        }
    }
}