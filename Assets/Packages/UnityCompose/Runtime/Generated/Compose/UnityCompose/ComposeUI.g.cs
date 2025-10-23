using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public abstract partial class ComposeUI
{
    [Composable]
    [Compiled]
    private void __ContentImpl()
    {
        if (CurrentComposer.BeginComposeGroup(null))
            return;
        try
        {
            if (!ApplicationUtils.IsPlaying)
                return;
            Content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __ContentImpl());
        }
    }

    [Composable]
    [Compiled]
    private void __Preview()
    {
        if (CurrentComposer.BeginComposeGroup(null))
            return;
        try
        {
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Preview());
        }
    }
}