using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposeUI : MonoBehaviour
{
    [Composable]
    protected abstract void __Content()
    {
        if (CurrentComposer.BeginComposeGroup(string.Empty))
            return;
        try
        {
        }
        finally
        {
            CurrentComposer.EndComposeGroup(static () => __Content());
        }
    };
    [Composable]
    private void __ContentImpl()
    {
        if (CurrentComposer.BeginComposeGroup(string.Empty))
            return;
        try
        {
            if (!ApplicationUtils.IsPlaying)
                return;
            Content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(static () => __ContentImpl());
        }
    }

    [Composable]
    protected virtual void __Preview()
    {
        if (CurrentComposer.BeginComposeGroup(string.Empty))
            return;
        try
        {
        }
        finally
        {
            CurrentComposer.EndComposeGroup(static () => __Preview());
        }
    }
}