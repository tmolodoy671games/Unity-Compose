using System;
using System.Collections.Generic;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;

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
    public IModifiersFactory ModifiersFactory { get; }
    public IReusableNodeFactory NodeFactory { get; } = new ReusableNodeFactoryImpl();
}