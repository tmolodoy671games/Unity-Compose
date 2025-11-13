// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
internal partial class OnGloballyPositionedModifierImpl
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private void __Apply(VisualElement element)
    {
        var previousLayoutCoordinates = Remember(static () => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
        Action<GeometryChangedEvent> onGeometryChanged = Remember<global::System.Action<global::UnityEngine.UIElements.GeometryChangedEvent>>((this, element, previousLayoutCoordinates), _ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                _onGloballyPositioned(newLayoutCoordinates);
            }
        });
        DisposableEffect(key: element, effect: Remember<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::System.IDisposable>>((element, onGeometryChanged), it =>
        {
            var ancestors = element.Ancestors().ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
            return it.OnDispose(() =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
            });
        }));
    }
}