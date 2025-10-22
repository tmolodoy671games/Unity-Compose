using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

internal class Box : ComposeVisualElement
{
    public Box()
    {
        style.overflow = Overflow.Hidden;
    }
}