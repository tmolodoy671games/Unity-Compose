using System;

namespace Packages.UnityCompose.Impl.Composition.Models;

public readonly struct AlwaysInvalidState : IEquatable<AlwaysInvalidState>
{
    public bool Equals(AlwaysInvalidState other) => false;
    public override bool Equals(object? obj) => false;
    public override int GetHashCode() => 0;
}