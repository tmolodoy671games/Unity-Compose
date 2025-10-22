using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

internal class AnimatedSize : ComposeVisualElement
{
    public AnimatedSize()
    {
        style.alignItems = Align.Center;
        style.justifyContent = Justify.Center;
    }
}