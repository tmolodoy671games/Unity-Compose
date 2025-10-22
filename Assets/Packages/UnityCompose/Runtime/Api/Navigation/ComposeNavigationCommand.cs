// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract record ComposeNavigationCommand
{
    public record Forward(ComposeScreen Screen) : ComposeNavigationCommand;
    public record Replace(ComposeScreen Screen) : ComposeNavigationCommand;

    public record Back : ComposeNavigationCommand;
    public record BackTo(ComposeScreen? Screen) : ComposeNavigationCommand;
}