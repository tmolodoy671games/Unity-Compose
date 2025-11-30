using StableCollections;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(1449131958, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1449231958, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(1367538738, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1367638738, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(-597693002, true))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1301388334, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(812200372, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
                    CompositionLocalProvider(LocalIsSwitched.Provides(isSwitched.Value), content: SampleReader);
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32).Border(radius: 16).OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-900605980, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value)).Margin(top: 80));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-597593002, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }

        [Composable]
        private static void __SampleReader()
        {
            if (CurrentComposer.BeginComposeGroup(-943066722, true))
                return;
            try
            {
                Box(CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1151300559, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    Box(CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1018330227, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, transition: Transition()).Padding(all: 100));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-942966722, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __SampleReader()));
            }
        }
    }
}