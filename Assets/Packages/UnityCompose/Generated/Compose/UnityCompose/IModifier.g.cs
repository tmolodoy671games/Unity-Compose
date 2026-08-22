#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using StableCollections;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using SharpExtensions;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public partial interface IModifier
{
    IModifier __Compose(global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        return this;
    }
}