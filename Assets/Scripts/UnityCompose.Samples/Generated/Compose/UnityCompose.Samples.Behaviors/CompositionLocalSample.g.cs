using StableCollections;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample
    {
        [Composable]
        [Compiled]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Content());
            }
        }

        [Composable]
        [Compiled]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        [Compiled]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Column(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: Modifier.Name("composition-local-sample").FlexGrow(1), content: RememberComposable<global::System.Action>(null, () =>
                {
                    var isSwitched = Remember(() => MutableStateOf(false));
                    CompositionLocalProvider(provides: IImmutableStableList.Create(LocalIsSwitched.Provides(isSwitched.Value)), content: SampleReader);
                    Label(text: "Switch", textColor: Color.white, fontSize: 32, style: Modifier.BackgroundColor(Color.blue).Padding(32).BorderRadius(16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)).MarginTop(80));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }

        [Composable]
        [Compiled]
        private static void __SampleReader()
        {
            if (CurrentComposer.BeginComposeGroup(null))
                return;
            try
            {
                Box(RememberComposable<global::System.Action>(null, () =>
                {
                    Box(RememberComposable<global::System.Action>(null, () =>
                    {
                        Spacer(style: Modifier.BackgroundColor(LocalIsSwitched.Current ? Color.green : Color.red, Transition()).Padding(100));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __SampleReader());
            }
        }
    }
}