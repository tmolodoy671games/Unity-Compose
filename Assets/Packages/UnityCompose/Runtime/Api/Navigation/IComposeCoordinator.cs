// ReSharper disable CheckNamespace

using StableCollections;

namespace UnityCompose;

public interface IComposeCoordinator
{
    private class MockComposeCoordinator : BaseComposeCoordinator
    {
    }

    public static readonly IComposeCoordinator Mock = new MockComposeCoordinator();

    IImmutableStableList<ComposeScreen> InitialScreens();

    ComposeCommandBuffer CommandBuffer { get; }

    void GoBack();
}

public abstract class BaseComposeCoordinator : IComposeCoordinator
{
    protected readonly ComposeRouter Router = new();

    public virtual IImmutableStableList<ComposeScreen> InitialScreens() => IImmutableStableList.Empty<ComposeScreen>();

    public ComposeCommandBuffer CommandBuffer => Router.CommandBuffer;

    public void GoBack()
    {
        Router.Exit();
    }
}