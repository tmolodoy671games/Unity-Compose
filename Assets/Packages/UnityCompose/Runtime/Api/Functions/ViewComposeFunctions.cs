using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
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
        var composer = CurrentComposer;
        composer.StartReusableGroup<T>(123);
        var parent = composer.GetParentVisualElement().NotNull();
        var indexInParent = composer.GetElementIndex();
        var node = composer.GetReusableNode<T>();
        var visualElement = node.VisualElement.NotNull();
        composer.EnterVisualElement(visualElement);

        modifier = modifier?.Compose();
        node.Update(
            parent: parent,
            indexInParent: indexInParent,
            modifier: modifier,
            initializer: initializer
        );

        modifier?.DrawBefore();
        content?.Invoke();
        modifier?.DrawAfter();

        composer.EndReusableGroup(123);
    }

    [Composable]
    public static void Column(
        ComposableContent content,
        IModifier? modifier = null,
        Alignment.Horizontal? horizontalAlignment = null,
        Arrangement.Vertical? verticalArrangement = null
    )
    {
        ReusableComposeView<Column>(
            modifier: modifier,
            initializer: it =>
            {
                it.style.alignItems = (horizontalAlignment ?? Alignment.Left).ToAlign();
                it.style.justifyContent = (verticalArrangement ?? Arrangement.Top).ToJustify();
            },
            content: content
        );
    }

    [Composable]
    public static void Row(
        ComposableContent content,
        IModifier? modifier = null,
        Arrangement.Horizontal? horizontalArrangement = null,
        Alignment.Vertical? verticalAlignment = null
    )
    {
        ReusableComposeView<Row>(
            modifier: modifier,
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
                it.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
            },
            content: content
        );
    }

    [Composable]
    public static void Box(
        ComposableContent content,
        IModifier? modifier = null,
        Alignment? alignment = null
    )
    {
        ReusableComposeView<Box>(
            modifier: modifier,
            initializer: it =>
            {
                var resolvedAlignment = alignment ?? Alignment.TopLeft;
                var (align, justify) = (resolvedAlignment.ToAlign(), resolvedAlignment.ToJustify());
                it.style.alignItems = align;
                it.style.justifyContent = justify;
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
        Optional<Sp> fontSize = default,
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
        var resolvedFontStyle = Remember((fontStyle, style, localTextStyle), () =>
            fontStyle.HasValue
                ? fontStyle.Value
                : style.HasValue
                    ? style.Value.FontStyle
                    : localTextStyle.HasValue
                        ? localTextStyle.Value.FontStyle
                        : FontStyle.Normal
        );
        var resolvedFontWeight = Remember((fontWeight, style, localTextStyle), () =>
            fontWeight.HasValue
                ? fontWeight.Value
                : style.HasValue
                    ? style.Value.FontWeight
                    : localTextStyle.HasValue
                        ? localTextStyle.Value.FontWeight
                        : FontWeight.Normal
        );
        var resolvedFontSize = Remember((fontSize, style, localTextStyle), () =>
            fontSize.HasValue
                ? fontSize.Value
                : style.HasValue
                    ? style.Value.FontSize
                    : localTextStyle.HasValue
                        ? localTextStyle.Value.FontSize
                        : 14.Sp()
        );
        var resolvedFontSizeValue = resolvedFontSize.Resolve();
        var resolvedColor = Remember((color, style, localContentColor, localTextStyle), () =>
            color.HasValue
                ? color.Value
                : style is { HasValue: true, Value.Color.HasValue: true }
                    ? style.Value.Color.Value
                    : localContentColor.HasValue
                        ? localContentColor.Value
                        : localTextStyle is { HasValue: true, Value.Color.HasValue: true }
                            ? localTextStyle.Value.Color.Value
                            : Color.black
        );

        ReusableComposeView<Text>(
            modifier: modifier,
            initializer: it =>
            {
                it.text = text;
                it.style.whiteSpace = softWrap ? WhiteSpace.Normal : WhiteSpace.NoWrap;
                it.style.unityFontStyleAndWeight =
                    FontStyleUtils.ToUnityFontStyle(resolvedFontStyle, resolvedFontWeight);
                it.style.unityTextAlign = textAlign.ToTextAnchor();
                it.style.fontSize = resolvedFontSizeValue;
                it.style.color = resolvedColor;
            }
        );
    }

    [Composable]
    public static void Image(
        ComposeImage image,
        Optional<Color> tint = default,
        IModifier? modifier = null
    )
    {
        ReusableComposeView<Image>(
            initializer: it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint.GetOrDefault(Color.white);
            },
            modifier: modifier
        );
    }
}