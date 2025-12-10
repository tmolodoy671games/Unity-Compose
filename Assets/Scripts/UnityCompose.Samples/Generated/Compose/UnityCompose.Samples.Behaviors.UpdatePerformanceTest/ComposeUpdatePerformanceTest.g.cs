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
                var parentSize = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>(IMutableStableProperty.Create(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(!__composer.Changed(parentSize) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.SizeWithPaddings)), content: !__composer.Changed(parentSize) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(853964739);
                    for (var i = 0; i < 1_000; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: !__composer.Changed((parentSize, currentI)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                        {
                            var position = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(Vector2.zero));
                            LaunchedEffect(key: string.Empty, coroutine: !__composer.Changed((parentSize, position)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => PerformanceUtils.MoveRandomlyCoroutine(parentSize: !__composer.Changed(parentSize) ? __composer.RememberedValue<System.Func<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<System.Func<UnityEngine.Vector2>>(() => parentSize.Value), !__composer.Changed(position) ? __composer.RememberedValue<System.Action<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<System.Action<UnityEngine.Vector2>>(it => position.Value = it))));
                            Spacer(modifier: Modifier.Size(50).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float().Position(left: position.Value.x, top: position.Value.y));
                        }));
                    }

                    __composer.EndReplaceGroup(853964739);
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