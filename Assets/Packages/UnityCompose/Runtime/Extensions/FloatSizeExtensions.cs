// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine;

namespace UnityCompose;

public static class FloatSizeExtensions
{
    public static Vector2 ToVector2(this FloatSize floatSize) => new(floatSize.Width, floatSize.Height);
}