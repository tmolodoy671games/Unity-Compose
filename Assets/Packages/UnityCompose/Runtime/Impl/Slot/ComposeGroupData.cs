using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal abstract class ComposeGroupData
{
    public object? ObjectKey;
    public readonly ComposeGroupRestartScope RestartScope;
    public CompositionLocalMap? CompositionLocalMap;
    public VisualElement? Element;

    protected ComposeGroupData(SlotWriter writer)
    {
        RestartScope = new ComposeGroupRestartScope(writer);
    }
}

internal class ComposeGroupData<T> : ComposeGroupData
{
    public Optional<T> PreviousState;

    public ComposeGroupData(SlotWriter writer, T initialState) : base(writer)
    {
        PreviousState = initialState;
    }

    public override string ToString()
    {
        var builder = new StringBuilder("ComposeGroupData(");
        builder.Append($"ObjectKey = {ObjectKey}, ");
        builder.Append($"PreviousState = {PreviousState}");
        builder.Append($"RestartScope = {RestartScope.Restart != null}");
        builder.Append($"CompositionLocalMap = {CompositionLocalMap}, ");
        builder.Append($"Element = {Element?.Format()}");
        builder.Append(")");
        return builder.ToString();
    }
}