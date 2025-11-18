using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeElementIndex
{
    public readonly VisualElement Element;
    public int CurrentIndex;

    public ComposeElementIndex(VisualElement visualElement)
    {
        Element = visualElement;
    }

    public ComposeElementIndex(VisualElement visualElement, int currentIndex)
    {
        Element = visualElement;
        CurrentIndex = currentIndex;
    }

    public override string ToString()
    {
        return $"(Element: {Element.Format()}, Index: {CurrentIndex})";
    }
}