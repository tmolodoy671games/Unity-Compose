// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public static class ComposeDefaults
{
    public const float TransitionDuration = 0.15f;
    public static readonly AnimationCurve DefaultCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
}