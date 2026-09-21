using System;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[AttributeUsage(AttributeTargets.Method  | AttributeTargets.Delegate)]
public class Composable : Attribute
{
}