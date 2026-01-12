// ReSharper disable CheckNamespace

using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions {

    public static IModifier Composed(this IModifier modifier, ComposableFunc<IModifier> body)
    {
        return modifier + new ComposedModifierImpl(body);
    }
}

internal class ComposedModifierImpl : BaseModifier<ComposedModifierImpl>
{
    private readonly ComposableFunc<IModifier> _body;

    public ComposedModifierImpl(ComposableFunc<IModifier> body)
    {
        _body = body;
    }

    public override void Apply(VisualElement element)
    {
    }

    public override void Revert(VisualElement element)
    {
    }

    public override IModifier Compose() => _body();

    protected override bool Equals(ComposedModifierImpl other)
    {
        return _body == other._body;
    }
}