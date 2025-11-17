using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class AnimatedContentSample : ComposeUI
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
                const int Duration = 1;
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.WithState(Duration).Remember<Action>(__ => () =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("animated-content-sample"), content: CurrentComposer.WithState(__.Duration).Remember<Action>(__ => () =>
                    {
                        var animationSpec = Tween(easing: EaseInOutEasing, duration: Duration);
                        var isSwitched = Remember(static () => MutableStateOf(false));
                        AnimatedContent(targetState: isSwitched.Value ? "Looooooooooooooooooong" : "Short", transitionSpec: CurrentComposer.WithState((__.animationSpec, __.isSwitched)).Remember<Func>(__ => _ => isSwitched.Value ? SlideInVertically(static it => -it, animationSpec: animationSpec).TogetherWith(SlideOutVertically(static it => it, animationSpec: animationSpec)) : SlideInVertically(static it => it, animationSpec: animationSpec).TogetherWith(SlideOutVertically(static it => -it, animationSpec: animationSpec))), sizeAnimationSpec: animationSpec, modifier: Modifier.Name("animated-content").Background(isSwitched.Value ? Color.green : Color.red, Transition(Duration)), content: static state =>
                        {
                            Text(text: state.ToString(), color: Color.white, fontSize: 64);
                        });
                        Text(text: "Switch", color: Color.white, fontSize: 64, modifier: Modifier.Padding(horizontal: 100, vertical: 32).Background(Color.blue).Margin(top: 16).Border(radius: 16).OnClick(CurrentComposer.WithState(__.isSwitched).Remember<Action>(__ => () => isSwitched.Value = !isSwitched.Value)));
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }
    }
}