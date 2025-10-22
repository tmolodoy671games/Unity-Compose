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
    private static void __ReusableComposeView<T>(ComposeStyle? style = null, Action<T>? initializer = null, [Composable] Action? content = null)
        where T : VisualElement, new()
    {
        if (CurrentComposer.BeginComposeGroup((style, initializer, content)))
            return;
        try
        {
            var resolvedStyle = style;
            var localStyle = LocalStyle.Current;
            if (localStyle.Before != null)
                resolvedStyle = localStyle.Before.Then(resolvedStyle.OrEmpty());
            if (localStyle.After != null)
                resolvedStyle = resolvedStyle.OrEmpty().Then(localStyle.After);
            var visualElement = CurrentComposer.GetOrCreateVisualElement<T>();
            var currentStyle = Remember(() => IMutableStableProperty.Create<ComposeStyle?>(null));
            var currentProperties = Remember(() => IMutableStableProperty.Create<IStableSet<ComposeModifiedProperty>>(IImmutableStableSet.Empty<ComposeModifiedProperty>()));
            // if (!Equals(currentStyle.Value, resolvedStyle))
            // {
            var newProperties = IMutableStableSet.Create<ComposeModifiedProperty>();
            resolvedStyle?.Apply(newProperties);
            var propertiesToRevert = currentProperties.Value.Where(Remember<global::System.Func<global::UnityCompose.ComposeModifiedProperty, bool>>(newProperties, it => !newProperties.Contains(it)));
            foreach (var property in propertiesToRevert)
                property.Revert(visualElement);
            currentProperties.Value = newProperties;
            currentStyle.Value = resolvedStyle;
            visualElement.ClearCallbacks();
            visualElement.style.transitionDelay.value?.Clear();
            visualElement.style.transitionDuration.value?.Clear();
            visualElement.style.transitionProperty.value?.Clear();
            visualElement.style.transitionTimingFunction.value?.Clear();
            visualElement.pickingMode = PickingMode.Ignore;
            resolvedStyle?.Apply(visualElement);
            // }
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
                CompositionLocalProvider(provides: IImmutableStableList.Create(LocalStyle.Provides((null, null)), LocalVisualElement.Provides(visualElement)), content: content);
            }
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __ReusableComposeView<T>(style, initializer, content));
        }
    }

    [Composable]
    [Compiled]
    private static void __Column([Composable] Action content, ComposeStyle? style = null, Align alignHorizontally = Align.FlexStart, Justify alignVertically = Justify.FlexStart)
    {
        if (CurrentComposer.BeginComposeGroup((content, style, alignHorizontally, alignVertically)))
            return;
        try
        {
            ReusableComposeView<Column>(style: style, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>>((alignHorizontally, alignVertically), it =>
            {
                StyleEnum<Align> alignHorizontallyEnum = alignHorizontally;
                StyleEnum<Justify> alignVerticallyEnum = alignVertically;
                it.style.alignItems = alignHorizontallyEnum;
                it.style.justifyContent = alignVerticallyEnum;
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Column(content, style, alignHorizontally, alignVertically));
        }
    }

    [Composable]
    [Compiled]
    private static void __Row([Composable] Action content, ComposeStyle? style = null, Justify alignHorizontally = Justify.FlexStart, Align alignVertically = Align.FlexStart)
    {
        if (CurrentComposer.BeginComposeGroup((content, style, alignHorizontally, alignVertically)))
            return;
        try
        {
            ReusableComposeView<Row>(style: style, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>>((alignHorizontally, alignVertically), it =>
            {
                StyleEnum<Justify> alignHorizontallyEnum = alignHorizontally;
                StyleEnum<Align> alignVerticallyEnum = alignVertically;
                it.style.alignItems = alignVerticallyEnum;
                it.style.justifyContent = alignHorizontallyEnum;
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Row(content, style, alignHorizontally, alignVertically));
        }
    }

    [Composable]
    [Compiled]
    private static void __Box([Composable] Action content, ComposeStyle? style = null, Align alignHorizontally = Align.FlexStart, Justify alignVertically = Justify.FlexStart)
    {
        if (CurrentComposer.BeginComposeGroup((content, style, alignHorizontally, alignVertically)))
            return;
        try
        {
            ReusableComposeView<Box>(style: style, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>>((alignHorizontally, alignVertically), it =>
            {
                StyleEnum<Align> alignHorizontallyEnum = alignHorizontally;
                StyleEnum<Justify> alignVerticallyEnum = alignVertically;
                it.style.alignItems = alignHorizontallyEnum;
                it.style.justifyContent = alignVerticallyEnum;
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Box(content, style, alignHorizontally, alignVertically));
        }
    }

    [Composable]
    [Compiled]
    private static void __Spacer(ComposeStyle style)
    {
        if (CurrentComposer.BeginComposeGroup((style)))
            return;
        try
        {
            ReusableComposeView<Spacer>(style: style);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Spacer(style));
        }
    }

    [Composable]
    [Compiled]
    private static void __Label(string text, Optional<TextStyle> textStyle = default, Optional<float> fontSize = default, Optional<FontStyle> fontStyle = default, Optional<Color> textColor = default, WhiteSpace whiteSpace = WhiteSpace.Normal, TextAnchor align = TextAnchor.UpperLeft, ComposeStyle? style = null)
    {
        if (CurrentComposer.BeginComposeGroup((text, textStyle, fontSize, fontStyle, textColor, whiteSpace, align, style)))
            return;
        try
        {
            ReusableComposeView<Label>(style: style, initializer: Remember<global::System.Action<global::UnityEngine.UIElements.Label>>((text, textStyle, fontSize, fontStyle, textColor, whiteSpace, align), it =>
            {
                it.text = text;
                it.style.whiteSpace = whiteSpace;
                it.style.unityFontStyleAndWeight = fontStyle.GetOrDefault(textStyle.HasValue ? textStyle.Value.FontStyle : FontStyle.Normal);
                it.style.unityTextAlign = align;
                it.style.fontSize = fontSize.GetOrDefault(textStyle.HasValue ? textStyle.Value.FontSize : 14f);
                it.style.color = textColor.GetOrDefault(textStyle.HasValue ? textStyle.Value.Color : Color.white);
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Label(text, textStyle, fontSize, fontStyle, textColor, whiteSpace, align, style));
        }
    }

    [Composable]
    [Compiled]
    private static void __Image(Background image, Color? tint = null, ComposeStyle? style = null)
    {
        if (CurrentComposer.BeginComposeGroup((image, tint, style)))
            return;
        try
        {
            ReusableComposeView<Image>(initializer: Remember<global::System.Action<global::UnityEngine.UIElements.Image>>((image, tint), it =>
            {
                it.sprite = image.sprite;
                it.image = image.renderTexture as Texture ?? image.texture;
                it.tintColor = tint ?? Color.white;
            }), style: style);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Image(image, tint, style));
        }
    }
}