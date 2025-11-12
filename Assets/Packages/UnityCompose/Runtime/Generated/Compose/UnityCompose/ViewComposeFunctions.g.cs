using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using Box = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box;
using Column = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __ReusableComposeView<T>(IModifier? modifier = null, Action<T>? initializer = null, [Composable] Action? content = null)
        where T : VisualElement, new()
    {
        if (CurrentComposer.BeginComposeGroup((modifier, initializer, content)))
            return;
        try
        {
            var resolvedModifier = modifier;
            var localStyle = LocalModifier.Current;
            if (localStyle.Before != null)
                resolvedModifier = localStyle.Before.Then(resolvedModifier.OrEmpty());
            if (localStyle.After != null)
                resolvedModifier = resolvedModifier.OrEmpty().Then(localStyle.After);
            var visualElement = CurrentComposer.GetOrCreateVisualElement<T>();
            var currentModifier = Remember(() => IMutableStableProperty.Create<IModifier?>(null));
            var currentProperties = Remember(() => IMutableStableProperty.Create<IStableSet<ComposeModifiedProperty>>(IImmutableStableSet.Empty<ComposeModifiedProperty>()));
            var newProperties = IMutableStableSet.Create<ComposeModifiedProperty>();
            resolvedModifier?.Apply(newProperties);
            var propertiesToRevert = currentProperties.Value.Where(Remember<global::System.Func<global::UnityCompose.ComposeModifiedProperty, bool>>(newProperties, it => !newProperties.Contains(it)));
            foreach (var property in propertiesToRevert)
                property.Revert(visualElement);
            currentProperties.Value = newProperties;
            currentModifier.Value = resolvedModifier;
            visualElement.ClearCallbacks();
            visualElement.style.transitionDelay.value?.Clear();
            visualElement.style.transitionDuration.value?.Clear();
            visualElement.style.transitionProperty.value?.Clear();
            visualElement.style.transitionTimingFunction.value?.Clear();
            visualElement.pickingMode = PickingMode.Ignore;
            visualElement.style.overflow = Overflow.Visible;
            resolvedModifier?.Apply(visualElement);
            if (initializer != null)
            {
                var currentInitializer = Remember(() => IMutableStableProperty.Create<Action<T>?>(null));
                if (currentInitializer.Value != initializer)
                {
                    currentInitializer.Value = initializer;
                    initializer(visualElement);
                }
            }

            if (content != null)
            {
                CompositionLocalProvider(LocalModifier.Provides((null, null)), LocalVisualElement.Provides(visualElement), LocalLayoutMeasurer.Provides(Remember(visualElement, () => new LayoutMeasurerImpl(visualElement))), content: content);
            }
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __ReusableComposeView<T>(modifier, initializer, content));
        }
    }

    [Composable]
    [Compiled]
    private static void __Column([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        if (CurrentComposer.BeginComposeGroup((content, modifier, horizontalAlignment, verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>>((horizontalAlignment, verticalAlignment), it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Column(content, modifier, horizontalAlignment, verticalAlignment));
        }
    }

    [Composable]
    [Compiled]
    private static void __Row([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        if (CurrentComposer.BeginComposeGroup((content, modifier, horizontalAlignment, verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>>((horizontalAlignment, verticalAlignment), it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Row(content, modifier, horizontalAlignment, verticalAlignment));
        }
    }

    [Composable]
    [Compiled]
    private static void __Box([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        if (CurrentComposer.BeginComposeGroup((content, modifier, horizontalAlignment, verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>>((horizontalAlignment, verticalAlignment), it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Box(content, modifier, horizontalAlignment, verticalAlignment));
        }
    }

    [Composable]
    [Compiled]
    private static void __Spacer(IModifier modifier)
    {
        if (CurrentComposer.BeginComposeGroup((modifier)))
            return;
        try
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Spacer(modifier));
        }
    }

    [Composable]
    [Compiled]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier)))
            return;
        try
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            ReusableComposeView<Text>(modifier: modifier, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, localContentColor, localTextStyle), it =>
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
                it.style.unityFontStyleAndWeight = FontStyleUtils.ToUnityFontStyle(resolvedFontStyle, resolvedFontWeight);
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
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Text(text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier));
        }
    }

    [Composable]
    [Compiled]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((image, tint, modifier)))
            return;
        try
        {
            ReusableComposeView<Image>(initializer: Remember<global::System.Action<global::UnityEngine.UIElements.Image>>((image, tint), it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint ?? Color.white;
            }), modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Image(image, tint, modifier));
        }
    }
}