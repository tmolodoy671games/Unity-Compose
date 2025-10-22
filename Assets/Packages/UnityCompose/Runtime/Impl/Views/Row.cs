using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

internal class Row : ComposeVisualElement
{
    public Row()
    {
        style.flexDirection = FlexDirection.Row;
    }
}