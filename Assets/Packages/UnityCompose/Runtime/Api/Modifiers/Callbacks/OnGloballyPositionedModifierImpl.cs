// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnGloballyPositioned(
        this IModifier modifier,
        Action<LayoutCoordinates> onGloballyPositioned
    )
    {
        return modifier + new OnGloballyPositionedModifierImpl(onGloballyPositioned);
    }
}

internal partial class OnGloballyPositionedModifierImpl : BaseModifier<OnGloballyPositionedModifierImpl>
{
    private readonly Action<GeometryChangedEvent> _onGeometryChanged;

    public OnGloballyPositionedModifierImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        var previousLayoutCoordinates = Optional.Empty<LayoutCoordinates>();
        _onGeometryChanged = it =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(it.VisualElement());
            if (previousLayoutCoordinates != newLayoutCoordinates)
            {
                onGloballyPositioned(newLayoutCoordinates);
                previousLayoutCoordinates = newLayoutCoordinates;
            }
        };
    }

    [Composable, DontGenerateComposeGroups]
    public override void Apply(VisualElement element)
    {
        DisposableEffect(
            key: element,
            effect: it =>
            {
                var ancestors = element.Ancestors().ToImmutableStableList();
                foreach (var ancestor in ancestors)
                    ancestor.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged);
                return it.OnDispose(() =>
                {
                    foreach (var ancestor in ancestors)
                        ancestor.GetComposeCallback<GeometryChangedEvent>().Remove(_onGeometryChanged);
                });
            }
        );
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
    }

    public override void Revert(VisualElement element)
    {
    }

    protected override bool Equals(OnGloballyPositionedModifierImpl other)
    {
        return _onGeometryChanged == other._onGeometryChanged;
    }
}

public static partial class VisualElementExtensions
{
    public static IEnumerable<VisualElement> Ancestors(this VisualElement element, bool includeSelf = false)
    {
        if (includeSelf)
            yield return element;
        var parent = element.parent;
        while (parent != null)
        {
            yield return parent;
            parent = parent.parent;
        }
    }
}