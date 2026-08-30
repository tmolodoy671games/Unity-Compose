// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier DrawBefore(
        this IModifier modifier,
        ComposableContent drawBefore
    )
    {
        return modifier + new DrawBeforeModifierImpl(drawBefore);
    }

    public static IModifier DrawAfter(
        this IModifier modifier,
        ComposableContent drawAfter
    )
    {
        return modifier + new DrawAfterModifierImpl(drawAfter);
    }
}

internal partial class DrawBeforeModifierImpl : BaseComposableModifier<DrawBeforeModifierImpl>
{
    private readonly ComposableContent _content;

    public DrawBeforeModifierImpl(ComposableContent content)
    {
        _content = content;
    }

    [Composable]
    public override void DrawBefore() => _content();
    protected override bool Equals(DrawBeforeModifierImpl other) => _content == other._content;
}

internal partial class DrawAfterModifierImpl : BaseComposableModifier<DrawAfterModifierImpl>
{
    private readonly ComposableContent _content;

    public DrawAfterModifierImpl(ComposableContent content)
    {
        _content = content;
    }

    [Composable]
    public override void DrawAfter() => _content();
    protected override bool Equals(DrawAfterModifierImpl other) => _content == other._content;
}