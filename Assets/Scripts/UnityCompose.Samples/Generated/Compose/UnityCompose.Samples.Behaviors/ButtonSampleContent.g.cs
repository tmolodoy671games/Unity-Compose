#nullable enable
// ReSharper disable ArrangeNamespaceBody

using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(264962251);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(264962251, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1172909443);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1172909443, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1125058844);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Box(alignment: Alignment.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isHovered = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var isPressed = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var isCapturingPointer = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var circleColor = __AnimateColorAsState(isPressed.Value ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f), Tween(1), __composer: __composer, __changed: 0b_01_00_00).Value;
                    __Box(modifier: Modifier.Padding(horizontal: __AnimateFloatAsState(isHovered.Value ? 80 : 40, __composer: __composer, __changed: 0b_01_01_00).Value.Px(), vertical: 16.Px()).Background(__AnimateColorAsState(isPressed.Value ? Color.darkBlue : Color.blue, __composer: __composer, __changed: 0b_01_01_00).Value).Border(radius: 16.Px()).OnMouseEnter((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = true))).OnMouseLeave((!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isHovered!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isHovered.Value = false))).CapturePointer(isCapturingPointer.Value).DrawAfter((!__composer.Changed<global::UnityEngine.Color>(circleColor!) ? __composer.RememberedValue<global::System.Action<global::UnityEngine.UIElements.MeshGenerationContext>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityEngine.UIElements.MeshGenerationContext>>(it =>
                    {
                        var size = it.visualElement.layout.size;
                        it.painter2D.fillColor = circleColor;
                        it.painter2D.BeginPath();
                        it.painter2D.Arc(size / 2, 50, 0, 360);
                        it.painter2D.Fill();
                    }))).Clip().Scale(2).OnLmbClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("LMB")))).OnRmbClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("RMB")))).OnMmbClick((!__composer.Changed() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => Debug.Log("MMB")))).OnLmbDown((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isPressed!).Changed<global::UnityCompose.IMutableState<bool>>(isCapturingPointer!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        isPressed.Value = true;
                        isCapturingPointer.Value = true;
                    }))).OnLmbUp((!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isPressed!).Changed<global::UnityCompose.IMutableState<bool>>(isCapturingPointer!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
                    {
                        isPressed.Value = false;
                        isCapturingPointer.Value = false;
                    }))), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isPressed!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        __CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isPressed!) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Text(text: "Click me", fontSize: 24, modifier: Modifier.Scale(__AnimateFloatAsState(isPressed.Value ? 0.6f : 1f, __composer: __composer, __changed: 0b_01_01_00).Value).Alpha(__AnimateFloatAsState(isPressed.Value ? 0.6f : 1f, __composer: __composer, __changed: 0b_01_01_00).Value), __composer: __composer, __changed: 0b_01_01_01_01_01_01_01_01);
                        })), __composer: __composer, __changed: 0b_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                })), __composer: __composer, __changed: 0b_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1125058844, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }

    partial interface IInterface
    {
        int __Foo(int a, int b, global::UnityCompose.Composer __composer = null !, int __changed = -1);
    }

    partial class MyClass
    {
        public int __Foo(int a, int b, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var(__a, __b) = (a, b);
            __composer.StartReplaceGroup(50560639);
            var __dirty = __changed;
            if ((__changed & 0b_00_11) == 0)
                __dirty |= __composer.Changed(a) ? 0b_00_10 : 0b_00_01;
            if ((__changed & 0b_11_00) == 0)
                __dirty |= __composer.Changed(b) ? 0b_10_00 : 0b_01_00;
            __composer.EndReplaceGroup(50560639);
            return 1;
        }
    }
}