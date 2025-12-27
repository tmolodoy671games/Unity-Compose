using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

public readonly record struct ElementAnchor(
    VisualElement? Parent,
    int Index
)
{
    public static readonly ElementAnchor None = new(null, -1);

    public bool IsValid => Index >= 0;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"ElementAnchor({Parent?.GetType().Name}, {Index})";
    }
}