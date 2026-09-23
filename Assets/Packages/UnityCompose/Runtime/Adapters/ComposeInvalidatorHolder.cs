using System;
using System.Collections;
using System.Collections.Generic;
using Compose.Net;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters;

internal class ComposeInvalidatorHolder : MonoBehaviour
{
    private static ComposeInvalidatorHolder? _instance;
    private readonly ComposeInvalidator _invalidator = new();

    private static ComposeInvalidatorHolder Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("Coroutine Runner").AddComponent<ComposeInvalidatorHolder>();
            return _instance;
        }
    }
    
    public static ComposeInvalidator ComposeInvalidator => Instance._invalidator;

    private void Update()
    {
        _invalidator.Tick();
    }

    public static IDisposable StartCoroutine(IEnumerable<TimeSpan?> coroutine)
    {
        var coroutineHandle = Instance.StartCoroutine(CoroutineAdapter(coroutine));
        return new CustomDisposable(() => Instance.StopCoroutine(coroutineHandle));
    }

    private static IEnumerator CoroutineAdapter(IEnumerable<TimeSpan?> coroutine)
    {
        foreach (var step in coroutine)
        {
            if (step == null)
                yield return null;
            else
                yield return new WaitForSeconds(step.Value.TotalSeconds.ToFloat());
        }
    }
}