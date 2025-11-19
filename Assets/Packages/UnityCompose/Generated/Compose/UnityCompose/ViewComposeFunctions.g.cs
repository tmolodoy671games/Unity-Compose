using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.GloballyPositionedComposeFunctions;
using Box = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box;
using Column = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __ReusableComposeView<T>(IModifier? modifier = null, Action<T>? initializer = null, [Composable] Action? content = null)
        where T : VisualElement, new()
    {
        var(__modifier, __initializer, __content) = (modifier, initializer, content);
        if (CurrentComposer.BeginComposeGroup((__modifier, __initializer, __content)))
            return;
        try
        {
            PerformanceMetrics.MeasureReusableComposeView(CurrentComposer.WithState((modifier, initializer, content)).Remember<System.Action>(__ => () =>
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
                var propertiesToRevert = currentProperties.Value.Where(CurrentComposer.WithState(newProperties).Remember<System.Func<UnityCompose.ComposeModifiedProperty, bool>>(__ => it => !newProperties.Contains(it)));
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
                FireOnGloballyPositionedCallback(visualElement);
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
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__modifier, __initializer, __content)).Remember<Action>(__ => () => __ReusableComposeView(__.__modifier, __.__initializer, __.__content)));
        }
    }

    [Composable]
    private static void __Column([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: CurrentComposer.WithState((horizontalAlignment, verticalAlignment)).Remember<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(__ => it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__content, __modifier, __horizontalAlignment, __verticalAlignment)).Remember<Action>(__ => () => __Column(__.__content, __.__modifier, __.__horizontalAlignment, __.__verticalAlignment)));
        }
    }

    [Composable]
    private static void __Row([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: CurrentComposer.WithState((horizontalAlignment, verticalAlignment)).Remember<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(__ => it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__content, __modifier, __horizontalAlignment, __verticalAlignment)).Remember<Action>(__ => () => __Row(__.__content, __.__modifier, __.__horizontalAlignment, __.__verticalAlignment)));
        }
    }

    [Composable]
    private static void __Box([Composable] Action content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: CurrentComposer.WithState((horizontalAlignment, verticalAlignment)).Remember<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(__ => it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__content, __modifier, __horizontalAlignment, __verticalAlignment)).Remember<Action>(__ => () => __Box(__.__content, __.__modifier, __.__horizontalAlignment, __.__verticalAlignment)));
        }
    }

    [Composable]
    private static void __Spacer(IModifier modifier)
    {
        var __modifier = (modifier);
        if (CurrentComposer.BeginComposeGroup(__modifier))
            return;
        try
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState(__modifier).Remember<Action>(__ => () => __Spacer(__)));
        }
    }

    [Composable]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        if (CurrentComposer.BeginComposeGroup((__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)))
            return;
        try
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            ReusableComposeView<Text>(modifier: modifier, initializer: CurrentComposer.WithState((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, localContentColor, localTextStyle)).Remember<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>(__ => it =>
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
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)).Remember<Action>(__ => () => __Text(__.__text, __.__color, __.__fontSize, __.__style, __.__fontStyle, __.__fontWeight, __.__softWrap, __.__textAlign, __.__modifier)));
        }
    }

    [Composable]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        if (CurrentComposer.BeginComposeGroup((__image, __tint, __modifier)))
            return;
        try
        {
            ReusableComposeView<Image>(initializer: CurrentComposer.WithState((image, tint)).Remember<System.Action<UnityEngine.UIElements.Image>?>(__ => it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint ?? Color.white;
            }), modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__image, __tint, __modifier)).Remember<Action>(__ => () => __Image(__.__image, __.__tint, __.__modifier)));
        }
    }
}