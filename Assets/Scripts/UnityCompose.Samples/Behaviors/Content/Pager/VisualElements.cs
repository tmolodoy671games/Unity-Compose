using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

internal class HorizontalPager : VisualElement
{
    public HorizontalPager()
    {
        style.flexDirection = FlexDirection.Row;
        style.alignItems = Alignment.CenterLeft.ToAlign();
        style.justifyContent = Alignment.CenterLeft.ToJustify();
    }
}

internal class VerticalPager : VisualElement
{
}