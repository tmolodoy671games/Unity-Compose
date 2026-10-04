// ReSharper disable ArrangeNamespaceBody

using System;
using Compose.Net;
using UnityEngine.UIElements;

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
            ComposableContent<T> content,
            Func<AnimatedContentTransitionScope<T>, ContentTransform>? transitionSpec = null,
            IModifier? modifier = null
        )
        {
            var increment = Remember(() => MutableStateOf(0));
            var lastValues = Remember(() => MutableStableListOf<T>(targetState));
            SideEffect(targetState, () =>
            {
                increment.Value++;
                lastValues.Add(targetState);
            });
            // TODO
            Box(
                modifier: modifier,
                content: () => { }
            );
        }
    }

    internal readonly record struct ValueEntry<T>(
        T Value,
        bool IsEntering,
        bool IsExiting,
        bool IsIdle
    );

    public readonly record struct ContentTransform(
        IEnterTransition Enter,
        IExitTransition Exit
    );

    public readonly record struct AnimatedContentTransitionScope<T>(
        T InitialState,
        T TargetState
    );

    internal static class EnterTransitionExtensions
    {
        public static ContentTransform TogetherWith(
            this IEnterTransition enter,
            IExitTransition exit
        )
        {
            return new ContentTransform(enter, exit);
        }
    }
}