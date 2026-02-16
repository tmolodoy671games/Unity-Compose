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
        protected override void __Content(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(816199380);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(816199380, __isRestarted)?.UpdateScope(() => __Content(__composer, 0));
        }

        protected override void __Preview(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(941665577);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                __Layout(__composer: __composer, __changed: 0b_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(941665577, __isRestarted)?.UpdateScope(() => __Preview(__composer, 0));
        }

        private static void __Layout(global::UnityCompose.Composer __composer = null !, int __changed = -1)
        {
            var __isCreated = __composer.StartRestartGroup(1292165183);
            var __isRestarted = __composer.IsRestarted();
            if (__isCreated || __isRestarted || __changed != 0b_00)
            {
                var parentCoordinates = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityCompose.LayoutCoordinates>>>(MutableStateOf(Optional.Empty<LayoutCoordinates>())));
                __Column(horizontalAlignment: Alignment.CenterHorizontally, modifier: Modifier.FillMaxSize().Padding(100.Px()).OnGloballyPositioned((!__composer.Changed(parentCoordinates) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => parentCoordinates.Value = it))), content: (!__composer.Changed(parentCoordinates) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
                    var layout = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityEngine.Vector2>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::SharpExtensions.Optional<global::UnityEngine.Vector2>>>(MutableStateOf(Optional.Empty<Vector2>())));
                    __Box(modifier: Modifier.FillMaxSize(), content: (!__composer.ChangedAsStruct((isSwitched, layout)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var transitionSpec = Tween();
                        __Box(alignment: Alignment.Center, modifier: Modifier.Size(40.Px()).Background(Color.blue).Offset(x: __AnimateFloatAsState(targetValue: 500 * isSwitched.Value.ToInt(), animationSpec: transitionSpec, __composer: __composer, __changed: 0b_00_00).Value.Px()), content: (!__composer.Changed(layout) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                        {
                            __Box((!__composer.Changed(layout) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                            {
                                __Box((!__composer.Changed(layout) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                                {
                                    __Spacer(Modifier.Background(Color.green).Size(20.Px()).OnGloballyPositioned((!__composer.Changed(layout) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it => layout.Value = it.GlobalCenter))), __composer: __composer, __changed: 0b_00);
                                })), __composer: __composer, __changed: 0b_01_01_00);
                            })), __composer: __composer, __changed: 0b_01_01_00);
                        })), __composer: __composer, __changed: 0b_00_00_00);
                    })), __composer: __composer, __changed: 0b_01_00_00);
                    __Text(modifier: Modifier.Background(Color.blue).Padding(32.Px()).Border(32.Px()).OnClick((!__composer.Changed(isSwitched) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value))), color: Color.white, text: "Switch", __composer: __composer, __changed: 0b_01_01_01_01_01_01_00_01);
                    __composer.StartReplaceGroup(835651239);
                    if (layout.Value.HasValue && parentCoordinates.Value.HasValue)
                    {
                        var parentCoordinatesValue = parentCoordinates.Value.Value;
                        __Spacer(modifier: Modifier.Size(10.Px()).Background(Color.red).Float().Position(left: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).x.Px(), top: parentCoordinatesValue.GlobalToLocal(layout.Value.Value).y.Px()), __composer: __composer, __changed: 0b_00);
                    }

                    __composer.EndReplaceGroup(835651239);
                })), __composer: __composer, __changed: 0b_01_00_00_00);
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(1292165183, __isRestarted)?.UpdateScope(() => __Layout(__composer, 0));
        }
    }
}