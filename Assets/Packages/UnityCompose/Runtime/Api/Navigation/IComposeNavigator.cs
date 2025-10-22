using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeNavigator
{
    void ApplyCommands(IStableList<ComposeNavigationCommand> commands);
}