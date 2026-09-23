using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class ColumnNodeFactoryImpl : IColumnNodeFactory
{
    public IReusableComposeNode CreateNode()
    {
        return new UnityReusableComposeNode(new Column());
    }

    public void Apply(
        IReusableComposeNode node,
        Alignment.Horizontal horizontalAlignment,
        Arrangement.Vertical verticalArrangement
    )
    {
        var column = node.VisualElement<Column>();
        column.style.alignItems = horizontalAlignment.ToAlign();
        column.style.justifyContent = verticalArrangement.ToJustify();
    }
}