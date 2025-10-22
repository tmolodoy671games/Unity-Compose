using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

internal class Column : ComposeVisualElement
{
    public Column()
    {
        style.overflow = Overflow.Hidden;
    }
}