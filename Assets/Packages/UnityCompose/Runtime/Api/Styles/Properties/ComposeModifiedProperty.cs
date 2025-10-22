using System;
using UnityEngine.UIElements;

#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class ComposeModifiedProperty
{
    public static readonly ComposeModifiedProperty AlignContent =
        new(it => it.style.alignContent = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty AlignItems =
        new(it => it.style.alignItems = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty AlignSelf =
        new(it => it.style.alignSelf = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundColor =
        new(it => it.style.backgroundColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundImage =
        new(it => it.style.backgroundImage = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundPositionX =
        new(it => it.style.backgroundPositionX = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundPositionY =
        new(it => it.style.backgroundPositionY = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundRepeat =
        new(it => it.style.backgroundRepeat = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BackgroundSize =
        new(it => it.style.backgroundSize = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderBottomColor =
        new(it => it.style.borderBottomColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderBottomLeftRadius =
        new(it => it.style.borderBottomLeftRadius = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderBottomRightRadius =
        new(it => it.style.borderBottomRightRadius = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderBottomWidth =
        new(it => it.style.borderBottomWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderLeftColor =
        new(it => it.style.borderLeftColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderLeftWidth =
        new(it => it.style.borderLeftWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderRightColor =
        new(it => it.style.borderRightColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderRightWidth =
        new(it => it.style.borderRightWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderTopColor =
        new(it => it.style.borderTopColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderTopLeftRadius =
        new(it => it.style.borderTopLeftRadius = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderTopRightRadius =
        new(it => it.style.borderTopRightRadius = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty BorderTopWidth =
        new(it => it.style.borderTopWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Bottom =
        new(it => it.style.bottom = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Color =
        new(it => it.style.color = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Cursor =
        new(it => it.style.cursor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Display =
        new(it => it.style.display = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FlexBasis =
        new(it => it.style.flexBasis = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FlexDirection =
        new(it => it.style.flexDirection = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FlexGrow =
        new(it => it.style.flexGrow = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FlexShrink =
        new(it => it.style.flexShrink = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FlexWrap =
        new(it => it.style.flexWrap = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty FontSize =
        new(it => it.style.fontSize = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Height =
        new(it => it.style.height = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty JustifyContent =
        new(it => it.style.justifyContent = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Left =
        new(it => it.style.left = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty LetterSpacing =
        new(it => it.style.letterSpacing = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MarginBottom =
        new(it => it.style.marginBottom = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MarginLeft =
        new(it => it.style.marginLeft = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MarginRight =
        new(it => it.style.marginRight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MarginTop =
        new(it => it.style.marginTop = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MaxHeight =
        new(it => it.style.maxHeight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MaxWidth =
        new(it => it.style.maxWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MinHeight =
        new(it => it.style.minHeight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty MinWidth =
        new(it => it.style.minWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Opacity =
        new(it => it.style.opacity = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Overflow =
        new(it => it.style.overflow = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty PaddingBottom =
        new(it => it.style.paddingBottom = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty PaddingLeft =
        new(it => it.style.paddingLeft = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty PaddingRight =
        new(it => it.style.paddingRight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty PaddingTop =
        new(it => it.style.paddingTop = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Position =
        new(it => it.style.position = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Right =
        new(it => it.style.right = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Rotate =
        new(it => it.style.rotate = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Scale =
        new(it => it.style.scale = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TextOverflow =
        new(it => it.style.textOverflow = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TextShadow =
        new(it => it.style.textShadow = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Top =
        new(it => it.style.top = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TransformOrigin =
        new(it => it.style.transformOrigin = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TransitionDelay =
        new(it => it.style.transitionDelay = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TransitionDuration =
        new(it => it.style.textShadow = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TransitionProperty =
        new(it => it.style.transitionProperty = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty TransitionTimingFunction =
        new(it => it.style.transitionTimingFunction = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Translate =
        new(it => it.style.translate = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityBackgroundImageTintColor =
        new(it => it.style.unityBackgroundImageTintColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityFont =
        new(it => it.style.unityFont = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityFontDefinition =
        new(it => it.style.unityFontDefinition = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityFontStyleAndWeight =
        new(it => it.style.unityFontStyleAndWeight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityOverflowClipBox =
        new(it => it.style.unityOverflowClipBox = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityParagraphSpacing =
        new(it => it.style.unityParagraphSpacing = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnitySliceBottom =
        new(it => it.style.unitySliceBottom = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnitySliceLeft =
        new(it => it.style.unitySliceLeft = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnitySliceRight =
        new(it => it.style.unitySliceRight = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnitySliceScale =
        new(it => it.style.unitySliceScale = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnitySliceTop =
        new(it => it.style.unitySliceTop = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityTextAlign =
        new(it => it.style.unityTextAlign = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityTextOutlineColor =
        new(it => it.style.unityTextOutlineColor = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityTextOutlineWidth =
        new(it => it.style.unityTextOutlineWidth = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityTextOverflowPosition =
        new(it => it.style.unityTextOverflowPosition = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Visibility =
        new(it => it.style.visibility = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty WhiteSpace =
        new(it => it.style.whiteSpace = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Width =
        new(it => it.style.width = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty WordSpacing =
        new(it => it.style.wordSpacing = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty UnityBackgroundScaleMode =
        new(it => it.style.unityBackgroundScaleMode = StyleKeyword.Null);

    public static readonly ComposeModifiedProperty Name =
        new(it => it.name = "");

    public static readonly ComposeModifiedProperty PickingMode =
        new(it => it.pickingMode = UnityEngine.UIElements.PickingMode.Position);

    private readonly Action<VisualElement> _revert;

    public ComposeModifiedProperty(Action<VisualElement> revert)
    {
        _revert = revert;
    }

    public void Revert(VisualElement element)
    {
        _revert(element);
    }
}