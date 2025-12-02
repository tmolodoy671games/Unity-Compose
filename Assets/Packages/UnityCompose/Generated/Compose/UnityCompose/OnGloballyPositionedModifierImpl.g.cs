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
            var previousLayoutCoordinates = !__composer.RememberedKeyChanged<bool>(1649382438, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>>(IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
            Action<GeometryChangedEvent> onGeometryChanged = !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.OnGloballyPositionedModifierImpl, UnityEngine.UIElements.VisualElement, StableCollections.IMutableStableProperty<SharpExtensions.Optional<UnityCompose.LayoutCoordinates>>?>>(1166132151, (this, element, previousLayoutCoordinates)) ? CurrentComposer.RememberedValue<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>() : CurrentComposer.UpdateLambda<System.Action<UnityEngine.UIElements.GeometryChangedEvent>>(_ =>
            {
                var newLayoutCoordinates = LayoutCoordinates.Create(element);
                __composer.StartReplaceGroup(-1822472975);
                if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
                {
                    previousLayoutCoordinates.Value = newLayoutCoordinates;
                    _onGloballyPositioned(newLayoutCoordinates);
                }

                __composer.EndReplaceGroup(-1822472975);
            });
            DisposableEffect(key: element, effect: !__composer.RememberedKeyChanged<ValueTuple<UnityEngine.UIElements.VisualElement, System.Action<UnityEngine.UIElements.GeometryChangedEvent>>>(614040719, (element, onGeometryChanged)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
                __composer.StartReplaceGroup(-1392951807);
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
                __composer.EndReplaceGroup(-1392951807);
                return it.OnDispose(!__composer.RememberedKeyChanged<ValueTuple<System.Action<UnityEngine.UIElements.GeometryChangedEvent>, StableCollections.IImmutableStableList<UnityEngine.UIElements.VisualElement>?>>(-1448001787, (onGeometryChanged, ancestors)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                {
                    __composer.StartReplaceGroup(149727591);
                    foreach (var ancestor in ancestors)
                        ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
                    __composer.EndReplaceGroup(149727591);
                }));
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
            var lastTranslate = !__composer.RememberedKeyChanged<bool>(-236844224, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>>(IMutableStableProperty.Create(style.translate));
            var lastScale = !__composer.RememberedKeyChanged<bool>(1145949574, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>>(IMutableStableProperty.Create(style.scale));
            LaunchedEffect((style.translate, style.scale), !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposeCallback<UnityEngine.UIElements.GeometryChangedEvent>?, UnityEngine.UIElements.IStyle?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleTranslate>?, StableCollections.IMutableStableProperty<UnityEngine.UIElements.StyleScale>?>>(358492789, (callback, style, lastTranslate, lastScale)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
            {
                __composer.StartReplaceGroup(-1037846502);
                if (lastTranslate.Value != style.translate || lastScale.Value != style.scale)
                {
                    lastTranslate.Value = style.translate;
                    lastScale.Value = style.scale;
                    callback.ReInvoke();
                }

                __composer.EndReplaceGroup(-1037846502);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(752919397)?.UpdateScope(() => __FireOnGloballyPositionedCallback(__element));
    }
}