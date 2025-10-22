// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeCoordinator
{
    private class MockComposeCoordinator : IComposeCoordinator
    {
        public ComposeCommandBuffer CommandBuffer { get; } = new();

        public void GoBack()
        {
        }
    }

    public static readonly IComposeCoordinator Mock = new MockComposeCoordinator();

    ComposeCommandBuffer CommandBuffer { get; }

    void GoBack();
}

public abstract class BaseComposeCoordinator : IComposeCoordinator
{
    protected readonly ComposeRouter Router = new();

    public ComposeCommandBuffer CommandBuffer => Router.CommandBuffer;

    public void GoBack()
    {
        Router.Exit();
    }
}