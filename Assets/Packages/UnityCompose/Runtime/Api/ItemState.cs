// ReSharper disable CheckNamespace
namespace UnityCompose;

public enum ItemState
{
    Idle,
    Hovered,
    Selected,
    Pressed,
    Disabled,
}

public static class ItemStates
{
    public static ItemState Get(bool isHovered, bool isPressed, bool isSelected = false, bool isEnabled = true)
    {
        if (!isEnabled) return ItemState.Disabled;
        if (isPressed) return ItemState.Pressed;
        if (isHovered) return ItemState.Hovered;
        if (isSelected) return ItemState.Selected;
        return ItemState.Idle;
    }
}