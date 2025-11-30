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
            if (CurrentComposer.BeginComposeGroup(-2129149783, true))
                return;
            try
            {
                var parentSize = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.Vector2>>(1393210914, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.Vector2>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<UnityEngine.Vector2>>(() => IMutableStableProperty.Create(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(CurrentComposer.HasRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(143329175, parentSize) ? CurrentComposer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it => parentSize.Value = it.SizeWithPaddings)), content: CurrentComposer.HasRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, UnityCompose.ComposableContent>(944826466, parentSize) ? CurrentComposer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, UnityCompose.ComposableContent>(() =>
                {
                    for (var i = 0; i < 1_000; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: CurrentComposer.HasRememberedValue<ValueTuple<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, int>, System.Action>(-52083886, (parentSize, currentI)) ? CurrentComposer.RememberedValue<ValueTuple<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, int>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<StableCollections.IMutableStableProperty<UnityEngine.Vector2>?, int>, System.Action>(() =>
                        {
                            var position = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(1726011018, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(static () => MutableStateOf(Vector2.zero));
                            LaunchedEffect(key: string.Empty, coroutine: () => PerformanceUtils.MoveRandomlyCoroutine(parentSize: () => parentSize.Value, it => position.Value = it));
                            Spacer(modifier: Modifier.Size(50).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float().Position(left: position.Value.x, top: position.Value.y));
                        }));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-2129049783, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }
    }
}