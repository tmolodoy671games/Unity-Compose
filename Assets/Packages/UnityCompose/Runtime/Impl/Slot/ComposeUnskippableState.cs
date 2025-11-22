using System;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

public readonly struct ComposeUnskippableState : IEquatable<ComposeUnskippableState>
{
    public bool Equals(ComposeUnskippableState other) => false;
    public override bool Equals(object? obj) => false;
    public override int GetHashCode() => 0;
}