// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
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
    private readonly Action<LayoutCoordinates> _onGloballyPositioned;

    public OnGloballyPositionedModifierImpl(Action<LayoutCoordinates> onGloballyPositioned)
    {
        _onGloballyPositioned = onGloballyPositioned;
    }

    [Composable, DontGenerateComposeGroups]
    public override void Apply(VisualElement element)
    {
        var previousLayoutCoordinates =
            Remember(static () => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
        Action<GeometryChangedEvent> onGeometryChanged = _ =>
        {
            var newLayoutCoordinates = LayoutCoordinates.Create(element);
            if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
            {
                previousLayoutCoordinates.Value = newLayoutCoordinates;
                _onGloballyPositioned(newLayoutCoordinates);
            }
        };
        DisposableEffect(
            key: element,
            effect: it =>
            {
                var ancestors = element.Ancestors(includeSelf: true).ToImmutableStableList();
                foreach (var ancestor in ancestors)
                    ancestor.OnGloballyPositionedCallback().Add(onGeometryChanged);
                return it.OnDispose(() =>
                {
                    foreach (var ancestor in ancestors)
                        ancestor.OnGloballyPositionedCallback().Remove(onGeometryChanged);
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
        return _onGloballyPositioned == other._onGloballyPositioned;
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

public static partial class VisualElementExtensions
{
    internal static ComposeCallback<GeometryChangedEvent> OnGloballyPositionedCallback(this VisualElement element)
    {
        var userData = element.UserData();
        if (userData.TryGet("__OnGloballyPositioned", out var cached) &&
            cached is ComposeCallback<GeometryChangedEvent> onGloballyPositioned)
            return onGloballyPositioned;
        var newCallback = new ComposeCallback<GeometryChangedEvent>();
        userData["__OnGloballyPositioned"] = newCallback;
        element.RegisterCallback(newCallback.Callback);
        return newCallback;
    }
    
    internal static ComposeCallback<GeometryChangedEvent>? OnGloballyPositionedCallbackOrNull(this VisualElement element)
    {
        var userData = element.UserData();
        if (userData.TryGet("__OnGloballyPositioned", out var cached) &&
            cached is ComposeCallback<GeometryChangedEvent> onGloballyPositioned)
            return onGloballyPositioned;
        return null;
    }
}

internal static partial class GloballyPositionedComposeFunctions
{
    [Composable, DontGenerateComposeGroups]
    public static void FireOnGloballyPositionedCallback(VisualElement element)
    {
        var callback = element.OnGloballyPositionedCallbackOrNull();
        if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
            return;
        var style = element.style;
        var lastTranslate = Remember(() => IMutableStableProperty.Create(style.translate));
        var lastScale = Remember(() => IMutableStableProperty.Create(style.scale));
        LaunchedEffect((style.translate, style.scale), () =>
        {
            if (lastTranslate.Value != style.translate || lastScale.Value != style.scale)
            {
                lastTranslate.Value = style.translate;
                lastScale.Value = style.scale;
                callback.ReInvoke();
            }
        });
    }
}