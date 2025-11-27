using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate)]
public class ComposableAttribute : Attribute
{
}