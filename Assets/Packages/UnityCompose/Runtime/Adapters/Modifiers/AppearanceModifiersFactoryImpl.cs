using System;
using System.Drawing;
using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class AppearanceModifiersFactoryImpl : IAppearanceModifiersFactory
{
    public IModifier Alpha(float alpha) => new AlphaModifierImpl(alpha);
    public IModifier Blur(float strength) => new BlurModifierImpl(strength);

    public IModifier Border(Dp borderWidth, Color borderColor)
    {
        return new BorderModifierImpl(borderWidth, borderColor.ToUnityColor());
    }

    public IModifier Clip(Optional<RoundedCornerShape> shape)
    {
        return new ClipModifierImpl(shape);
    }

    public IModifier Background(Color color)
    {
        return new BackgroundColorModifierImpl(color.ToUnityColor(), default);
    }

    public IModifier Background(IBrush brush, Optional<RoundedCornerShape> shape)
    {
        return new BackgroundBrushModifierImpl(brush, shape);
    }

    public IModifier DropShadow(RoundedCornerShape shape, Compose.Net.Shadow shadow)
    {
        return new DropShadowModifierImpl(shape, shadow);
    }

    public IModifier DrawBehind(Action<IDrawScope> onDraw)
    {
        return new DrawBehindModifierImpl(onDraw);
    }

    public IModifier DrawBehind<T>(Action<IDrawScope<T>> onDraw)
    {
        return DrawBehind(it => onDraw((IDrawScope<T>)it));
    }

    public IModifier DrawOn(Action<IDrawScope> onDraw)
    {
        return new DrawOnModifierImpl(onDraw);
    }

    public IModifier DrawOn<T>(Action<IDrawScope<T>> onDraw)
    {
        return DrawOn(it => onDraw((IDrawScope<T>)it));
    }
}