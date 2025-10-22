using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property)]
public class ComposableAttribute : Attribute
{
}