using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

internal class HorizontalPager : VisualElement
{
    private readonly UnityCompose.Content _content;

    public HorizontalPager()
    {
        style.alignItems = Alignment.CenterLeft.ToAlign();
        style.justifyContent = Alignment.CenterLeft.ToJustify();
        _content = new UnityCompose.Content();
        hierarchy.Add(_content);
        
        _content.style.flexDirection = FlexDirection.Row;
        _content.style.flexShrink = 0;
        _content.style.height = new Length(100, LengthUnit.Percent);
    }

    public void Init(
        IMutablePagerState pagerState,
        PaddingValues contentPadding
    )
    {
        pagerState.SubscribeToValueChange(OnValueChange );
        _content.RegisterCallbackOnce<DetachFromPanelEvent>(_ =>
        {
            pagerState.UnsubscribeToValueChange(OnValueChange);
        });
        _content.style.paddingTop = contentPadding.Top.Value;
        _content.style.paddingBottom = contentPadding.Bottom.Value;
    }

    public override VisualElement contentContainer => _content;
    
    private void OnValueChange(float value) => _content.style.translate = new Translate(-value, 0);
}

internal class VerticalPager : VisualElement
{
    private readonly UnityCompose.Content _content;

    public VerticalPager()
    {
        style.alignItems = Alignment.TopCenter.ToAlign();
        style.justifyContent = Alignment.TopCenter.ToJustify();
        _content = new UnityCompose.Content();
        hierarchy.Add(_content);
        
        _content.style.flexDirection = FlexDirection.Column;
        _content.style.flexShrink = 0;
        _content.style.width = new Length(100, LengthUnit.Percent);
    }

    public void Init(
        IMutablePagerState pagerState,
        PaddingValues contentPadding
    )
    {
        pagerState.SubscribeToValueChange(OnValueChange );
        _content.RegisterCallbackOnce<DetachFromPanelEvent>(_ =>
        {
            pagerState.UnsubscribeToValueChange(OnValueChange);
        });
        _content.style.paddingLeft = contentPadding.Left.Value;
        _content.style.paddingRight = contentPadding.Right.Value;
    }

    public override VisualElement contentContainer => _content;
    
    private void OnValueChange(float value) => _content.style.translate = new Translate(0, -value);
}