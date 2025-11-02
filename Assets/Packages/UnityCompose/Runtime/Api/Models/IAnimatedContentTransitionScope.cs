// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IAnimatedContentTransitionScope<T>
{
    T InitialState { get; }
    T TargetState { get; }
}

internal class AnimatedContentTransitionScopeImpl<T> : IAnimatedContentTransitionScope<T>
{
    public AnimatedContentTransitionScopeImpl(T initialState, T targetState)
    {
        InitialState = initialState;
        TargetState = targetState;
    }

    public T InitialState { get; }
    public T TargetState { get; }
}