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
        __composer.StartRestartGroup(1473523162);
        if (__composer.ShouldExecute(__element))
        {
            var previousLayoutCoordinates = !__composer.RememberedKeyChanged<bool>(1016421644, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
            Action<GeometryChangedEvent> onGeometryChanged = !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.OnGloballyPositionedModifierImpl, UnityEngine.UIElements.VisualElement, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>?>>(-2103811659, (this, element, previousLayoutCoordinates)) ? CurrentComposer.RememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>() : CurrentComposer.UpdateLambda<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(_ =>
            {
                var newLayoutCoordinates = LayoutCoordinates.Create(element);
                if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
                {
                    previousLayoutCoordinates.Value = newLayoutCoordinates;
                    _onGloballyPositioned(newLayoutCoordinates);
                }
            });
            DisposableEffect(key: element, effect: !__composer.RememberedKeyChanged<ValueTuple<UnityEngine.UIElements.VisualElement, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>>(-2126079137, (element, onGeometryChanged)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
                return it.OnDispose(!__composer.RememberedKeyChanged<ValueTuple<System.Action<UnityEngine.UIElements.GeometryChangedEvent>, StableCollections.IImmutableStableList<UnityEngine.UIElements.VisualElement>?>>(-1215541753, (onGeometryChanged, ancestors)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                {
                    foreach (var ancestor in ancestors)
                        ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
                }));
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1473523162)?.UpdateScope(() => __Apply(__element));
    }
}

internal static partial class GloballyPositionedComposeFunctions
{
    [Composable]
    private static void __FireOnGloballyPositionedCallback(VisualElement element)
    {
        var __element = (element);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-429797837);
        if (__composer.ShouldExecute(__element))
        {
            var callback = element.OnGloballyPositionedCallbackOrNull();
            if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
            {
                __composer.EndRestartGroup(-429797837)?.UpdateScope(() => __FireOnGloballyPositionedCallback(__element));
                return;
            }

            var style = element.style;
            var lastTranslate = !__composer.RememberedKeyChanged<bool>(-2068886363, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>(IMutableStableProperty.Create(style.translate));
            var lastScale = !__composer.RememberedKeyChanged<bool>(570818614, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>(IMutableStableProperty.Create(style.scale));
            LaunchedEffect((style.translate, style.scale), !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposeCallback<UnityEngine.UIElements.GeometryChangedEvent>?, UnityEngine.UIElements.IStyle?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>?>>(-1527717317, (callback, style, lastTranslate, lastScale)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
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

        __composer.EndRestartGroup(-429797837)?.UpdateScope(() => __FireOnGloballyPositionedCallback(__element));
    }
}