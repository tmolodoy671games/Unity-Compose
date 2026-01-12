using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeNavigator
{
    void ApplyCommands(IEnumerable<ComposeNavigationCommand> commands);
}