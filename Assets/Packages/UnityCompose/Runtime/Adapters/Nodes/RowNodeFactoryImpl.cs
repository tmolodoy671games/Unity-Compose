using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class RowNodeFactoryImpl : IRowNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Row());
    }

    public void Apply(
        IReusableComposeNode node,
        Arrangement.Horizontal horizontalArrangement,
        Alignment.Vertical verticalAlignment
    )
    {
        var row = node.VisualElement<Row>();
        row.style.flexDirection = FlexDirection.Row;
        row.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
        row.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
    }
}