// ReSharper disable ArrangeNamespaceBody

using System;
using Compose.Net;

namespace UnityCompose.Samples.Behaviors.Content
{
    internal partial class AnimatedContentSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
        }

        [Composable]
        private static void Layout()
        {
        }

        [Composable]
        public static void AnimatedContent<T>(
            T targetState,
            [Composable] Action<T> content,
            Func<AnimatedContentTransitionScope<T>, ContentTransform>? transitionSpec = null,
            IModifier? modifier = null
        )
        {
        }

        public readonly record struct ContentTransform(
            IEnterTransition Enter,
            IExitTransition Exit
        );

        public readonly record struct AnimatedContentTransitionScope<T>(
            T InitialState,
            T TargetState
        );
    }

    internal static class EnterTransitionExtensions
    {
        public static AnimatedContentSample.ContentTransform TogetherWith(
            this IEnterTransition enter,
            IExitTransition exit
        )
        {
            return new AnimatedContentSample.ContentTransform(enter, exit);
        }
    }
}