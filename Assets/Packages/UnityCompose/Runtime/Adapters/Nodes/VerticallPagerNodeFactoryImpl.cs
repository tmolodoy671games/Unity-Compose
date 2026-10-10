using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class VerticalPagerNodeFactoryImpl : IVerticalPagerNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new VerticalPager());
    }

    public void Apply(
        IReusableComposeNode node,
        Action<Action<float>> subscribeToValue,
        Action<Action<float>> unsubscribeFromValue,
        PaddingValues contentPadding
    )
    {
        node.VisualElement<VerticalPager>().Init(subscribeToValue, unsubscribeFromValue, contentPadding);
    }
}

internal class VerticalPager : VisualElement
{
    private readonly Content _content;

    public VerticalPager()
    {
        style.alignItems = Alignment.TopCenter.ToAlign();
        style.justifyContent = Alignment.TopCenter.ToJustify();
        _content = new Content();
        hierarchy.Add(_content);

        _content.style.flexDirection = FlexDirection.Column;
        _content.style.flexShrink = 0;
        _content.style.width = new Length(100, LengthUnit.Percent);
    }

    public void Init(
        Action<Action<float>> subscribeToValue,
        Action<Action<float>> unsubscribeFromValue,
        PaddingValues contentPadding
    )
    {
        subscribeToValue(OnValueChange);
        _content.RegisterCallbackOnce<DetachFromPanelEvent>(_ =>
        {
            unsubscribeFromValue(OnValueChange);
        });
        _content.style.paddingLeft = contentPadding.Left.Value;
        _content.style.paddingRight = contentPadding.Right.Value;
    }

    public override VisualElement contentContainer => _content;

    private void OnValueChange(float value) => _content.style.translate = new Translate(0, -value);
}