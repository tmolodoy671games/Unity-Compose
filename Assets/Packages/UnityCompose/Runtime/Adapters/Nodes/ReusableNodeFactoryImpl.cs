using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

internal class ReusableNodeFactoryImpl : IReusableNodeFactory
{
    public IColumnNodeFactory Column { get; } = new ColumnNodeFactoryImpl();
    public IRowNodeFactory Row { get; } = new RowNodeFactoryImpl();
    public IBoxNodeFactory Box { get; } = new BoxNodeFactoryImpl();
    public ISpacerNodeFactory Spacer { get; } = new SpacerNodeFactoryImpl();
    public IImageNodeFactory Image { get; }
    public ITextNodeFactory Text { get; } = new TextNodeFactoryImpl();
}