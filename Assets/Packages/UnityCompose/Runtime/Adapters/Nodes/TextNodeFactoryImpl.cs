using System.Drawing;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class TextNodeFactoryImpl : ITextNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Text());
    }

    public void Apply(
        IReusableComposeNode node,
        string text,
        bool softWrap,
        FontStyle fontStyle,
        FontWeight fontWeight,
        Sp fontSize,
        Color color,
        TextAlign textAlign
    )
    {
        var it = node.VisualElement<Text>();
        it.text = text;
        it.style.whiteSpace = softWrap ? WhiteSpace.Normal : WhiteSpace.NoWrap;
        it.style.unityFontStyleAndWeight =
            FontStyleUtils.ToUnityFontStyle(fontStyle, fontWeight);
        it.style.unityTextAlign = textAlign.ToTextAnchor();
        it.style.fontSize = fontSize.Value;
        it.style.color = color.ToUnityColor();
    }
}