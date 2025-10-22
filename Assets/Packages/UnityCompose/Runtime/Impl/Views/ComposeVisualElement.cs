// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public abstract class ComposeVisualElement : VisualElement
{
    public ComposeVisualElement()
    {
        style.overflow = Overflow.Hidden;
        pickingMode = PickingMode.Ignore;
    }
}