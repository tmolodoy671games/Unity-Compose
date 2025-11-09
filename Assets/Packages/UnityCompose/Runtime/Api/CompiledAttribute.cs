// ReSharper disable CheckNamespace

using System;

namespace UnityCompose;

[AttributeUsage(AttributeTargets.Method)]
public class CompiledAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class DontGenerateComposeGroupsAttribute : Attribute
{
}