using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal static class VisualElementExtensions
{
    public static VisualElement? GetOrNull(this VisualElement element, int index)
    {
        return index >= element.childCount ? null : element[index];
    }

    public static string Format(this VisualElement element)
    {
        var result = element.GetType().Name;
        if (element.name is { Length: > 0 })
            result += $"({element.name})";
        if (element.parent != null)
            result += $"[{element.parent.IndexOf(element)}]";
        if (element is Label label)
            result += $".text={label.text}";
        return result;
    }

    public static bool FastReinsert(this VisualElement parent, int index, VisualElement child)
    {
        if (parent.GetOrNull(index) == child) return false;
        child.RemoveFromHierarchy();
        parent.Insert(index, child);
        return true;
    }
        
    public static void ClearStyle(this VisualElement visualElement)
    {
        // visualElement.ClearCallbacks();
        visualElement.ClearClassList();
        visualElement.name = null;
        visualElement.pickingMode = PickingMode.Position;
        const StyleKeyword nullValue = StyleKeyword.Null;

        var style = visualElement.style;
        if (!style.alignSelf.Equals(nullValue))
            style.alignSelf = nullValue;
        if (!style.alignItems.Equals(Align.FlexStart))
            style.alignItems = Align.FlexStart;
        if (!style.alignContent.Equals(nullValue))
            style.alignContent = nullValue;
        if (!style.justifyContent.Equals(nullValue))
            style.justifyContent = nullValue;
        if (!style.flexDirection.Equals(nullValue))
            style.flexDirection = nullValue;
        if (!style.flexWrap.Equals(nullValue))
            style.flexWrap = nullValue;

        if (!style.flexGrow.Equals(nullValue))
            style.flexGrow = nullValue;
        if (!style.flexShrink.Equals(nullValue))
            style.flexShrink = nullValue;
        if (!style.flexBasis.Equals(nullValue))
            style.flexBasis = nullValue;
        if (!style.position.Equals(nullValue))
            style.position = nullValue;
        if (!style.top.Equals(nullValue))
            style.top = nullValue;
        if (!style.left.Equals(nullValue))
            style.left = nullValue;
        if (!style.right.Equals(nullValue))
            style.right = nullValue;
        if (!style.bottom.Equals(nullValue))
            style.bottom = nullValue;
        if (!style.translate.Equals(nullValue))
            style.translate = nullValue;

        if (!style.backgroundColor.Equals(nullValue))
            style.backgroundColor = nullValue;
        if (!style.backgroundImage.Equals(nullValue))
            style.backgroundImage = nullValue;
        if (!style.backgroundPositionX.Equals(nullValue))
            style.backgroundPositionX = nullValue;
        if (!style.backgroundPositionY.Equals(nullValue))
            style.backgroundPositionY = nullValue;
        if (!style.backgroundRepeat.Equals(nullValue))
            style.backgroundRepeat = nullValue;
        if (!style.backgroundSize.Equals(nullValue))
            style.backgroundSize = nullValue;
        if (!style.unityBackgroundImageTintColor.Equals(nullValue))
            style.unityBackgroundImageTintColor = nullValue;
        // if (!style.unityBackgroundScaleMode.Equals(nullValue))
        //     style.unityBackgroundScaleMode = nullValue;
        if (!style.unitySliceBottom.Equals(nullValue))
            style.unitySliceBottom = nullValue;
        if (!style.unitySliceLeft.Equals(nullValue))
            style.unitySliceLeft = nullValue;
        if (!style.unitySliceRight.Equals(nullValue))
            style.unitySliceRight = nullValue;
        if (!style.unitySliceTop.Equals(nullValue))
            style.unitySliceTop = nullValue;
        if (!style.unitySliceScale.Equals(nullValue))
            style.unitySliceScale = nullValue;
        if (!style.visibility.Equals(nullValue))
            style.visibility = nullValue;
        if (!style.display.Equals(nullValue))
            style.display = nullValue;
        if (!style.opacity.Equals(nullValue))
            style.opacity = nullValue;
        if (!style.scale.Equals(nullValue))
            style.scale = nullValue;
        if (!style.rotate.Equals(nullValue))
            style.rotate = nullValue;
        if (!style.overflow.Equals(Overflow.Hidden))
            style.overflow = Overflow.Hidden;
        if (!style.cursor.Equals(nullValue))
            style.cursor = nullValue;
        if (!style.transformOrigin.Equals(nullValue))
            style.transformOrigin = nullValue;

        if (!style.borderBottomLeftRadius.Equals(nullValue))
            style.borderBottomLeftRadius = nullValue;
        if (!style.borderBottomRightRadius.Equals(nullValue))
            style.borderBottomRightRadius = nullValue;
        if (!style.borderTopLeftRadius.Equals(nullValue))
            style.borderTopLeftRadius = nullValue;
        if (!style.borderTopRightRadius.Equals(nullValue))
            style.borderTopRightRadius = nullValue;
        if (!style.borderTopWidth.Equals(nullValue))
            style.borderTopWidth = nullValue;
        if (!style.borderBottomWidth.Equals(nullValue))
            style.borderBottomWidth = nullValue;
        if (!style.borderLeftWidth.Equals(nullValue))
            style.borderLeftWidth = nullValue;
        if (!style.borderRightWidth.Equals(nullValue))
            style.borderRightWidth = nullValue;
        if (!style.borderTopColor.Equals(nullValue))
            style.borderTopColor = nullValue;
        if (!style.borderBottomColor.Equals(nullValue))
            style.borderBottomColor = nullValue;
        if (!style.borderLeftColor.Equals(nullValue))
            style.borderLeftColor = nullValue;
        if (!style.borderRightColor.Equals(nullValue))
            style.borderRightColor = nullValue;

        if (!style.marginTop.Equals(nullValue))
            style.marginTop = nullValue;
        if (!style.marginBottom.Equals(nullValue))
            style.marginBottom = nullValue;
        if (!style.marginLeft.Equals(nullValue))
            style.marginLeft = nullValue;
        if (!style.marginRight.Equals(nullValue))
            style.marginRight = nullValue;

        if (!style.paddingTop.Equals(nullValue))
            style.paddingTop = nullValue;
        if (!style.paddingBottom.Equals(nullValue))
            style.paddingBottom = nullValue;
        if (!style.paddingLeft.Equals(nullValue))
            style.paddingLeft = nullValue;
        if (!style.paddingRight.Equals(nullValue))
            style.paddingRight = nullValue;

        if (!style.width.Equals(nullValue))
            style.width = nullValue;
        if (!style.height.Equals(nullValue))
            style.height = nullValue;
        if (!style.maxHeight.Equals(nullValue))
            style.maxHeight = nullValue;
        if (!style.maxWidth.Equals(nullValue))
            style.maxWidth = nullValue;

        if (!style.fontSize.Equals(nullValue))
            style.fontSize = nullValue;
        if (!style.letterSpacing.Equals(nullValue))
            style.letterSpacing = nullValue;
        if (!style.textOverflow.Equals(nullValue))
            style.textOverflow = nullValue;
        if (!style.textShadow.Equals(nullValue))
            style.textShadow = nullValue;
        if (!style.unityFont.Equals(nullValue))
            style.unityFont = nullValue;
        if (!style.unityFontDefinition.Equals(nullValue))
            style.unityFontDefinition = nullValue;
        if (!style.unityFontStyleAndWeight.Equals(nullValue))
            style.unityFontStyleAndWeight = nullValue;
        if (!style.unityOverflowClipBox.Equals(nullValue))
            style.unityOverflowClipBox = nullValue;
        if (!style.unityParagraphSpacing.Equals(nullValue))
            style.unityParagraphSpacing = nullValue;
        if (!style.unityTextAlign.Equals(nullValue))
            style.unityTextAlign = nullValue;
        if (!style.unityTextOutlineColor.Equals(nullValue))
            style.unityTextOutlineColor = nullValue;
        if (!style.unityTextOutlineWidth.Equals(nullValue))
            style.unityTextOutlineWidth = nullValue;
        if (!style.unityTextOverflowPosition.Equals(nullValue))
            style.unityTextOverflowPosition = nullValue;
        if (!style.whiteSpace.Equals(nullValue))
            style.whiteSpace = nullValue;
        if (style.wordSpacing.Equals(nullValue))
            style.wordSpacing = nullValue;

        style.transitionProperty.value?.Clear();
        style.transitionDuration.value?.Clear();
        style.transitionDelay.value?.Clear();
    }
}