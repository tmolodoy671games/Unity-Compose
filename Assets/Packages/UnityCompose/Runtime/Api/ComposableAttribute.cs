using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
                AttributeTargets.Field)]
public class ComposableAttribute : Attribute
{
}