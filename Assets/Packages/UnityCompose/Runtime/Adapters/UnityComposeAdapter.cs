using System;
using System.Collections.Generic;
using Compose.Net;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Nodes;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters;

public class UnityComposeAdapter : IComposeAdapter
{
    public static readonly UnityComposeAdapter Instance = new();

    private UnityComposeAdapter()
    {
    }

    public IDisposable StartTimeCoroutine(IEnumerable<TimeSpan?> coroutine) =>
        ComposeInvalidatorHolder.StartCoroutineAsDisposable(coroutine);

    public int Framerate()
    {
        var frameRate = Application.targetFrameRate;
        if (frameRate < 0)
            frameRate = Screen.currentResolution.refreshRateRatio.value.ToFloat().ToInt();
        return frameRate;
    }

    public ComposeInvalidator? ComposeInvalidatorInstance => ComposeInvalidatorHolder.ComposeInvalidator;
    public IModifiersFactory ModifiersFactory { get; } = new ModifiersFactoryImpl();
    public IReusableNodeFactory NodeFactory { get; } = new ReusableNodeFactoryImpl();
    public IComposeLogger Logger { get; } = new ComposeLoggerImpl();
    public IEnterTransitionsFactory EnterTransitionsFactory { get; } = new EnterTransitionsFactoryImpl();
    public IExitTransitionsFactory ExitTransitionsFactory { get; } = new ExitTransitionsFactoryImpl();
}