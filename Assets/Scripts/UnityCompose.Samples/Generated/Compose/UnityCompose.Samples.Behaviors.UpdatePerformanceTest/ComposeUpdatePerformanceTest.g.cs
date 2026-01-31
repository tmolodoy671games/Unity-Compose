#nullable enable
// ReSharper disable ArrangeNamespaceBody

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
            __composer.StartRestartGroup(-3566712);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecute())
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
                var parentSize = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(!__composer.Changed(parentSize) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.Size)), content: !__composer.Changed(parentSize) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(-1991249029);
                    for (var i = 0; i < 1_00; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: !__composer.ChangedAsStruct((parentSize, currentI)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => Item(currentI, parentSize.Value)));
                    }

                    __composer.EndReplaceGroup(-1991249029);
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-3566712, __isRestarted)?.UpdateScope(() => __Content());
        }

        [Composable]
        private static void __Item(int currentI, Vector2 parentSize)
        {
            var(__currentI, __parentSize) = (currentI, parentSize);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-760552624);
            var __isRestarted = __composer.IsRestarted();
            if (__isRestarted || __composer.ShouldExecuteAsStruct((__currentI, __parentSize)))
            {
                var position = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(Vector2.zero));
                LaunchedEffect(key: parentSize, coroutine: !__composer.ChangedAsStruct((parentSize, position)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => PerformanceUtils.MoveRandomlyCoroutine(parentSize: () => parentSize, it => position.Value = it)));
                var baseModifier = !__composer.ChangedAsStruct(currentI) ? __composer.RememberedValue<UnityCompose.IModifier>() : __composer.UpdateRememberedValue<UnityCompose.IModifier>(Modifier.Size(50.Px()).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float());
                Spacer(modifier: baseModifier.Position(left: position.Value.x.Px(), top: position.Value.y.Px()));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-760552624, __isRestarted)?.UpdateScope(() => __Item(__currentI, __parentSize));
        }
    }
}