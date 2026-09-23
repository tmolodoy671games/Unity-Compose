// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine;

namespace UnityCompose;

public static class OffsetExtensions
{
    public static Vector2 ToVector2(this Offset offset) => new(offset.X, offset.Y);
}