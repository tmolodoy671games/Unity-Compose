using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ModifierExtensions
{
    [Composable]
    private static IModifier __OnGloballyPositionedImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        var __onGloballyPositioned = (onGloballyPositioned);
        var __composer = CurrentComposer;
        __composer.StartReplaceGroup(-282749780);
        var element = CurrentComposer.GetParentVisualElement().NotNull();
        var previousLayoutCoordinates = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
        Action<GeometryChangedEvent> onGeometryChanged = !__composer.ChangedAsStruct((onGloballyPositioned, element, previousLayoutCoordinates)) ? __composer.RememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>() : __composer.UpdateRememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(_ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                onGloballyPositioned(newLayoutCoordinates);
            }
        });
        DisposableEffect(key: element, effect: !__composer.ChangedAsStruct((element, onGeometryChanged)) ? __composer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
        {
            var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
            return it.OnDispose(() =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
            });
        }));
        __composer.EndReplaceGroup(-282749780);
        return Modifier;
        __composer.EndReplaceGroup(-282749780);
        return Modifier;
    }
}