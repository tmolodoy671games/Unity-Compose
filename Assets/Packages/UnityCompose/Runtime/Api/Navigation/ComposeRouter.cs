using System.Linq;
using SharpExtensions;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public class ComposeRouter
{
    public readonly ComposeCommandBuffer CommandBuffer = new();

    public void NavigateTo(ComposeScreen screen)
    {
        ExecuteCommands(new ComposeNavigationCommand.Forward(screen));
    }

    public void NewRootScreen(ComposeScreen screen)
    {
        ExecuteCommands(new ComposeNavigationCommand.BackTo(null), new ComposeNavigationCommand.Forward(screen));
    }

    public void ReplaceScreen(ComposeScreen screen)
    {
        ExecuteCommands(new ComposeNavigationCommand.Replace(screen));
    }

    public void BackTo(ComposeScreen? screen)
    {
        ExecuteCommands(new ComposeNavigationCommand.BackTo(screen));
    }

    public void NewChain(params ComposeScreen[] screens)
    {
        var commands = screens
            .Select<ComposeScreen, ComposeNavigationCommand>(it => new ComposeNavigationCommand.Forward(it)
            ).ToArray();
        ExecuteCommands(commands);
    }

    public void NewRootChain(params ComposeScreen[] screens)
    {
        var commands = screens
            .Select<ComposeScreen, ComposeNavigationCommand>(static (screen, index) =>
                index == 0
                    ? new ComposeNavigationCommand.Forward(screen)
                    : new ComposeNavigationCommand.BackTo(null)
            )
            .ToArray();
        ExecuteCommands(commands);
    }

    public void FinishChain()
    {
        ExecuteCommands(new ComposeNavigationCommand.BackTo(null), new ComposeNavigationCommand.Back());
    }

    public void Exit()
    {
        ExecuteCommands(new ComposeNavigationCommand.Back());
    }

    private void ExecuteCommands(params ComposeNavigationCommand[] commands)
    {
        CommandBuffer.ExecuteCommands(ImmutableStableListOf(commands));
    }
}