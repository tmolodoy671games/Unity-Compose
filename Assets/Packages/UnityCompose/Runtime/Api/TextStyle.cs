using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public readonly record struct TextStyle(
    Color Color,
    float FontSize,
    FontWeight FontWeight = FontWeight.Normal, 
    FontStyle FontStyle = FontStyle.Normal
);