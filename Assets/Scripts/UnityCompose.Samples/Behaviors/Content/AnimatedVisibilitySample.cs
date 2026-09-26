using System;
using Compose.Net;
using SharpExtensions;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class AnimatedVisibilitySample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
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
            var resolvedEnter = enter.GetOrDefault(IEnterTransition.None);
            var resolvedExit = exit.GetOrDefault(IExitTransition.None);
            var duration = Math.Max(resolvedEnter.TotalDuration, resolvedExit.TotalDuration);
            var animationSpec = Tween(duration: duration, easing: LinearEasing);

            var startTime = Remember(() => MutableStateOf(DateTime.Now));
            var visibilityProgress = AnimateFloatAsState(visible.ToInt(), animationSpec).Value;
            SideEffect(visible, () => startTime.Value = DateTime.Now);
            Box(
                modifier: modifier.OrEmpty(),
                content: () =>
                {
                    if (visibilityProgress <= 0f)
                        return;
                    var timeElapsed = DateTime.Now - startTime.Value;
                    Box(
                        modifier: Modifier,
                        content: content
                    );
                }
            );
        }
    }
}