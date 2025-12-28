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
    private static void __WithModifiers(ComposableContent content, IModifier? before = null, IModifier? after = null)
    {
        var(__content, __before, __after) = (content, before, after);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(2065177648);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __before, __after)))
        {
            var composer = CurrentComposer;
            composer.StartModifierGroup(1337);
            composer.PushModifiers(before, after);
            content();
            composer.EndModifierGroup(1337);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(2065177648, __isRestarted)?.UpdateScope(() => __WithModifiers(__content, __before, __after));
    }

    [Composable]
    private static void __ReusableComposeView<T>(IModifier? modifier = null, Action<T>? initializer = null, ComposableContent? content = null)
        where T : VisualElement, new()
    {
        var(__modifier, __initializer, __content) = (modifier, initializer, content);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(576016899);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__modifier, __initializer, __content)))
        {
            var composer = CurrentComposer;
            composer.StartReusableGroup(123);
            var parent = composer.GetParentVisualElement().NotNull();
            var indexInParent = composer.GetElementIndex();
            var node = composer.GetReusableNode<T>();
            var modifiers = composer.GetModifiers();
            node.VisualElement ??= new T();
            var visualElement = node.VisualElement.NotNull();
            composer.EnterVisualElement(visualElement);
            __composer.StartReplaceGroup(-621423155);
            if (true)
                modifier = modifier?.Compose();
            __composer.EndReplaceGroup(-621423155);
            node.Update(parent: parent, indexInParent: indexInParent, modifiers: modifiers, modifier: modifier, initializer: initializer);
            __composer.StartReplaceGroup(1942991667);
            if (content != null)
            {
                WithModifiers(before: null, after: null, content: content);
            }

            __composer.EndReplaceGroup(1942991667);
            composer.EndReusableGroup(123);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(576016899, __isRestarted)?.UpdateScope(() => __ReusableComposeView(__modifier, __initializer, __content));
    }

    [Composable]
    private static void __Column(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1518836211);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Column>(modifier: modifier, initializer: !__composer.ChangedAsStruct((horizontalAlignment, verticalAlignment)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1518836211, __isRestarted)?.UpdateScope(() => __Column(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Row(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1038076151);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Row>(modifier: modifier, initializer: !__composer.ChangedAsStruct((horizontalAlignment, verticalAlignment)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Row>?>(it =>
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

        __composer.EndRestartGroup(-1038076151, __isRestarted)?.UpdateScope(() => __Row(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Box(ComposableContent content, IModifier? modifier = null, Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left, Alignment.Vertical verticalAlignment = Alignment.Vertical.Top)
    {
        var(__content, __modifier, __horizontalAlignment, __verticalAlignment) = (content, modifier, horizontalAlignment, verticalAlignment);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-461003948);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __horizontalAlignment, __verticalAlignment)))
        {
            ReusableComposeView<Box>(modifier: modifier, initializer: !__composer.ChangedAsStruct((horizontalAlignment, verticalAlignment)) ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box>?>(it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            }), content: content);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-461003948, __isRestarted)?.UpdateScope(() => __Box(__content, __modifier, __horizontalAlignment, __verticalAlignment));
    }

    [Composable]
    private static void __Spacer(IModifier modifier)
    {
        var __modifier = (modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1660626813);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute(__modifier))
        {
            ReusableComposeView<Spacer>(modifier: modifier);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1660626813, __isRestarted)?.UpdateScope(() => __Spacer(__modifier));
    }

    [Composable]
    private static void __Text(string text, Optional<Color> color = default, Optional<float> fontSize = default, Optional<TextStyle> style = default, Optional<FontStyle> fontStyle = default, Optional<FontWeight> fontWeight = default, bool softWrap = true, TextAlign textAlign = TextAlign.UpperLeft, IModifier? modifier = null)
    {
        var(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier) = (text, color, fontSize, style, fontStyle, fontWeight, softWrap, textAlign, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-624556531);
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

        __composer.EndRestartGroup(-624556531, __isRestarted)?.UpdateScope(() => __Text(__text, __color, __fontSize, __style, __fontStyle, __fontWeight, __softWrap, __textAlign, __modifier));
    }

    [Composable]
    private static void __Image(ComposeImage image, Color? tint = null, IModifier? modifier = null)
    {
        var(__image, __tint, __modifier) = (image, tint, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-252250433);
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

        __composer.EndRestartGroup(-252250433, __isRestarted)?.UpdateScope(() => __Image(__image, __tint, __modifier));
    }
}