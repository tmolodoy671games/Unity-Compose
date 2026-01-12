using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class ComposeCommandBuffer
{
    private IComposeNavigator? _navigator;

    private readonly IMutableStableList<IImmutableStableList<ComposeNavigationCommand>> _pendingCommands =
        IMutableStableList.Create<IImmutableStableList<ComposeNavigationCommand>>();

    public void SetNavigator(IComposeNavigator navigator)
    {
        _navigator = navigator;
        foreach (var pendingCommandGroup in _pendingCommands)
            _navigator.ApplyCommands(pendingCommandGroup);
        _pendingCommands.Clear();
    }

    public void RemoveNavigator() => _navigator = null;

    public void ExecuteCommands(IEnumerable<ComposeNavigationCommand> commands)
    {
        if (_navigator != null)
            _navigator.ApplyCommands(commands);
        else
            _pendingCommands.Add(commands.ToImmutableStableList());
    }
}