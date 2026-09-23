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
}