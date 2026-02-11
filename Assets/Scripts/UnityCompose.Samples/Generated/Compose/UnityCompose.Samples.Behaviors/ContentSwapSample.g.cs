#nullable enable
// ReSharper disable ArrangeNamespaceBody

using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ContentSwapSample
    {
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1629007819);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1629007819, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-86711710);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-86711710, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(1338862325);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Column(horizontalAlignment: Alignment.CenterHorizontally, verticalArrangement: Arrangement.Center, modifier: Modifier.FillMaxSize(), content: (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    if (isSwitched.Value)
                    {
                        __composer.StartReplaceGroup(-955378125);
                        Content2();
                        __composer.EndReplaceGroup(-955378125);
                    }
                    else
                    {
                        __composer.StartReplaceGroup(-1284654179);
                        Content1();
                        __composer.EndReplaceGroup(-1284654179);
                    }

                    Text(text: "Switch", color: Color.white, fontSize: 62, modifier: Modifier.Padding(horizontal: 20.Px(), vertical: 12.Px()).Border(16.Px()).Background(Color.blue).Margin(top: 16.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1338862325, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }

        private static void __Content1(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(1026411864);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Spacer(Modifier.Size(100.Px()).Background(Color.green));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1026411864, __isRestarted)?.UpdateScope(() => __Content1());
        }

        private static void __Content1()
        {
            __Content1(CurrentComposer);
        }

        private static void __Content2(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(23616230);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Row((!__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    Spacer(Modifier.Size(100.Px()).Background(Color.red));
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(23616230, __isRestarted)?.UpdateScope(() => __Content2());
        }

        private static void __Content2()
        {
            __Content2(CurrentComposer);
        }
    }
}