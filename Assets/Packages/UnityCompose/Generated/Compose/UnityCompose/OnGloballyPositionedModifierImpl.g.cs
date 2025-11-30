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
        var previousLayoutCoordinates = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(1649382438, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(static () => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
        Action<GeometryChangedEvent> onGeometryChanged = CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.OnGloballyPositionedModifierImpl, UnityEngine.UIElements.VisualElement, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>?>, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(1166132151, (this, element, previousLayoutCoordinates)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.OnGloballyPositionedModifierImpl, UnityEngine.UIElements.VisualElement, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>?>, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.OnGloballyPositionedModifierImpl, UnityEngine.UIElements.VisualElement, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>?>, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(_ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                _onGloballyPositioned(newLayoutCoordinates);
            }
        });
        DisposableEffect(key: element, effect: CurrentComposer.HasRememberedValue<ValueTuple<UnityEngine.UIElements.VisualElement, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(614040719, (element, onGeometryChanged)) ? CurrentComposer.RememberedValue<ValueTuple<UnityEngine.UIElements.VisualElement, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.WriteLambda<ValueTuple<UnityEngine.UIElements.VisualElement, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
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
        var lastTranslate = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>(-236844224, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>(() => IMutableStableProperty.Create(style.translate));
        var lastScale = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>(1145949574, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>(() => IMutableStableProperty.Create(style.scale));
        LaunchedEffect((style.translate, style.scale), CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.ComposeCallback<UnityEngine.UIElements.GeometryChangedEvent>?, UnityEngine.UIElements.IStyle?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>?>, System.Action>(358492789, (callback, style, lastTranslate, lastScale)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.ComposeCallback<UnityEngine.UIElements.GeometryChangedEvent>?, UnityEngine.UIElements.IStyle?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>?>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.ComposeCallback<UnityEngine.UIElements.GeometryChangedEvent>?, UnityEngine.UIElements.IStyle?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>?>, System.Action>(() =>
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