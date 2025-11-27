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
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __ReusableComposeView<T>(IModifier? modifier = null, Action<T>? initializer = null, ComposableContent? content = null)
        where T : VisualElement, new()
    {
        var(__modifier, __initializer, __content) = (modifier, initializer, content);
        if (CurrentComposer.BeginComposeGroup(546009290, (__modifier, __initializer, __content)))
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
            var parent = LocalVisualElement.Current;
            var index = CurrentComposer.GetElementIndex();
            DisposableEffect((visualElement, parent, index), CurrentComposer.HasRememberedValue<ValueTuple<T?, UnityEngine.UIElements.VisualElement?, int>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(-1682168451, (visualElement, parent, index)) ? CurrentComposer.RememberedValue<ValueTuple<T?, UnityEngine.UIElements.VisualElement?, int>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.WriteLambda<ValueTuple<T?, UnityEngine.UIElements.VisualElement?, int>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                parent.FastReinsert(index, visualElement);
                return it.OnDispose(() => parent.Remove(visualElement));
            }));
            var currentModifier = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<UnityCompose.IModifier?>>(-1445206198, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<UnityCompose.IModifier?>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<UnityCompose.IModifier?>>(() => IMutableStableProperty.Create<IModifier?>(null));
            var currentProperties = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<StableCollections.IStableSet<UnityCompose.ComposeModifiedProperty>>>(-287396718, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<StableCollections.IStableSet<UnityCompose.ComposeModifiedProperty>>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<StableCollections.IStableSet<UnityCompose.ComposeModifiedProperty>>>(() => IMutableStableProperty.Create<IStableSet<ComposeModifiedProperty>>(IImmutableStableSet.Empty<ComposeModifiedProperty>()));
            var newProperties = IMutableStableSet.Create<ComposeModifiedProperty>();
            resolvedModifier?.Apply(newProperties);
            var propertiesToRevert = currentProperties.Value.Where(CurrentComposer.HasRememberedValue<StableCollections.IMutableStableSet<UnityCompose.ComposeModifiedProperty>?, System.Func<UnityCompose.ComposeModifiedProperty, bool>>(-1013838698, newProperties) ? CurrentComposer.RememberedValue<StableCollections.IMutableStableSet<UnityCompose.ComposeModifiedProperty>?, System.Func<UnityCompose.ComposeModifiedProperty, bool>>() : CurrentComposer.WriteLambda<StableCollections.IMutableStableSet<UnityCompose.ComposeModifiedProperty>?, System.Func<UnityCompose.ComposeModifiedProperty, bool>>(it => !newProperties.Contains(it)));
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
            var a = CurrentComposer.HasRememberedValue<bool, int>(1353683705, true) ? CurrentComposer.RememberedValue<bool, int>() : CurrentComposer.WriteValue<bool, int>(() => 1);
            LaunchedEffect(1, CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IModifier?, T?>, System.Action>(-719417518, (resolvedModifier, visualElement)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IModifier?, T?>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.IModifier?, T?>, System.Action>(() => resolvedModifier?.Apply(visualElement)));
            resolvedModifier?.Apply(visualElement);
            FireOnGloballyPositionedCallback(visualElement);
            var currentInitializer = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<System.Action<T>?>>(-663587346, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<System.Action<T>?>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<System.Action<T>?>>(() => IMutableStableProperty.Create<Action<T>?>(null));
            if (initializer != null)
            {
                if (currentInitializer.Value != initializer)
                {
                    currentInitializer.Value = initializer;
                    initializer(visualElement);
                }
            }

            if (content != null)
            {
                CompositionLocalProvider(LocalModifier.Provides((null, null)), LocalVisualElement.Provides(visualElement), LocalLayoutMeasurer.Provides(CurrentComposer.HasRememberedValue<T, UnityCompose.LayoutMeasurerImpl>(302422410, visualElement) ? CurrentComposer.RememberedValue<T, UnityCompose.LayoutMeasurerImpl>() : CurrentComposer.WriteValue<T, UnityCompose.LayoutMeasurerImpl>(() => new LayoutMeasurerImpl(visualElement))), content: content);
            }
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<IModifier?, Action<T>?, ComposableContent?>, Action>(546109290, (__modifier, __initializer, __content)) ? CurrentComposer.RememberedValue<ValueTuple<IModifier?, Action<T>?, ComposableContent?>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<IModifier?, Action<T>?, ComposableContent?>, Action>(() => __ReusableComposeView(__modifier, __initializer, __content)));
        }
    }

    [Composable]
    private static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup(511035581, (__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(-1156036186, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(511135581, (__content, __modifier, __horizontalAlignment, __verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(() => __Column(__content, __modifier, __horizontalAlignment, __verticalAlignment)));
        }
    }

    [Composable]
    private static void __Row(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup(-1566475176, (__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(-811496807, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(-1566375176, (__content, __modifier, __horizontalAlignment, __verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(() => __Row(__content, __modifier, __horizontalAlignment, __verticalAlignment)));
        }
    }

    [Composable]
    private static void __Box(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        if (CurrentComposer.BeginComposeGroup(567914418, (__content, __modifier, __horizontalAlignment, __verticalAlignment)))
            return;
        try
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(900884801, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(568014418, (__content, __modifier, __horizontalAlignment, __verticalAlignment)) ? CurrentComposer.RememberedValue<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<ComposableContent, IModifier?, Alignment.Horizontal, Alignment.Vertical>, Action>(() => __Box(__content, __modifier, __horizontalAlignment, __verticalAlignment)));
        }
    }

    [Composable]
    private static void __Spacer(IModifier modifier)
    {
        var __modifier = (modifier);
        if (CurrentComposer.BeginComposeGroup(-1503161144, __modifier))
            return;
        try
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<IModifier, Action>(-1503061144, __modifier) ? CurrentComposer.RememberedValue<IModifier, Action>() : CurrentComposer.WriteComposableLambda<IModifier, Action>(() => __Spacer(__modifier)));
        }
    }

    [Composable]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        if (CurrentComposer.BeginComposeGroup(1097159214, (__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)))
            return;
        try
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            ReusableComposeView<Text>(modifier: modifier, initializer: CurrentComposer.HasRememberedValue<ValueTuple<string, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<float>, SharpExtensions.Optional<UnityCompose.TextStyle>, SharpExtensions.Optional<UnityCompose.FontStyle>, SharpExtensions.Optional<UnityCompose.FontWeight>, bool, ValueTuple<UnityCompose.TextAlign, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<UnityCompose.TextStyle>>>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>(200010220, (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, localContentColor, localTextStyle)) ? CurrentComposer.RememberedValue<ValueTuple<string, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<float>, SharpExtensions.Optional<UnityCompose.TextStyle>, SharpExtensions.Optional<UnityCompose.FontStyle>, SharpExtensions.Optional<UnityCompose.FontWeight>, bool, ValueTuple<UnityCompose.TextAlign, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<UnityCompose.TextStyle>>>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>() : CurrentComposer.WriteLambda<ValueTuple<string, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<float>, SharpExtensions.Optional<UnityCompose.TextStyle>, SharpExtensions.Optional<UnityCompose.FontStyle>, SharpExtensions.Optional<UnityCompose.FontWeight>, bool, ValueTuple<UnityCompose.TextAlign, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<UnityCompose.TextStyle>>>, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>(it =>
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
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<string, Optional<Color>, Optional<float>, Optional<TextStyle>, Optional<FontStyle>, Optional<FontWeight>, bool, ValueTuple<TextAlign, IModifier?>>, Action>(1097259214, (__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)) ? CurrentComposer.RememberedValue<ValueTuple<string, Optional<Color>, Optional<float>, Optional<TextStyle>, Optional<FontStyle>, Optional<FontWeight>, bool, ValueTuple<TextAlign, IModifier?>>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<string, Optional<Color>, Optional<float>, Optional<TextStyle>, Optional<FontStyle>, Optional<FontWeight>, bool, ValueTuple<TextAlign, IModifier?>>, Action>(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)));
        }
    }

    [Composable]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        if (CurrentComposer.BeginComposeGroup(-951093574, (__image, __tint, __modifier)))
            return;
        try
        {
            ReusableComposeView<Image>(initializer: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.ComposeImage, UnityEngine.Color?>, System.Action<UnityEngine.UIElements.Image>?>(70091237, (image, tint)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.ComposeImage, UnityEngine.Color?>, System.Action<UnityEngine.UIElements.Image>?>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.ComposeImage, UnityEngine.Color?>, System.Action<UnityEngine.UIElements.Image>?>(it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint ?? Color.white;
            }), modifier: modifier);
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposeImage, Color?, IModifier?>, Action>(-950993574, (__image, __tint, __modifier)) ? CurrentComposer.RememberedValue<ValueTuple<ComposeImage, Color?, IModifier?>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<ComposeImage, Color?, IModifier?>, Action>(() => __Image(__image, __tint, __modifier)));
        }
    }
}