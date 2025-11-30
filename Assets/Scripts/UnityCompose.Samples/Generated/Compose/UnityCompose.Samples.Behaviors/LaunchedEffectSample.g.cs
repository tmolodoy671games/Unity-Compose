using System;
using System.Collections;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class LaunchedEffectSample
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(964402292, true))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("launched-effect-disposal").FillMaxSize(), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1900480729, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var count = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<int>>(881651747, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<int>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<int>>(() => MutableStateOf(0));
                    Text(text: count.Value.ToString(), color: Color.white, fontSize: 40, modifier: Modifier.Name("test-label").Background(Color.red).Padding(all: 10));
                    var isEffectRunning = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-1413886213, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                    if (isEffectRunning.Value)
                    {
                        IEnumerator EffectCoroutine()
                        {
                            while (true)
                            {
                                yield return new WaitForSeconds(1f);
                                count.Value++;
                            }
                        }

                        LaunchedEffect(key: string.Empty, CurrentComposer.WithState(EffectCoroutine).Remember<Func<IEnumerator>>(static __ => () => __()));
                    }

                    var onOrOff = isEffectRunning.Value ? "On" : "Off";
                    var isHovered = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-89714165, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                    Text(text: $"Launched Effect is {onOrOff}", color: Color.white, fontSize: 40, modifier: Modifier.Name("test-button").Background(isHovered.Value ? Color.cyan : Color.blue, Transition()).Padding(vertical: 20).Padding(horizontal: isHovered.Value ? 40 : 20, transition: Transition()).Border(radius: 16).Margin(top: 32).OnMouseEnter(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-420369165, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = true)).OnMouseLeave(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(870908836, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = false)).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(1157415743, isEffectRunning) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isEffectRunning.Value = !isEffectRunning.Value)));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(964502292, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}