using System.Collections;
using StableCollections;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors.UpdatePerformanceTest
{
    internal partial class ComposeUpdatePerformanceTest
    {
        [Composable]
        private void __Content()
        {
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-2129149783);
            if (__composer.ShouldExecute(true))
            {
                var parentSize = !__composer.RememberedKeyChanged<bool>(1393210914, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>(IMutableStableProperty.Create(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(!__composer.RememberedKeyChanged<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?>(143329175, parentSize) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.SizeWithPaddings)), content: !__composer.RememberedKeyChanged<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?>(944826466, parentSize) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(-848998979);
                    for (var i = 0; i < 1_000; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: !__composer.RememberedKeyChanged<ValueTuple<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, int>>(-52083886, (parentSize, currentI)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                        {
                            var position = !__composer.RememberedKeyChanged<bool>(1726011018, true) ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(Vector2.zero));
                            LaunchedEffect(key: string.Empty, coroutine: !__composer.RememberedKeyChanged<ValueTuple<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, UnityCompose.IMutableState<UnityEngine.Vector2>?>>(187652311, (parentSize, position)) ? CurrentComposer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : CurrentComposer.UpdateLambda<System.Func<System.Collections.IEnumerator>>(() => PerformanceUtils.MoveRandomlyCoroutine(parentSize: !__composer.RememberedKeyChanged<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?>(-557200043, parentSize) ? CurrentComposer.RememberedValue<System.Func<UnityEngine.Vector2>>() : CurrentComposer.UpdateLambda<System.Func<UnityEngine.Vector2>>(() => parentSize.Value), !__composer.RememberedKeyChanged<UnityCompose.IMutableState<UnityEngine.Vector2>?>(-884454977, position) ? CurrentComposer.RememberedValue<System.Action<UnityEngine.Vector2>>() : CurrentComposer.UpdateLambda<System.Action<UnityEngine.Vector2>>(it => position.Value = it))));
                            Spacer(modifier: Modifier.Size(50).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float().Position(left: position.Value.x, top: position.Value.y));
                        }));
                    }

                    __composer.EndReplaceGroup(-848998979);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-2129149783)?.UpdateScope(() => __Content());
        }
    }
}