using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeElementIndex
{
    public readonly VisualElement Element;
    public int Index;

    public ComposeElementIndex(VisualElement visualElement)
    {
        Element = visualElement;
    }

    public ComposeElementIndex(VisualElement visualElement, int index)
    {
        Element = visualElement;
        Index = index;
    }

    public override string ToString()
    {
        return $"(Element: {Element.Format()}, Index: {Index})";
    }
}