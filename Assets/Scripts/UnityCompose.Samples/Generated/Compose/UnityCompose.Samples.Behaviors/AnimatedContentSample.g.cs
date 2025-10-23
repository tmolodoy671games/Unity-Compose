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
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, style: Modifier.FillMaxSize(), content: RememberComposable<global::System.Action>(Duration, () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, style: Modifier.Name("animated-content-sample"), content: RememberComposable<global::System.Action>(Duration, () =>
                    {
                        var isSwitched = Remember(() => MutableStateOf(false));
                        AnimatedContent(value: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transition: Remember<global::System.Func<string, string, global::UnityCompose.ContentTransform>>(isSwitched, (_, _) => isSwitched.Value ? ContentTransform(enter: SlideIn(SlideDirection.Up) + FadeIn(), exit: SlideOut(SlideDirection.Up) + FadeOut()) : ContentTransform(enter: SlideIn(SlideDirection.Down) + FadeIn(), exit: SlideOut(SlideDirection.Down) + FadeOut())), animateSize: true, transitionDuration: Duration, style: Modifier.Name("animated-content").Background(isSwitched.Value ? Color.green : Color.red, Transition(Duration)), content: RememberComposable<global::System.Action<string>>(null, state =>
                        {
                            Text(text: state.ToString(), textColor: Color.white, fontSize: 64);
                        }));
                        Text(text: "Switch", textColor: Color.white, fontSize: 64, style: Modifier.NewPadding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value)));
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