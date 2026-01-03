// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract partial class ComposeScreen
{
    public record ScreenTransitions(
        ContentTransform Enter,
        ContentTransform Exit
    )
    {
        public static ScreenTransitions Empty { get; } = new(
            Enter: ContentTransform.Instant,
            Exit: ContentTransform.Instant
        );
    }

    public virtual string ScreenKey => GetType().FullName!;
    public virtual int Priority => 0;
    public virtual ScreenTransitions? Transitions => null;

    public abstract void Content(IModifier modifier);

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return ScreenKey.Equals(((ComposeScreen)obj).ScreenKey);
    }

    public override int GetHashCode()
    {
        return ScreenKey.GetHashCode();
    }

    public override string ToString()
    {
        return ScreenKey;
    }
}