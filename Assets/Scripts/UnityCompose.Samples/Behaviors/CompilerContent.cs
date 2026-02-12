// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class CompilerContent : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            Foo(
                text: "bla".ToString(),
                modifier: @"Modifier
                    .OnMouseLeave(() => { })".ToString()
            );
        }

        [Composable]
        private static void Foo(
            string text,
            int misc1 = -1,
            string? modifier = null
        )
        {
        }
    }
}