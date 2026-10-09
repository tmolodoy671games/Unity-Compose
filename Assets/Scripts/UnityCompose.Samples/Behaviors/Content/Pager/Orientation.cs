using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.Content.Pager;

public enum Orientation
{
    Horizontal,
    Vertical,
}

internal static class OrientationExtensions
{
    public static FlexDirection ToFlexDirection(this Orientation orientation, bool reverseLayout)
    {
        return orientation switch
        {
            Orientation.Horizontal => reverseLayout ? FlexDirection.RowReverse : FlexDirection.Row,
            Orientation.Vertical => reverseLayout ? FlexDirection.ColumnReverse : FlexDirection.Column,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
    }

    public static float ToPadding(this Orientation orientation, bool reverseLayout, PaddingValues paddingValues)
    {
        switch (orientation)
        {
            case Orientation.Horizontal:
                return reverseLayout ? paddingValues.Right.Value : paddingValues.Left.Value;
                break;
            case Orientation.Vertical:
                return reverseLayout ? paddingValues.Bottom.Value : paddingValues.Top.Value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
        }
    }
}