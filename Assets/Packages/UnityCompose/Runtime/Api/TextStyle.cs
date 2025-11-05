using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public readonly record struct TextStyle(
    float FontSize,
    Optional<Color> Color = default,
    FontWeight FontWeight = FontWeight.Normal, 
    FontStyle FontStyle = FontStyle.Normal
);