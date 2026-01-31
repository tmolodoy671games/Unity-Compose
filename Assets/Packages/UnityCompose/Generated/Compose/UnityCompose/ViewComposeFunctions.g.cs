#nullable enable
using System;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
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
        __composer.StartRestartGroup(-715795794);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__modifier, __initializer, __content)))
        {
            var composer = CurrentComposer;
            composer.StartReusableGroup(123);
            var parent = composer.GetParentVisualElement().NotNull();
            var indexInParent = composer.GetElementIndex();
            var node = composer.GetReusableNode<T>();
            node.VisualElement ??= new T();
            var visualElement = node.VisualElement.NotNull();
            composer.EnterVisualElement(visualElement);
            node.Update(parent: parent, indexInParent: indexInParent, modifier: modifier, initializer: initializer);
            __composer.StartReplaceGroup(945714250);
            content?.Invoke();
            __composer.EndReplaceGroup(945714250);
            composer.EndReusableGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-715795794, __isRestarted)?.UpdateScope(() => __ReusableComposeView(__modifier, __initializer, __content));
    }

    [Composable]
    private static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal? horizontalAlignment = null, Arrangement.Vertical? verticalArrangement = null)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalArrangement) = (content, modifier, horizontalAlignment, verticalArrangement);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1973896401);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __horizontalAlignment, __verticalArrangement)))
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: !__composer.ChangedAsStruct((horizontalAlignment, verticalArrangement)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(it =>
            {
                it.style.alignItems = (horizontalAlignment ?? Alignment.Left).ToAlign();
                it.style.justifyContent = (verticalArrangement ?? Arrangement.Top).ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1973896401, __isRestarted)?.UpdateScope(() => __Column(__content, __modifier, __horizontalAlignment, __verticalArrangement));
    }

    [Composable]
    private static void __Row(ComposableContent content, IModifier? modifier = null, Arrangement.Horizontal? horizontalArrangement = null, Alignment.Vertical? verticalAlignment = null)
    {
        var(__content, __modifier, __horizontalArrangement, __verticalAlignment) = (content, modifier, horizontalArrangement, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1840176122);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __horizontalArrangement, __verticalAlignment)))
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: !__composer.ChangedAsStruct((horizontalArrangement, verticalAlignment)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
                it.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1840176122, __isRestarted)?.UpdateScope(() => __Row(__content, __modifier, __horizontalArrangement, __verticalAlignment));
    }

    [Composable]
    private static void __Box(ComposableContent content, IModifier? modifier = null, Alignment? alignment = null)
    {
        var(__content, __modifier, __alignment) = (content, modifier, alignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1004496921);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __alignment)))
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: !__composer.Changed(alignment) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(it =>
            {
                var resolvedAlignment = alignment ?? Alignment.TopLeft;
                var(align, justify) = (resolvedAlignment.ToAlign(), resolvedAlignment.ToJustify());
                it.style.alignItems = align;
                it.style.justifyContent = justify;
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1004496921, __isRestarted)?.UpdateScope(() => __Box(__content, __modifier, __alignment));
    }

    [Composable]
    private static void __Spacer(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1246952902);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1246952902, __isRestarted)?.UpdateScope(() => __Spacer(__modifier));
    }

    [Composable]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1348786803);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier)))
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            ReusableComposeView<Text>(modifier: modifier, initializer: !__composer.ChangedAsStruct((text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, localContentColor, localTextStyle)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>?>(it =>
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

        __composer.EndRestartGroup(1348786803, __isRestarted)?.UpdateScope(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier));
    }

    [Composable]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(2137052529);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__image, __tint, __modifier)))
        {
            ReusableComposeView<Image>(initializer: !__composer.ChangedAsStruct((image, tint)) ? __composer.RememberedValue<System.Action<UnityEngine.UIElements.Image>?>() : __composer.UpdateRememberedValue<System.Action<UnityEngine.UIElements.Image>?>(it =>
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

        __composer.EndRestartGroup(2137052529, __isRestarted)?.UpdateScope(() => __Image(__image, __tint, __modifier));
    }
}