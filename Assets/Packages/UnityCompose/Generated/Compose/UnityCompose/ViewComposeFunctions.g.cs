#nullable enable
using System;
using System.Collections.Generic;
using NUnit.Framework;
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
    public static void __ReusableComposeView<T>(IModifier? modifier = null, Action<T>? initializer = null, ComposableContent? content = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
        where T : VisualElement, new()
    {
        var(__modifier, __initializer, __content) = (modifier, initializer, content);
        var __isCreated = __composer.StartRestartGroup(1765969868);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(initializer) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var composer = __composer;
            composer.StartReusableGroup<T>(123);
            var parent = composer.GetParentVisualElement().NotNull();
            var indexInParent = composer.GetElementIndex();
            var node = composer.GetReusableNode<T>();
            var visualElement = node.VisualElement.NotNull();
            composer.EnterVisualElement(visualElement);
            __composer.StartReplaceGroup(1320298374);
            if (modifier is { IsComposable: true })
                modifier = modifier.__Compose(__composer: __composer, __changed: 0b_00);
            __composer.EndReplaceGroup(1320298374);
            node.Update(parent: parent, indexInParent: indexInParent, modifier: modifier, initializer: initializer);
            __composer.StartReplaceGroup(1265773657);
            __composer.StartReplaceGroup(1265773657);
            content?.Invoke();
            __composer.EndReplaceGroup(1265773657);
            __composer.EndReplaceGroup(1265773657);
            composer.EndReusableGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(1765969868, __isRestarted)?.UpdateScope(() => __ReusableComposeView(__modifier, __initializer, __content, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal? horizontalAlignment = null, Arrangement.Vertical? verticalArrangement = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalArrangement) = (content, modifier, horizontalAlignment, verticalArrangement);
        var __isCreated = __composer.StartRestartGroup(582601264);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(horizontalAlignment) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalArrangement) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
        {
            __ReusableComposeView<Column>(modifier: modifier, initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).ChangedAsFlag((__dirty & 0b_11_00_00_00) == 0b_10_00_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>>(it =>
            {
                it.style.alignItems = (horizontalAlignment ?? Alignment.Left).ToAlign();
                it.style.justifyContent = (verticalArrangement ?? Arrangement.Top).ToJustify();
            })), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) >> 2) | ((__dirty & 0b_00_00_11) << 4));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01;
        __composer.EndRestartGroup(582601264, __isRestarted)?.UpdateScope(() => __Column(__content, __modifier, __horizontalAlignment, __verticalArrangement, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Row(ComposableContent content, IModifier? modifier = null, Arrangement.Horizontal? horizontalArrangement = null, Alignment.Vertical? verticalAlignment = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __horizontalArrangement, __verticalAlignment) = (content, modifier, horizontalArrangement, verticalAlignment);
        var __isCreated = __composer.StartRestartGroup(725539182);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(horizontalArrangement) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalAlignment) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01)
        {
            __ReusableComposeView<Row>(modifier: modifier, initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_11_00_00) == 0b_00_10_00_00).ChangedAsFlag((__dirty & 0b_11_00_00_00) == 0b_10_00_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>>(it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
                it.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
            })), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) >> 2) | ((__dirty & 0b_00_00_11) << 4));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01;
        __composer.EndRestartGroup(725539182, __isRestarted)?.UpdateScope(() => __Row(__content, __modifier, __horizontalArrangement, __verticalAlignment, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Box(ComposableContent content, IModifier? modifier = null, Alignment? alignment = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __alignment) = (content, modifier, alignment);
        var __isCreated = __composer.StartRestartGroup(1037535202);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(alignment) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            __ReusableComposeView<Box>(modifier: modifier, initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_11_00_00) == 0b_10_00_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>>(it =>
            {
                var resolvedAlignment = alignment ?? Alignment.TopLeft;
                var(align, justify) = (resolvedAlignment.ToAlign(), resolvedAlignment.ToJustify());
                it.style.alignItems = align;
                it.style.justifyContent = justify;
            })), content: content, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) >> 2) | ((__dirty & 0b_00_00_11) << 4));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(1037535202, __isRestarted)?.UpdateScope(() => __Box(__content, __modifier, __alignment, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Spacer(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(930722157);
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01)
        {
            __ReusableComposeView<Spacer>(modifier: modifier, __composer: __composer, __changed: 0b_01_01_00 | (__dirty & 0b_11));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01;
        __composer.EndRestartGroup(930722157, __isRestarted)?.UpdateScope(() => __Spacer(__modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Text(string text, Optional<Color> color = default, Optional<Sp> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        var __isCreated = __composer.StartRestartGroup(860831349);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(text) ? 0b_00_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(color) ? 0b_00_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(fontSize) ? 0b_00_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(style) ? 0b_00_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(fontStyle) ? 0b_00_00_00_00_10_00_00_00_00 : 0b_00_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(fontWeight) ? 0b_00_00_00_10_00_00_00_00_00 : 0b_00_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(softWrap) ? 0b_00_00_10_00_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(textAlign) ? 0b_00_10_00_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01_01)
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            var resolvedFontStyle = (!__composer.Changed<(global::SharpExtensions.Optional<global::UnityCompose.FontStyle> fontStyle, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> style, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> localTextStyle)>((fontStyle, style, localTextStyle)!) ? __composer.RememberedValue<global::UnityCompose.FontStyle>() : __composer.UpdateRememberedValue<global::UnityCompose.FontStyle>(fontStyle.HasValue ? fontStyle.Value : style.HasValue ? style.Value.FontStyle : localTextStyle.HasValue ? localTextStyle.Value.FontStyle : FontStyle.Normal));
            var resolvedFontWeight = (!__composer.Changed<(global::SharpExtensions.Optional<global::UnityCompose.FontWeight> fontWeight, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> style, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> localTextStyle)>((fontWeight, style, localTextStyle)!) ? __composer.RememberedValue<global::UnityCompose.FontWeight>() : __composer.UpdateRememberedValue<global::UnityCompose.FontWeight>(fontWeight.HasValue ? fontWeight.Value : style.HasValue ? style.Value.FontWeight : localTextStyle.HasValue ? localTextStyle.Value.FontWeight : FontWeight.Normal));
            var resolvedFontSize = (!__composer.Changed<(global::SharpExtensions.Optional<global::UnityCompose.Sp> fontSize, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> style, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> localTextStyle)>((fontSize, style, localTextStyle)!) ? __composer.RememberedValue<global::UnityCompose.Sp>() : __composer.UpdateRememberedValue<global::UnityCompose.Sp>(fontSize.HasValue ? fontSize.Value : style.HasValue ? style.Value.FontSize : localTextStyle.HasValue ? localTextStyle.Value.FontSize : 14.Sp()));
            var resolvedFontSizeValue = resolvedFontSize.__Resolve(__composer: __composer, __changed: 0b_00);
            var resolvedColor = (!__composer.Changed<(global::SharpExtensions.Optional<global::UnityEngine.Color> color, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> style, global::SharpExtensions.Optional<global::UnityEngine.Color> localContentColor, global::SharpExtensions.Optional<global::UnityCompose.TextStyle> localTextStyle)>((color, style, localContentColor, localTextStyle)!) ? __composer.RememberedValue<global::UnityEngine.Color>() : __composer.UpdateRememberedValue<global::UnityEngine.Color>(color.HasValue ? color.Value : style is { HasValue: true, Value.Color.HasValue: true } ? style.Value.Color.Value : localContentColor.HasValue ? localContentColor.Value : localTextStyle is { HasValue: true, Value.Color.HasValue: true } ? localTextStyle.Value.Color.Value : Color.black));
            __ReusableComposeView<Text>(modifier: modifier, initializer: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_00_00_00_11) == 0b_00_00_00_00_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00_00_00_00) == 0b_00_00_10_00_00_00_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00_00_00_00) == 0b_00_10_00_00_00_00_00_00_00).Changed<global::UnityCompose.FontStyle>(resolvedFontStyle!).Changed<global::UnityCompose.FontWeight>(resolvedFontWeight!).Changed<float>(resolvedFontSizeValue!).Changed<global::UnityEngine.Color>(resolvedColor!).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>(it =>
            {
                it.text = text;
                it.style.whiteSpace = softWrap ? WhiteSpace.Normal : WhiteSpace.NoWrap;
                it.style.unityFontStyleAndWeight = FontStyleUtils.ToUnityFontStyle(resolvedFontStyle, resolvedFontWeight);
                it.style.unityTextAlign = textAlign.ToTextAnchor();
                it.style.fontSize = resolvedFontSizeValue;
                it.style.color = resolvedColor;
            })), __composer: __composer, __changed: 0b_01_00_00 | ((__dirty & 0b_11_00_00_00_00_00_00_00_00) >> 16));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(860831349, __isRestarted)?.UpdateScope(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }

    public static void __Image(ComposeImage image, Optional<Color> tint = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        var __isCreated = __composer.StartRestartGroup(1956783564);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(image) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(tint) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            __ReusableComposeView<Image>(initializer: (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).ChangedAsFlag((__dirty & 0b_00_11_00) == 0b_00_10_00).Get() ? __composer.RememberedValue<global::System.Action<global::UnityEngine.UIElements.Image>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityEngine.UIElements.Image>>(it =>
            {
                it.sprite = image.Sprite;
                it.vectorImage = image.VectorImage;
                it.image = image.Texture;
                it.tintColor = tint.GetOrDefault(Color.white);
            })), modifier: modifier, __composer: __composer, __changed: 0b_01_00_00 | ((__dirty & 0b_11_00_00) >> 4));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(1956783564, __isRestarted)?.UpdateScope(() => __Image(__image, __tint, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}