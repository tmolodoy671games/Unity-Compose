using System;

namespace UnityCompose.Packages.UnityCompose.Runtime.Api.Models;

public readonly struct AlwaysInvalidState : IEquatable<AlwaysInvalidState>
{
    public bool Equals(AlwaysInvalidState other) => false;
    public override bool Equals(object? obj) => false;
    public override int GetHashCode() => 0;
}