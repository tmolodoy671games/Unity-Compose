// ReSharper disable CheckNamespace

using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Column : VisualElement
{
}

public class Row : VisualElement
{
}

public class Box : VisualElement
{
}

public class Spacer : VisualElement
{
}

public class Text : Label
{
    public Text()
    {
        ClearClassList();
        
        style.flexBasis = StyleKeyword.Auto;
        style.flexGrow = 0;
        style.flexShrink = 0;
        style.fontSize = 14;
        
        style.marginBottom = 2;
        style.marginLeft = 2;
        style.marginRight = 4;
        style.marginTop = 4;

        style.paddingBottom = 4;
        style.paddingLeft = 1;
        style.paddingRight = 2;
        style.paddingTop = 4;
    }
}