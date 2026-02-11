#nullable enable
// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositionedSample
    {
        private void __Content(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-816199380);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-816199380, __isRestarted)?.UpdateScope(() => __Content());
        }

        private void __Content()
        {
            __Content(CurrentComposer);
        }

        private void __Preview(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-941665577);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                Layout();
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-941665577, __isRestarted)?.UpdateScope(() => __Preview());
        }

        private void __Preview()
        {
            __Preview(CurrentComposer);
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !)
        {
            __composer.StartRestartGroup(-1292165183);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                var parentCoordinates = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>())));
                Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.FillMaxSize().Padding(100.Px()).OnGloballyPositioned((!__composer.Changed(parentCoordinates) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => parentCoordinates.Value = it))), content: (!__composer.Changed(parentCoordinates) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var layout = (!__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<SharpExtensions.Optional<UnityEngine.Vector2>>>(MutableStateOf(Optional.Empty<Vector2>())));
                    Box(modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct((isSwitched, layout)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var transitionSpec = Tween();
                        Box(alignment: Alignment.Center, modifier: Modifier.Size(40.Px()).Background(Color.blue).Offset(x: AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec).Value.Px()), content: (!__composer.Changed(layout) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            Box((!__composer.Changed(layout) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                            {
                                Box((!__composer.Changed(layout) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                                {
                                    Spacer(Modifier.Background(Color.green).Size(20.Px()).OnGloballyPositioned((!__composer.Changed(layout) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => layout.Value = it.GlobalCenter))));
                                })));
                            })));
                        })));
                    })));
                    Text(modifier: Modifier.Background(Color.blue).Padding(32.Px()).Border(32.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value))), color: Color.white, text: "Switch");
                    __composer.StartReplaceGroup(-835651239);
                    if (layout.Value.HasValue && parentCoordinates.Value.HasValue)
                    {
                        var parentCoordinatesValue = parentCoordinates.Value.Value;
                        Spacer(modifier: Modifier.Size(10.Px()).Background(Color.red).Float().Position(left: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).x.Px(), top: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).y.Px()));
                    }

                    __composer.EndReplaceGroup(-835651239);
                })));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1292165183, __isRestarted)?.UpdateScope(() => __Layout());
        }

        private static void __Layout()
        {
            __Layout(CurrentComposer);
        }
    }
}