using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(-1183406456, true))
                return;
            try
            {
                Preview();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-1183306456, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(-2102891545, true))
                return;
            try
            {
                var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-894395642, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => LoggableMutableStateOf(false));
                var isHovered = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-207343291, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => LoggableMutableStateOf(false));
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<bool>?>, UnityCompose.ComposableContent>(-1524387000, (isSwitched, isHovered)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<bool>?>, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<ValueTuple<UnityCompose.IMutableState<bool>?, UnityCompose.IMutableState<bool>?>, UnityCompose.ComposableContent>(() =>
                {
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }

                    Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Background(Color.blue).Border(16).Size(100).Scale(isSwitched.Value ? 1.5f : 1, transition: Transition())// .Scale(AnimateFloatAsState(isHovered.Value ? 1.5f : 1).Value)
                    .OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(237932776, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)).OnMouseEnter(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1871759484, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = true)).OnMouseLeave(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-1154458528, isHovered) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isHovered.Value = false)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-934727963, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        Box(modifier: Modifier.Size(50).Background(Color.red).Border(16), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1761317323, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                        {
                            Text(text: "Text", color: Color.white);
                        }));
                    }));
                    if (isSwitched.Value)
                    {
                        Spacer(modifier: Modifier.Size(50).Background(Color.green).Border(16).Margin(top: 100));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-2102791545, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }
    }
}