using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.GloballyPositionedComposeFunctions;
using Box = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box;
using Column = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static void ReusableComposeView<T>(
        IModifier? modifier = null,
        Action<T>? initializer = null,
        ComposableContent? content = null
    ) where T : VisualElement, new()
    {
        CurrentComposer.StartReusableGroup(123);
        var visualElement = CurrentComposer.GetOrCreateVisualElement<T>();
        var parent = LocalVisualElement.Current;
        var index = CurrentComposer.GetElementIndex();
        DisposableEffect((visualElement, parent, index), it =>
        {
            parent.FastReinsert(index, visualElement);
            return it.OnDispose(() => parent.Remove(visualElement));
        });
        CurrentComposer.EnterVisualElement();

        var resolvedModifier = modifier;
        var localStyle = LocalModifier.Current;
        if (localStyle.Before != null)
            resolvedModifier = localStyle.Before.Then(resolvedModifier.OrEmpty());
        if (localStyle.After != null)
            resolvedModifier = resolvedModifier.OrEmpty().Then(localStyle.After);

        var currentProperties = Remember(() => IMutableStableSet.Create<ComposeModifiedProperty>());
        var newProperties = Remember(() => IMutableStableSet.Create<ComposeModifiedProperty>());
        resolvedModifier?.Apply(newProperties);
        foreach (var property in currentProperties)
        {
            if (newProperties.Contains(property))
                continue;
            property.Revert(visualElement);
        }

        currentProperties.Clear();
        if (newProperties.IsNotEmpty())
            currentProperties.AddRange(newProperties);
        newProperties.Clear();

        visualElement.ClearCallbacks();
        visualElement.style.transitionDelay.value?.Clear();
        visualElement.style.transitionDuration.value?.Clear();
        visualElement.style.transitionProperty.value?.Clear();
        visualElement.style.transitionTimingFunction.value?.Clear();
        visualElement.pickingMode = PickingMode.Ignore;
        visualElement.style.overflow = Overflow.Visible;
        LaunchedEffect(resolvedModifier, () => resolvedModifier?.Apply(visualElement));
        FireOnGloballyPositionedCallback(visualElement);

        if (initializer != null)
            LaunchedEffect(initializer, () => initializer?.Invoke(visualElement));

        if (content != null)
        {
            CompositionLocalProvider(
                LocalModifier.Provides((null, null)),
                LocalVisualElement.Provides(visualElement),
                LocalLayoutMeasurer.Provides(Remember(visualElement, () => new LayoutMeasurerImpl(visualElement))),
                content: content
            );
        }

        CurrentComposer.EndReusableGroup(123);
    }

    [Composable]
    public static void Column(
        ComposableContent content,
        IModifier? modifier = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Column>(
            modifier: modifier,
            initializer: it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            },
            content: content
        );
    }

    [Composable]
    public static void Row(
        ComposableContent content,
        IModifier? modifier = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Row>(
            modifier: modifier,
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            },
            content: content
        );
    }

    [Composable]
    public static void Box(
        ComposableContent content,
        IModifier? modifier = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Box>(
            modifier: modifier,
            initializer: it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            },
            content: content
        );
    }

    [Composable]
    public static void Spacer(
        IModifier modifier
    )
    {
        ReusableComposeView<Spacer>(
            modifier: modifier
        );
    }

    [Composable]
    public static void Text(
        string text,
        Optional<Color> color = default,
        Optional<float> fontSize = default,
        Optional<TextStyle> style = default,
        Optional<FontStyle> fontStyle = default,
        Optional<FontWeight> fontWeight = default,
        bool softWrap = true,
        TextAlign textAlign = TextAlign.UpperLeft,
        IModifier? modifier = null
    )
    {
        var localContentColor = LocalContentColor.Current;
        var localTextStyle = LocalTextStyle.Current;
        ReusableComposeView<Text>(
            modifier: modifier,
            initializer: it =>
            {
                it.text = text;
                it.style.whiteSpace = softWrap ? WhiteSpace.Normal : WhiteSpace.NoWrap;

                // FontStyle
                FontStyle resolvedFontStyle;
                if (fontStyle.HasValue)
                    resolvedFontStyle = fontStyle.Value;
                else if (style.HasValue)
                    resolvedFontStyle = style.Value.FontStyle;
                else if (localTextStyle.HasValue)
                    resolvedFontStyle = localTextStyle.Value.FontStyle;
                else
                    resolvedFontStyle = FontStyle.Normal;

                // FontWeight
                FontWeight resolvedFontWeight;
                if (fontWeight.HasValue)
                    resolvedFontWeight = fontWeight.Value;
                else if (style.HasValue)
                    resolvedFontWeight = style.Value.FontWeight;
                else if (localTextStyle.HasValue)
                    resolvedFontWeight = localTextStyle.Value.FontWeight;
                else
                    resolvedFontWeight = FontWeight.Normal;

                it.style.unityFontStyleAndWeight =
                    FontStyleUtils.ToUnityFontStyle(resolvedFontStyle, resolvedFontWeight);
                it.style.unityTextAlign = textAlign.ToTextAnchor();

                // FontSize
                if (fontSize.HasValue)
                    it.style.fontSize = fontSize.Value;
                else if (style.HasValue)
                    it.style.fontSize = style.Value.FontSize;
                else if (localTextStyle.HasValue)
                    it.style.fontSize = localTextStyle.Value.FontSize;
                else
                    it.style.fontSize = 14f;

                // Color
                if (color.HasValue)
                    it.style.color = color.Value;
                else if (style is { HasValue: true, Value.Color.HasValue: true })
                    it.style.color = style.Value.Color.Value;
                else if (localContentColor.HasValue)
                    it.style.color = localContentColor.Value;
                else if (localTextStyle is { HasValue: true, Value.Color.HasValue: true })
                    it.style.color = localTextStyle.Value.Color.Value;
                else
                    it.style.color = Color.black;
            }
        );
    }

    [Composable]
    public static void Image(
        ComposeImage image,
        Color? tint = null,
        IModifier? modifier = null
    )
    {
        ReusableComposeView<Image>(
            initializer: it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint ?? Color.white;
            },
            modifier: modifier
        );
    }
}