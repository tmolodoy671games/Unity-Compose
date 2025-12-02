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
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1230753333);
        if (__composer.ShouldExecute((__modifier, __initializer, __content)))
        {
            CurrentComposer.StartReusableGroup(123);
            var visualElement = CurrentComposer.GetOrCreateVisualElement<T>();
            var parent = LocalVisualElement.Current;
            var index = CurrentComposer.GetElementIndex();
            DisposableEffect((visualElement, parent, index), !__composer.RememberedKeyChanged<ValueTuple<T?, UnityEngine.UIElements.VisualElement?, int>>(1862079167, (visualElement, parent, index)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                parent.FastReinsert(index, visualElement);
                return it.OnDispose(!__composer.RememberedKeyChanged<ValueTuple<T?, UnityEngine.UIElements.VisualElement?>>(1487837232, (visualElement, parent)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => parent.Remove(visualElement)));
            }));
            CurrentComposer.EnterVisualElement();
            var resolvedModifier = modifier;
            var localStyle = LocalModifier.Current;
            if (localStyle.Before != null)
                resolvedModifier = localStyle.Before.Then(resolvedModifier.OrEmpty());
            if (localStyle.After != null)
                resolvedModifier = resolvedModifier.OrEmpty().Then(localStyle.After);
            var currentModifier = !__composer.RememberedKeyChanged<bool>(584154846, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<UnityCompose.IModifier?>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<UnityCompose.IModifier?>>(IMutableStableProperty.Create<IModifier?>(null));
            var currentProperties = !__composer.RememberedKeyChanged<bool>(349352904, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<StableCollections.IStableSet<UnityCompose.ComposeModifiedProperty>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<StableCollections.IStableSet<UnityCompose.ComposeModifiedProperty>>>(IMutableStableProperty.Create<IStableSet<ComposeModifiedProperty>>(IImmutableStableSet.Empty<ComposeModifiedProperty>()));
            var newProperties = IMutableStableSet.Create<ComposeModifiedProperty>();
            resolvedModifier?.Apply(newProperties);
            var propertiesToRevert = currentProperties.Value.Where(!__composer.RememberedKeyChanged<StableCollections.IMutableStableSet<UnityCompose.ComposeModifiedProperty>?>(1986802330, newProperties) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.ComposeModifiedProperty, bool>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.ComposeModifiedProperty, bool>>(it => !newProperties.Contains(it)));
            __composer.StartReplaceGroup(-1759835366);
            foreach (var property in propertiesToRevert)
                property.Revert(visualElement);
            __composer.EndReplaceGroup(-1759835366);
            currentProperties.Value = newProperties;
            currentModifier.Value = resolvedModifier;
            visualElement.ClearCallbacks();
            visualElement.style.transitionDelay.value?.Clear();
            visualElement.style.transitionDuration.value?.Clear();
            visualElement.style.transitionProperty.value?.Clear();
            visualElement.style.transitionTimingFunction.value?.Clear();
            visualElement.pickingMode = PickingMode.Ignore;
            visualElement.style.overflow = Overflow.Visible;
            LaunchedEffect(1, !__composer.RememberedKeyChanged<ValueTuple<T?, UnityCompose.IModifier?>>(-1783067216, (visualElement, resolvedModifier)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => resolvedModifier?.Apply(visualElement)));
            resolvedModifier?.Apply(visualElement);
            FireOnGloballyPositionedCallback(visualElement);
            var currentInitializer = !__composer.RememberedKeyChanged<bool>(211651232, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<System.Action<T>?>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<System.Action<T>?>>(IMutableStableProperty.Create<Action<T>?>(null));
            __composer.StartReplaceGroup(398703009);
            if (initializer != null)
            {
                __composer.StartReplaceGroup(1625118031);
                if (currentInitializer.Value != initializer)
                {
                    currentInitializer.Value = initializer;
                    initializer(visualElement);
                }

                __composer.EndReplaceGroup(1625118031);
            }

            __composer.EndReplaceGroup(398703009);
            __composer.StartReplaceGroup(502670197);
            if (content != null)
            {
                CompositionLocalProvider(LocalModifier.Provides((null, null)), LocalVisualElement.Provides(visualElement), LocalLayoutMeasurer.Provides(!__composer.RememberedKeyChanged<T>(-1649070255, visualElement) ? __composer.RememberedValue<UnityCompose.LayoutMeasurerImpl>() : __composer.UpdateRememberedValue<UnityCompose.LayoutMeasurerImpl>(new LayoutMeasurerImpl(visualElement))), content: content);
            }

            __composer.EndReplaceGroup(502670197);
            CurrentComposer.EndReusableGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1230753333)?.UpdateScope(() => __ReusableComposeView(__modifier, __initializer, __content));
    }

    [Composable]
    private static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1269982359);
        if (__composer.ShouldExecute((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>>(176466432, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1269982359)?.UpdateScope(() => __Column(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Row(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1510478461);
        if (__composer.ShouldExecute((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>>(922852958, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1510478461)?.UpdateScope(() => __Row(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Box(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1115575440);
        if (__composer.ShouldExecute((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.Alignment.Horizontal, UnityCompose.Alignment.Vertical>>(663077804, (horizontalAlignment, verticalAlignment)) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1115575440)?.UpdateScope(() => __Box(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Spacer(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-50213941);
        if (__composer.ShouldExecute(__modifier))
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-50213941)?.UpdateScope(() => __Spacer(__modifier));
    }

    [Composable]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-480385983);
        if (__composer.ShouldExecute((__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)))
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            ReusableComposeView<Text>(modifier: modifier, initializer: !__composer.RememberedKeyChanged<ValueTuple<string, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<float>, SharpExtensions.Optional<UnityCompose.TextStyle>, SharpExtensions.Optional<UnityCompose.FontStyle>, SharpExtensions.Optional<UnityCompose.FontWeight>, bool, ValueTuple<UnityCompose.TextAlign, SharpExtensions.Optional<UnityEngine.Color>, SharpExtensions.Optional<UnityCompose.TextStyle>>>>(365779819, (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, localContentColor, localTextStyle)) ? CurrentComposer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>() : CurrentComposer.UpdateLambda<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>(it =>
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
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-480385983)?.UpdateScope(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier));
    }

    [Composable]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1018411186);
        if (__composer.ShouldExecute((__image, __tint, __modifier)))
        {
            ReusableComposeView<Image>(initializer: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposeImage, UnityEngine.Color?>>(75317162, (image, tint)) ? CurrentComposer.RememberedValue<System.Action<UnityEngine.UIElements.Image>?>() : CurrentComposer.UpdateLambda<System.Action<UnityEngine.UIElements.Image>?>(it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint ?? Color.white;
            }), modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1018411186)?.UpdateScope(() => __Image(__image, __tint, __modifier));
    }
}