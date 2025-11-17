using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
internal partial class OnGloballyPositionedModifierImpl : BaseModifier<OnGloballyPositionedModifierImpl>
{
    [Composable, DontGenerateComposeGroups]
    public override void __Apply(VisualElement element)
    {
        var previousLayoutCoordinates = Remember(CurrentComposer.WithState((this, element, previousLayoutCoordinates)).Remember<Action>(__ => _ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                _onGloballyPositioned(newLayoutCoordinates);
            }
        }));
        Action<GeometryChangedEvent> onGeometryChanged = CurrentComposer.WithState((element, onGeometryChanged)).Remember<Func>(__ => it =>
        {
            var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
            return it.OnDispose(CurrentComposer.WithState((__.onGeometryChanged, __.ancestors)).Remember<Action>(__ => () =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
            }));
        });
        DisposableEffect(key: element, effect: it =>
        {
            var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
            return it.OnDispose(() =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
            });
        });
    }
}

internal static partial class GloballyPositionedComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    public static void __FireOnGloballyPositionedCallback(VisualElement element)
    {
        var callback = element.OnGloballyPositionedCallbackOrNull();
        if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
            return;
        var style = element.style;
        var lastTranslate = Remember(CurrentComposer.WithState(style).Remember<Func>(__ => () => IMutableStableProperty.Create(style.translate)));
        var lastScale = Remember(CurrentComposer.WithState(style).Remember<Func>(__ => () => IMutableStableProperty.Create(style.scale)));
        LaunchedEffect((style.translate, style.scale), CurrentComposer.WithState((callback, style, lastTranslate, lastScale)).Remember<Action>(__ => () =>
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