using Compose.Net;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

public record PageSize
{
    public static readonly PageSize Fill = new();

    public record Fixed(Dp PageSize) : PageSize;

    private PageSize()
    {
    }
}