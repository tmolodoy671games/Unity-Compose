using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class ComposeStyleProperty
{
    internal readonly IReadOnlyList<string> Properties;

    public static readonly ComposeStyleProperty PaddingLeft = new("padding-left");
    public static readonly ComposeStyleProperty PaddingRight = new("padding-right");
    public static readonly ComposeStyleProperty PaddingTop = new("padding-top");
    public static readonly ComposeStyleProperty PaddingBottom = new("padding-bottom");
    public static readonly ComposeStyleProperty PaddingHorizontal = new("padding-left", "padding-right");
    public static readonly ComposeStyleProperty PaddingVertical = new("padding-top", "padding-bottom");

    public static readonly ComposeStyleProperty Padding = new(
        "padding-left",
        "padding-right",
        "padding-top",
        "padding-bottom"
    );

    public static readonly ComposeStyleProperty MarginLeft = new("margin-left");
    public static readonly ComposeStyleProperty MarginRight = new("margin-right");
    public static readonly ComposeStyleProperty MarginTop = new("margin-top");
    public static readonly ComposeStyleProperty MarginBottom = new("margin-bottom");
    public static readonly ComposeStyleProperty MarginHorizontal = new("margin-left", "margin-right");
    public static readonly ComposeStyleProperty MarginVertical = new("margin-top", "margin-bottom");

    public static readonly ComposeStyleProperty Margin = new(
        "margin-left",
        "margin-right",
        "margin-top",
        "margin-bottom"
    );
        
    public static readonly ComposeStyleProperty MinWidth = new("min-width");
    public static readonly ComposeStyleProperty MinHeight = new("min-height");
    public static readonly ComposeStyleProperty MinSize = new("min-width", "min-height");
    public static readonly ComposeStyleProperty MaxWidth = new("max-width");
    public static readonly ComposeStyleProperty MaxHeight = new("max-height");
    public static readonly ComposeStyleProperty MaxSize = new("max-width", "max-height");

    public static readonly ComposeStyleProperty Visibility = new("visibility");
    public static readonly ComposeStyleProperty Display = new("display");
    public static readonly ComposeStyleProperty Opacity = new("opacity");
    public static readonly ComposeStyleProperty Scale = new("scale");

    public static readonly ComposeStyleProperty BackgroundColor = new("background-color");
    public static readonly ComposeStyleProperty BackgroundPositionX = new("background-position-x");
    public static readonly ComposeStyleProperty BackgroundPositionY = new("background-position-y");
    public static readonly ComposeStyleProperty BackgroundRepeat = new("background-repeat");
    public static readonly ComposeStyleProperty BackgroundSize = new("background-size");
    public static readonly ComposeStyleProperty BackgroundImageTintColor = new("unity-background-image-tint-color");

    public static readonly ComposeStyleProperty UnityBackgroundImageTintColor =
        new("unity-background-image-tint-color");

    public static readonly ComposeStyleProperty BorderLeftWidth = new("border-left-width");
    public static readonly ComposeStyleProperty BorderRightWidth = new("border-right-width");
    public static readonly ComposeStyleProperty BorderTopWidth = new("border-top-width");
    public static readonly ComposeStyleProperty BorderBottomWidth = new("border-bottom-width");

    public static readonly ComposeStyleProperty BorderWidth = new(
        "border-left-width", "border-right-width", "border-top-width", "border-bottom-width"
    );

    public static readonly ComposeStyleProperty BorderTopColor = new("border-top-color");
    public static readonly ComposeStyleProperty BorderBottomColor = new("border-bottom-color");
    public static readonly ComposeStyleProperty BorderLeftColor = new("border-left-color");
    public static readonly ComposeStyleProperty BorderRightColor = new("border-right-color");

    public static readonly ComposeStyleProperty BorderColor = new(
        "border-top-color", "border-bottom-color", "border-left-color", "border-right-color"
    );

    public static readonly ComposeStyleProperty BorderTopLeftRadius = new("border-top-left-radius");
    public static readonly ComposeStyleProperty BorderTopRightRadius = new("border-top-right-radius");
    public static readonly ComposeStyleProperty BorderBottomLeftRadius = new("border-bottom-left-radius");
    public static readonly ComposeStyleProperty BorderBottomRightRadius = new("border-bottom-right-radius");

    public static readonly ComposeStyleProperty BorderRadius = new(
        "border-top-left-radius",
        "border-top-right-radius",
        "border-bottom-left-radius",
        "border-bottom-right-radius"
    );

    public static readonly ComposeStyleProperty TextColor = new("-unity-text-color");

    public static readonly ComposeStyleProperty FlexGrow = new("flex-grow");
    public static readonly ComposeStyleProperty FlexShrink = new("flex-shrink");
    public static readonly ComposeStyleProperty Translate = new("translate");

    private ComposeStyleProperty(params string[] properties)
    {
        Properties = properties;
    }
}