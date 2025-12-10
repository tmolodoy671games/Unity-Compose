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
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(776275622);
            if (__composer.ShouldExecute(true))
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize().Background(Color.white), content: !__composer.RememberedKeyChanged<bool>(-1472395850, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    var isHovered = !__composer.RememberedKeyChanged<bool>(1943989439, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
                    Box(modifier: Modifier.Padding(horizontal: isHovered.Value ? 80 : 40, vertical: 16, transition: Transition()).Background(Color.blue).Border(radius: 16).OnMouseEnter(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1281041416, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = true)).OnMouseLeave(!__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(1724576420, isHovered) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isHovered.Value = false)), content: !__composer.RememberedKeyChanged<bool>(1661219497, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        CompositionLocalProvider(LocalContentColor.Provides(Color.white), content: !__composer.RememberedKeyChanged<bool>(403584552, true) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: "Click me", fontSize: 24);
                        }));
                    }));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(776275622)?.UpdateScope(() => __Layout());
        }
    }
}