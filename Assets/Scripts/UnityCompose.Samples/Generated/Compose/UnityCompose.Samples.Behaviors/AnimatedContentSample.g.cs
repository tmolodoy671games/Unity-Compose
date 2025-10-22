using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample
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
                const int Duration = 1;
                Box(alignHorizontally: Align.Center, alignVertically: Justify.Center, style: ComposeStyle.Empty.Width(100.Percent()).Height(100.Percent()).FlexGrow(1), content: RememberComposable<global::System.Action>(Duration, () =>
                {
                    Column(alignHorizontally: Align.Center, style: ComposeStyle.Empty.Name("animated-content-sample"), content: RememberComposable<global::System.Action>(Duration, () =>
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        AnimatedContent(value: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transition: Remember<global::System.Func<string, string, global::UnityCompose.ContentTransform>>(isSwitched, (_, _) => isSwitched.Value ? ContentTransform(enter: SlideIn(SlideDirection.Up) + FadeIn(), exit: SlideOut(SlideDirection.Up) + FadeOut()) : ContentTransform(enter: SlideIn(SlideDirection.Down) + FadeIn(), exit: SlideOut(SlideDirection.Down) + FadeOut())), animateSize: true, transitionDuration: Duration, style: ComposeStyle.Empty.Name("animated-content").BackgroundColor(isSwitched.Value ? Color.green : Color.red, Transition(Duration)), content: RememberComposable<global::System.Action<string>>(null, state =>
                        {
                            Label(text: state.ToString(), textColor: Color.white, fontSize: 64);
                        }));
                        Label(text: "Switch", textColor: Color.white, fontSize: 64, style: ComposeStyle.Empty.Padding(100, 32).BackgroundColor(Color.blue).MarginTop(16).BorderRadius(16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}