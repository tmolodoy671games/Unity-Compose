using StableCollections;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompositionLocalSample : ComposeUI
    {
        [Composable]
        protected override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }

        [Composable]
        protected override void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Preview());
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Column(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("composition-local-sample").FillMaxSize(), content: static () =>
                {
                    var isSwitched = Remember(static () => MutableStateOf(false));
                    CompositionLocalProvider(provides: IImmutableStableList.Create(LocalIsSwitched.Provides(isSwitched.Value)), content: SampleReader);
                    Text(text: "Switch", color: Color.white, fontSize: 32, modifier: Modifier.Background(Color.blue).Padding(all: 32).Border(radius: 16).OnClick(CurrentComposer.WithState(isSwitched).Remember<Action>(__ => () => isSwitched.Value = !isSwitched.Value)).Margin(top: 80));
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }

        [Composable]
        private static void __SampleReader()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(static () =>
                {
                    Box(static () =>
                    {
                        Spacer(modifier: Modifier.Background(LocalIsSwitched.Current ? Color.green : Color.red, Transition()).Padding(all: 100));
                    });
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __SampleReader());
            }
        }
    }
}