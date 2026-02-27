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
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10 : 0b_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(initializer) ? 0b_00_10_00 : 0b_00_01_00;
        else
            __dirtyRestart |= 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_10_00_00 : 0b_01_00_00;
        else
            __dirtyRestart |= 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var composer = __composer;
            composer.StartReusableGroup(123);
            var parent = composer.GetParentVisualElement().NotNull();
            var indexInParent = composer.GetElementIndex();
            var node = composer.GetReusableNode<T>();
            node.VisualElement ??= new T();
            var visualElement = node.VisualElement.NotNull();
            composer.EnterVisualElement(visualElement);
            node.Update(parent: parent, indexInParent: indexInParent, modifier: modifier, initializer: initializer);
            __composer.StartReplaceGroup(708751226);
            __composer.StartReplaceGroup(708751226);
            content?.Invoke();
            __composer.EndReplaceGroup(708751226);
            __composer.EndReplaceGroup(708751226);
            composer.EndReusableGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(1765969868, __isRestarted)?.UpdateScope(() => __ReusableComposeView(__modifier, __initializer, __content, __composer, __dirtyRestart));
    }

    public static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal? horizontalAlignment = null, Arrangement.Vertical? verticalArrangement = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalArrangement) = (content, modifier, horizontalAlignment, verticalArrangement);
        var __isCreated = __composer.StartRestartGroup(1922237605);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        else
            __dirtyRestart |= 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(horizontalAlignment) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        else
            __dirtyRestart |= 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalArrangement) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        else
            __dirtyRestart |= 0b_01_00_00_00;
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
        __composer.EndRestartGroup(1922237605, __isRestarted)?.UpdateScope(() => __Column(__content, __modifier, __horizontalAlignment, __verticalArrangement, __composer, __dirtyRestart));
    }

    public static void __Row(ComposableContent content, IModifier? modifier = null, Arrangement.Horizontal? horizontalArrangement = null, Alignment.Vertical? verticalAlignment = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __horizontalArrangement, __verticalAlignment) = (content, modifier, horizontalArrangement, verticalAlignment);
        var __isCreated = __composer.StartRestartGroup(2110962436);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        else
            __dirtyRestart |= 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(horizontalArrangement) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        else
            __dirtyRestart |= 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(verticalAlignment) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        else
            __dirtyRestart |= 0b_01_00_00_00;
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
        __composer.EndRestartGroup(2110962436, __isRestarted)?.UpdateScope(() => __Row(__content, __modifier, __horizontalArrangement, __verticalAlignment, __composer, __dirtyRestart));
    }

    public static void __Box(ComposableContent content, IModifier? modifier = null, Alignment? alignment = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __alignment) = (content, modifier, alignment);
        var __isCreated = __composer.StartRestartGroup(734131074);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_10 : 0b_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_00_10_00 : 0b_00_01_00;
        else
            __dirtyRestart |= 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(alignment) ? 0b_10_00_00 : 0b_01_00_00;
        else
            __dirtyRestart |= 0b_01_00_00;
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
        __composer.EndRestartGroup(734131074, __isRestarted)?.UpdateScope(() => __Box(__content, __modifier, __alignment, __composer, __dirtyRestart));
    }

    public static void __Spacer(IModifier modifier, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __modifier = (modifier);
        var __isCreated = __composer.StartRestartGroup(1810854548);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10 : 0b_01;
        else
            __dirtyRestart |= 0b_01;
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
        __composer.EndRestartGroup(1810854548, __isRestarted)?.UpdateScope(() => __Spacer(__modifier, __composer, __dirtyRestart));
    }

    public static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        var __isCreated = __composer.StartRestartGroup(579625941);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(text) ? 0b_00_00_00_00_00_00_00_00_10 : 0b_00_00_00_00_00_00_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_00_00_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_00_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(color) ? 0b_00_00_00_00_00_00_00_10_00 : 0b_00_00_00_00_00_00_00_01_00;
        else
            __dirtyRestart |= 0b_00_00_00_00_00_00_00_01_00;
        if ((__changed & 0b_00_00_00_00_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(fontSize) ? 0b_00_00_00_00_00_00_10_00_00 : 0b_00_00_00_00_00_00_01_00_00;
        else
            __dirtyRestart |= 0b_00_00_00_00_00_00_01_00_00;
        if ((__changed & 0b_00_00_00_00_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(style) ? 0b_00_00_00_00_00_10_00_00_00 : 0b_00_00_00_00_00_01_00_00_00;
        else
            __dirtyRestart |= 0b_00_00_00_00_00_01_00_00_00;
        if ((__changed & 0b_00_00_00_00_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(fontStyle) ? 0b_00_00_00_00_10_00_00_00_00 : 0b_00_00_00_00_01_00_00_00_00;
        else
            __dirtyRestart |= 0b_00_00_00_00_01_00_00_00_00;
        if ((__changed & 0b_00_00_00_11_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(fontWeight) ? 0b_00_00_00_10_00_00_00_00_00 : 0b_00_00_00_01_00_00_00_00_00;
        else
            __dirtyRestart |= 0b_00_00_00_01_00_00_00_00_00;
        if ((__changed & 0b_00_00_11_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(softWrap) ? 0b_00_00_10_00_00_00_00_00_00 : 0b_00_00_01_00_00_00_00_00_00;
        else
            __dirtyRestart |= 0b_00_00_01_00_00_00_00_00_00;
        if ((__changed & 0b_00_11_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(textAlign) ? 0b_00_10_00_00_00_00_00_00_00 : 0b_00_01_00_00_00_00_00_00_00;
        else
            __dirtyRestart |= 0b_00_01_00_00_00_00_00_00_00;
        if ((__changed & 0b_11_00_00_00_00_00_00_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00_00_00_00_00 : 0b_01_00_00_00_00_00_00_00_00;
        else
            __dirtyRestart |= 0b_01_00_00_00_00_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01_01_01_01_01)
        {
            var localContentColor = LocalContentColor.Current;
            var localTextStyle = LocalTextStyle.Current;
            __ReusableComposeView<Text>(modifier: modifier, initializer: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_00_00_00_00_11) == 0b_00_00_00_00_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_00_00_00_00_00_11_00) == 0b_00_00_00_00_00_00_00_10_00).ChangedAsFlag((__dirty & 0b_00_00_00_00_00_00_11_00_00) == 0b_00_00_00_00_00_00_10_00_00).ChangedAsFlag((__dirty & 0b_00_00_00_00_00_11_00_00_00) == 0b_00_00_00_00_00_10_00_00_00).ChangedAsFlag((__dirty & 0b_00_00_00_00_11_00_00_00_00) == 0b_00_00_00_00_10_00_00_00_00).ChangedAsFlag((__dirty & 0b_00_00_00_11_00_00_00_00_00) == 0b_00_00_00_10_00_00_00_00_00).ChangedAsFlag((__dirty & 0b_00_00_11_00_00_00_00_00_00) == 0b_00_00_10_00_00_00_00_00_00).ChangedAsFlag((__dirty & 0b_00_11_00_00_00_00_00_00_00) == 0b_00_10_00_00_00_00_00_00_00).Changed<global::SharpExtensions.Optional<global::UnityEngine.Color>>(localContentColor!).Changed<global::SharpExtensions.Optional<global::UnityCompose.TextStyle>>(localTextStyle!).Get() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Text>>(it =>
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
            })), __composer: __composer, __changed: 0b_01_00_00 | ((__dirty & 0b_11_00_00_00_00_00_00_00_00) >> 16));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01_01_01_01_01;
        __composer.EndRestartGroup(579625941, __isRestarted)?.UpdateScope(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier, __composer, __dirtyRestart));
    }

    public static void __Image(ComposeImage image, Optional<Color> tint = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        var __isCreated = __composer.StartRestartGroup(1428143876);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(image) ? 0b_00_00_10 : 0b_00_00_01;
        else
            __dirtyRestart |= 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(tint) ? 0b_00_10_00 : 0b_00_01_00;
        else
            __dirtyRestart |= 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
        else
            __dirtyRestart |= 0b_01_00_00;
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
        __composer.EndRestartGroup(1428143876, __isRestarted)?.UpdateScope(() => __Image(__image, __tint, __modifier, __composer, __dirtyRestart));
    }
}