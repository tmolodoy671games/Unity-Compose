// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier OnGloballyPositioned(
        this IModifier modifier,
        Action<LayoutCoordinates> onGloballyPositioned
    ) => modifier.Composed(() => OnGloballyPositionedImpl(onGloballyPositioned));

    [Composable]
    [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
    private static IModifier OnGloballyPositionedImpl(
        Action<LayoutCoordinates> onGloballyPositioned
    )
    {
        if (true)
        {
            var previousCoordinates =
                Remember(() => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
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

            LaunchedEffect(onGloballyPositioned, () => EveryFrameCoroutine());
        }

        return Modifier;
    }

    [Composable]
    [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
    private static IModifier OnGloballyPositionedImplDeprecated(
        Action<LayoutCoordinates> onGloballyPositioned
    )
    {
        if (true)
        {
            var element = CurrentComposer.GetParentVisualElement().NotNull();
            var previousLayoutCoordinates =
                Remember(static () => IMutableStableProperty.Create(Optional.Empty<LayoutCoordinates>()));
            Action<GeometryChangedEvent> onGeometryChanged = _ =>
            {
                var newLayoutCoordinates = LayoutCoordinates.Create(element);
                if (!previousLayoutCoordinates.Value.Equals(newLayoutCoordinates))
                {
                    previousLayoutCoordinates.Value = newLayoutCoordinates;
                    onGloballyPositioned(newLayoutCoordinates);
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

        return Modifier;
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

    internal static ComposeCallback<GeometryChangedEvent>? OnGloballyPositionedCallbackOrNull(
        this VisualElement element
    )
    {
        var userData = element.UserData();
        if (userData.TryGet("__OnGloballyPositioned", out var cached) &&
            cached is ComposeCallback<GeometryChangedEvent> onGloballyPositioned)
            return onGloballyPositioned;
        return null;
    }
}