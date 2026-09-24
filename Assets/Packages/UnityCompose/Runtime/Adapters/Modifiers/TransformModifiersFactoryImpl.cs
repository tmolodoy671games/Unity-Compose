using Compose.Net;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class TransformModifiersFactoryImpl : ITransformModifiersFactory
{
    public IModifier Offset(Dp x, Dp y) => new OffsetModifierImpl(x, y);
    public IModifier Rotate(float degrees) => new RotateModifierImpl(degrees);
    public IModifier Scale(float scaleX, float scaleY) => new ScaleModifierImpl(scaleX, scaleY);
    public IModifier TransformOrigin(Dp originX, Dp originY) => new TransformOriginModifierImpl(originX, originY);
}