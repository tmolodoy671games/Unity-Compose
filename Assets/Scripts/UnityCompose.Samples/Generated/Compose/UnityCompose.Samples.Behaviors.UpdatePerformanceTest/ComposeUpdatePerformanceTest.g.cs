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
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                var parentSize = Remember(() => IMutableStableProperty.Create(Vector2.zero));
                Box(modifier: Modifier.FillMaxSize().OnGloballyPositioned(CurrentComposer.WithState(parentSize).Remember<System.Action<UnityCompose.LayoutCoordinates>>(__ => it => parentSize.Value = it.SizeWithPaddings)), content: CurrentComposer.WithState(parentSize).Remember<System.Action>(__ => () =>
                {
                    for (var i = 0; i < 1_000; i++)
                    {
                        var currentI = i;
                        Key(key: currentI, content: CurrentComposer.WithState((parentSize, currentI)).Remember<System.Action>(__ => () =>
                        {
                            var position = Remember(static () => MutableStateOf(Vector2.zero));
                            LaunchedEffect(key: string.Empty, coroutine: CurrentComposer.WithState((parentSize, position)).Remember<System.Func<System.Collections.IEnumerator>>(__ => () => PerformanceUtils.MoveRandomlyCoroutine(parentSize: CurrentComposer.WithState(parentSize).Remember<System.Func<UnityEngine.Vector2>>(__ => () => parentSize.Value), CurrentComposer.WithState(position).Remember<System.Action<UnityEngine.Vector2>>(__ => it => position.Value = it))));
                            Spacer(modifier: Modifier.Size(50).Background(PerformanceUtils.Colors[currentI % PerformanceUtils.Colors.Length]).Float().Position(left: position.Value.x, top: position.Value.y));
                        }));
                    }

                    var fps = Remember(() => MutableStateOf(0));
                    LaunchedEffect(string.Empty, CurrentComposer.WithState(fps).Remember<System.Func<System.Collections.IEnumerator>>(__ => () => PerformanceUtils.MeasureFpsCoroutine(CurrentComposer.WithState(fps).Remember<System.Action<int>>(__ => it => fps.Value = it))));
                    Text(text: fps.Value.ToString(), color: Color.white, modifier: Modifier.Float().Background(Color.black).Position(right: 40, top: 40));
                }));
                LaunchedEffect(string.Empty, static () => PrintStats());
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }
    }
}