using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class HorizontalPagerNodeFactoryImpl : IHorizontalPagerNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new HorizontalPager());
    }

    public void Apply(
        IReusableComposeNode node,
        Action<Action<float>> subscribeToValue,
        Action<Action<float>> unsubscribeFromValue,
        PaddingValues contentPadding
    )
    {
        node.VisualElement<HorizontalPager>().Init(subscribeToValue, unsubscribeFromValue, contentPadding);
    }
}

internal class HorizontalPager : VisualElement
{
    private readonly Content _content;

    public HorizontalPager()
    {
        style.alignItems = Alignment.CenterLeft.ToAlign();
        style.justifyContent = Alignment.CenterLeft.ToJustify();
        _content = new Content();
        hierarchy.Add(_content);

        _content.style.flexDirection = FlexDirection.Row;
        _content.style.flexShrink = 0;
        _content.style.height = new Length(100, LengthUnit.Percent);
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
        _content.style.paddingTop = contentPadding.Top.Value;
        _content.style.paddingBottom = contentPadding.Bottom.Value;
    }

    public override VisualElement contentContainer => _content;

    private void OnValueChange(float value) => _content.style.translate = new Translate(-value, 0);
}