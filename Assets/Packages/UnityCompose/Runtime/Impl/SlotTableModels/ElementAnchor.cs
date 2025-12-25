using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

public readonly record struct ElementAnchor(
    VisualElement? Parent,
    int Index
);