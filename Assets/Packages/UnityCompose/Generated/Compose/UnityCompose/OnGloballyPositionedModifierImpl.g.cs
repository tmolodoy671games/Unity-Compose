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
    [Composable]
    private void __Apply(VisualElement element)
    {
        var __element = (element);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1249381947);
        if (__composer.ShouldExecute(__element))
        {
            var previousLayoutCoordinates = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
            Action<GeometryChangedEvent> onGeometryChanged = !__composer.Changed((this, element, previousLayoutCoordinates)) ? __composer.RememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>() : __composer.UpdateRememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(_ =>
            {
                var newLayoutCoordinates = LayoutCoordinates.Create(element);
                if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
                {
                    previousLayoutCoordinates.Value = newLayoutCoordinates;
                    _onGloballyPositioned(newLayoutCoordinates);
                }
            });
            DisposableEffect(key: element, effect: !__composer.Changed((element, onGeometryChanged)) ? __composer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
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
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1249381947)?.UpdateScope(() => __Apply(__element));
    }
}

internal static partial class GloballyPositionedComposeFunctions
{
    [Composable]
    private static void __FireOnGloballyPositionedCallback(VisualElement element)
    {
        var __element = (element);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(752919397);
        if (__composer.ShouldExecute(__element))
        {
            var callback = element.OnGloballyPositionedCallbackOrNull();
            if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
            {
                __composer.EndRestartGroup(752919397)?.UpdateScope(() => __FireOnGloballyPositionedCallback(__element));
                return;
            }

            var style = element.style;
            var lastTranslate = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>(IMutableStableProperty.Create(style.translate));
            var lastScale = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>(IMutableStableProperty.Create(style.scale));
            LaunchedEffect((style.translate, style.scale), !__composer.Changed((callback, style, lastTranslate, lastScale)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                if (lastTranslate.Value != style.translate || lastScale.Value != style.scale)
                {
                    lastTranslate.Value = style.translate;
                    lastScale.Value = style.scale;
                    callback.ReInvoke();
                }
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(752919397)?.UpdateScope(() => __FireOnGloballyPositionedCallback(__element));
    }
}