using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ButtonSampleContent
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(776275622, true))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1472395850, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var isHovered = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(1943989439, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                    Box(modifier: Modifier.Padding(horizontal: isHovered.Value ? 80 : 40, vertical: 16, transition: Transition()).Background(Color.blue).Border(radius: 16).OnMouseEnter(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(1281041416, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = true)).OnMouseLeave(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(1724576420, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = false)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1661219497, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(403584552, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: "Click me", fontSize: 24);
                        }));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(776375622, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }
    }
}