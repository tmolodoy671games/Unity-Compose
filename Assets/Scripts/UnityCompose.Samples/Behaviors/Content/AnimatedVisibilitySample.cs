using System;
using Compose.Net;
using SharpExtensions;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class AnimatedVisibilitySample : ComposeUI
    {
        [Composable]
        protected override void Content() => Layout();

        [Composable]
        protected override void Preview() => Layout();

        [Composable]
        private static void Layout()
        {
            Box(
                alignment: Alignment.Center,
                modifier: Modifier.FillMaxSize(),
                content: () =>
                {
                    var isVisible = Remember(() => MutableStateOf(true));
                    Spacer(
                        Modifier
                            .Size(100.Dp(), 40.Dp())
                            .Background(Color.forestGreen.ToSystemColor())
                            .Clip(RoundedCornerShape(16.Dp()))
                            .OnClick(() => isVisible.Value = !isVisible.Value)
                    );
                    AnimatedVisibility(
                        visible: isVisible.Value,
                        content: () => Text("Text", fontSize: 64.Sp()),
                        modifier: Modifier
                            .Background(Color.lightBlue.ToSystemColor())
                            .AnimateContentSize()
                    );
                }
            );
        }

        [Composable]
        public static void AnimatedVisibility(
            bool visible,
            [Composable] Action content,
            Optional<IEnterTransition> enter = default,
            Optional<IExitTransition> exit = default,
            IModifier? modifier = null
        )
        {
            var resolvedEnter = enter.GetOrDefault(FadeIn(animationSpec: Tween()));
            var resolvedExit = exit.GetOrDefault(FadeOut(animationSpec: Tween()));
            var duration = Math.Max(resolvedEnter.TotalDuration, resolvedExit.TotalDuration);
            var animationSpec = Tween(duration: duration, easing: LinearEasing);

            var visibilityProgress = AnimateFloatAsState(visible.ToInt(), animationSpec).Value;
            Box(
                modifier: modifier.OrEmpty(),
                content: () =>
                {
                    if (visibilityProgress <= 0f)
                        return;
                    var absoluteProgress = visible ? visibilityProgress : 1 - visibilityProgress;
                    var timeElapsed = duration * absoluteProgress;
                    var wrapperModifier = visible
                        ? Modifier.EnterTransition(resolvedEnter, timeElapsed)
                        : Modifier.ExitTransition(resolvedExit, timeElapsed);
                    Box(
                        modifier: wrapperModifier
                            .FillMaxSize(),
                        content: () =>
                        {
                            ReusableComposeNode(
                                nodeFactory: () => new UnityReusableComposeNode(new TransitionContent()),
                                content: content
                            );
                        }
                    );
                }
            );
        }
    }

    internal class TransitionContent : VisualElement
    {
        public TransitionContent()
        {
            RegisterCallback<GeometryChangedEvent>(_ =>
            {
                style.position = Position.Absolute;
                parent.style.width = resolvedStyle.width;
                parent.style.height = resolvedStyle.height;
            });
        }
    }
}