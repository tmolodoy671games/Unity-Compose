namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal readonly record struct RememberId(
    string FileName,
    int LineNumber,
    object? AdditionalKey = null,
    int Increment = 0
);