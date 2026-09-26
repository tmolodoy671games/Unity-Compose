using System;
using System.Collections.Generic;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters;

internal class UnityComposeAdapter : IComposeAdapter
{
    public static readonly UnityComposeAdapter Instance = new();

    private UnityComposeAdapter()
    {
    }

    public IDisposable StartTimeCoroutine(IEnumerable<TimeSpan?> coroutine) =>
        ComposeInvalidatorHolder.StartCoroutine(coroutine);

    public ComposeInvalidator ComposeInvalidatorInstance => ComposeInvalidatorHolder.ComposeInvalidator;
    public IModifiersFactory ModifiersFactory { get; } = new ModifiersFactoryImpl();
    public IReusableNodeFactory NodeFactory { get; } = new ReusableNodeFactoryImpl();
    public IComposeLogger Logger { get; } = new ComposeLoggerImpl();
    public IEnterTransitionsFactory EnterTransitionsFactory { get; } = new EnterTransitionsFactoryImpl();
    public IExitTransitionsFactory ExitTransitionsFactory { get; }
}