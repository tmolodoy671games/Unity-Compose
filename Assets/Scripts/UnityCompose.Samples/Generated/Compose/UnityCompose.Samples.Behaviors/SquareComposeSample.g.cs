using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class SquareComposeSample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
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
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Spacer(Modifier);
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Preview());
            }
        }

        [Composable]
        private static void __EmptyColumn([Composable] Action action)
        {
            var __action = (action);
            if (CurrentComposer.BeginComposeGroup(__action))
                return;
            try
            {
                action();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.WithState(__action).Remember<Action>(__ => () => __EmptyColumn(__)));
            }
        }

        [Composable]
        private static void __EmptySpacer(IModifier modifier)
        {
            var __modifier = (modifier);
            if (CurrentComposer.BeginComposeGroup(__modifier))
                return;
            try
            {
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.WithState(__modifier).Remember<Action>(__ => () => __EmptySpacer(__)));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                // var value1 = Remember(static () => MutableStateOf(false));
                // var value2 = Remember(static () => MutableStateOf(0));
                // var value3 = Remember(static () => MutableStateOf(1.2));
                // var value4 = Remember(static () => MutableStateOf("text"));
                Spacer(Modifier);
            }
            finally
            {
                CurrentComposer.EndComposeGroup(() => __Layout());
            }
        }
    }
}