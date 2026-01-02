using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class ModifierExtensions
{
    [Composable]
    private static IModifier __OnGloballyPositionedImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        var __composer = CurrentComposer;
        __composer.StartReplaceGroup(-1601962195);
        if (true)
        {
            var previousCoordinates = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
            var element = CurrentComposer.GetParentVisualElement().NotNull();
            IEnumerator EveryFrameCoroutine()
            {
                yield return null;
                while (true)
                {
                    var newCoordinates = LayoutCoordinates.Create(element);
                    if (!previousCoordinates.Value.Equals(newCoordinates))
                    {
                        previousCoordinates.Value = newCoordinates;
                        onGloballyPositioned(newCoordinates);
                    }

                    yield return null;
                }
            }

            LaunchedEffect(onGloballyPositioned, !__composer.ChangedAsStruct((previousCoordinates, element)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => EveryFrameCoroutine()));
        }

        __composer.EndReplaceGroup(-1601962195);
        return Modifier;
    }

    [Composable]
    private static IModifier __OnGloballyPositionedImplDeprecated(Action<LayoutCoordinates> onGloballyPositioned)
    {
        var __composer = CurrentComposer;
        __composer.StartReplaceGroup(931312751);
        if (true)
        {
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
        }

        __composer.EndReplaceGroup(931312751);
        return Modifier;
    }
}