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
                var parentSize = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>>(IMutableStableProperty.Create(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(!__composer.Changed(parentSize) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.SizeWithPaddings)), content: !__composer.Changed(parentSize) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                {
                    __composer.StartReplaceGroup(-1991249029);
                    for (var i = 0; i < 1_00; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: !__composer.ChangedAsStruct((parentSize, currentI)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            var position = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(Vector2.zero));
                            LaunchedEffect(key: string.Empty, coroutine: !__composer.ChangedAsStruct((parentSize, position)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => PerformanceUtils.MoveRandomlyCoroutine(parentSize: () => parentSize.Value, it => position.Value = it)));
                            var baseModifier = !__composer.ChangedAsStruct(currentI) ? __composer.RememberedValue<UnityCompose.IModifier>() : __composer.UpdateRememberedValue<UnityCompose.IModifier>(Modifier.Size(50).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float());
                            Spacer(modifier: baseModifier.Position(left: position.Value.x, top: position.Value.y));
                        }));
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
    }
}