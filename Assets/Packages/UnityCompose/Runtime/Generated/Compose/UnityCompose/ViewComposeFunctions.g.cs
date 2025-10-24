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
                CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides((null, null)), LocalVisualElement.Provides(visualElement)), content: content);
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
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, FontStyle fontStyle = FontStyle.Normal, FontWeight fontWeight = FontWeight.Normal, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier)))
            return;
        try
        {
            ReusableComposeView<Text>(modifier: modifier, initializer: Remember<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign), it =>
            {
                it.text = text;
                it.style.whiteSpace = softWrap ? WhiteSpace.Normal : WhiteSpace.NoWrap;
                it.style.unityFontStyleAndWeight = FontStyleUtils.ToUnityFontStyle(fontStyle, fontWeight);
                it.style.unityTextAlign = textAlign.ToTextAnchor();
                it.style.fontSize = fontSize.GetOrDefault(style.HasValue ? style.Value.FontSize : 14f);
                it.style.color = color.GetOrDefault(style.HasValue ? style.Value.Color : Color.white);
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