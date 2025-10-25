using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public static class ProgressExtensions
{
    public static float RemapProgress(this float progress, float startOffset = 0f, float speed = 1f, float endOffset = 0f)
    {
        progress = Mathf.Clamp01(progress);
        startOffset = Mathf.Clamp01(startOffset);
        endOffset = Mathf.Clamp01(endOffset);
        speed = Mathf.Max(0f, speed);

        if (progress <= startOffset) return 0f;
        if (progress >= 1f - endOffset) return 1f;

        var range = 1f - startOffset - endOffset;
        if (range <= 0f) return progress <= startOffset ? 0f : 1f;

        var normalized = (progress - startOffset) / range;
        return Mathf.Clamp01(normalized * speed);
    }
}