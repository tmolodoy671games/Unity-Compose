using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
internal partial class OnGloballyPositionedModifierImpl
{
    [Composable, DontGenerateComposeGroups]
    private void __Apply(VisualElement element)
    {
        var previousLayoutCoordinates = Remember(static () => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
        Action<GeometryChangedEvent> onGeometryChanged = CurrentComposer.WithState((this, element, previousLayoutCoordinates)).Remember<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(__ => _ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                _onGloballyPositioned(newLayoutCoordinates);
            }
        });
        DisposableEffect(key: element, effect: CurrentComposer.WithState((element, onGeometryChanged)).Remember<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(__ => it =>
        {
            var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
            return it.OnDispose(CurrentComposer.WithState((onGeometryChanged, ancestors)).Remember<System.Action>(__ => () =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
            }));
        }));
    }
}

internal static partial class GloballyPositionedComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    private static void __FireOnGloballyPositionedCallback(VisualElement element)
    {
        var callback = element.OnGloballyPositionedCallbackOrNull();
        if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
            return;
        var style = element.style;
        var lastTranslate = Remember(() => IMutableStableProperty.Create(style.translate));
        var lastScale = Remember(() => IMutableStableProperty.Create(style.scale));
        LaunchedEffect((style.translate, style.scale), CurrentComposer.WithState((callback, style, lastTranslate, lastScale)).Remember<System.Action>(__ => () =>
        {
            if (lastTranslate.Value != style.translate || lastScale.Value != style.scale)
            {
                lastTranslate.Value = style.translate;
                lastScale.Value = style.scale;
                callback.ReInvoke();
            }
        }));
    }
}