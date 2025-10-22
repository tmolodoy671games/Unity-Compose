using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public readonly record struct TextStyle(
    Color Color,
    float FontSize,
    FontStyle FontStyle = FontStyle.Normal
);