using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class ComposeGroupData
{
    public object? ObjectKey;
    public IComposeGroupState? PreviousState;
    public readonly ComposeGroupRestartScope RestartScope;
    public CompositionLocalMap? CompositionLocalMap;
    public VisualElement? Element;

    public ComposeGroupData(SlotWriter writer)
    {
        RestartScope = new ComposeGroupRestartScope(writer);
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