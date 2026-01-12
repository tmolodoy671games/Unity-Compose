// // ReSharper disable CheckNamespace
//
// using System;
// using SharpExtensions;
// using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// namespace UnityCompose;
//
// public struct ComposeModifier : IEquatable<ComposeModifier>
// {
//     // private struct Builder
//     // {
//     //     internal bool _hasValue;
//     //     internal long _mask1;
//     //     internal long _mask2;
//     //     internal Alignment.Horizontal _horizontalAlignment;
//     //     internal Alignment.Vertical _verticalAlignment;
//     //
//     //     internal LayoutLength _top;
//     //     internal LayoutLength _bottom;
//     //     internal LayoutLength _left;
//     //     internal LayoutLength _right;
//     //     internal float _weight;
//     //
//     //     internal float _alpha;
//     //     internal ComposeTransition _alphaTransition;
//     //
//     //     internal Color _backgroundColor;
//     //     internal ComposeTransition _backgroundTransition;
//     //     internal Background _background;
//     //
//     //     internal LayoutLength _topLeftBorderRadius;
//     //     internal ComposeTransition _topLeftBorderRadiusTransition;
//     //     internal LayoutLength _topRightBorderRadius;
//     //     internal ComposeTransition _topRightBorderRadiusTransition;
//     //     internal LayoutLength _bottomRightBorderRadius;
//     //     internal ComposeTransition _bottomRightBorderRadiusTransition;
//     //     internal LayoutLength _bottomLeftBorderRadius;
//     //     internal ComposeTransition _bottomLeftBorderRadiusTransition;
//     //
//     //     internal float _topBorderWidth;
//     //     internal ComposeTransition _topBorderWidthTransition;
//     //     internal float _bottomBorderWidth;
//     //     internal ComposeTransition _bottomBorderWidthTransition;
//     //     internal float _leftBorderWidth;
//     //     internal ComposeTransition _leftBorderWidthTransition;
//     //     internal float _rightBorderWidth;
//     //     internal ComposeTransition _rightBorderWidthTransition;
//     //
//     //     internal Color _topBorderColor;
//     //     internal ComposeTransition _topBorderColorTransition;
//     //     internal Color _bottomBorderColor;
//     //     internal ComposeTransition _bottomBorderColorTransition;
//     //     internal Color _leftBorderColor;
//     //     internal ComposeTransition _leftBorderColorTransition;
//     //     internal Color _rightBorderColor;
//     //     internal ComposeTransition _rightBorderColorTransition;
//     //
//     //     internal LayoutLength _topMargin;
//     //     internal ComposeTransition _topMarginTransition;
//     //     internal LayoutLength _bottomMargin;
//     //     internal ComposeTransition _bottomMarginTransition;
//     //     internal LayoutLength _leftMargin;
//     //     internal ComposeTransition _leftMarginTransition;
//     //     internal LayoutLength _rightMargin;
//     //     internal ComposeTransition _rightMarginTransition;
//     //
//     //     internal LayoutLength _topPadding;
//     //     internal ComposeTransition _topPaddingTransition;
//     //     internal LayoutLength _bottomPadding;
//     //     internal ComposeTransition _bottomPaddingTransition;
//     //     internal LayoutLength _leftPadding;
//     //     internal ComposeTransition _leftPaddingTransition;
//     //     internal LayoutLength _rightPadding;
//     //     internal ComposeTransition _rightPaddingTransition;
//     //
//     //     internal float _widthFraction;
//     //     internal float _heightFraction;
//     //     internal LayoutLength _minWidth;
//     //     internal LayoutLength _maxWidth;
//     //     internal LayoutLength _minHeight;
//     //     internal LayoutLength _maxHeight;
//     //     internal LayoutLength _height;
//     //     internal LayoutLength _width;
//     //
//     //     internal (LayoutLength X, LayoutLength Y) _offset;
//     //     internal float _rotation;
//     //     internal ComposeTransition _rotationTransition;
//     //     internal Vector2 _scale;
//     //     internal ComposeTransition _scaleTransition;
//     //     internal (LayoutLength X, LayoutLength Y) _transformOrigin;
//     //
//     //     internal IModifier? _customModifier;
//     //
//     //     public Builder(ComposeModifier modifier)
//     //     {
//     //         _hasValue = modifier._hasValue;
//     //         _mask1 = modifier._mask1;
//     //         _mask2 = modifier._mask2;
//     //         _horizontalAlignment = modifier._horizontalAlignment;
//     //         _verticalAlignment = modifier._verticalAlignment;
//     //
//     //         _top = modifier._top;
//     //         _bottom = modifier._bottom;
//     //         _left = modifier._left;
//     //         _right = modifier._right;
//     //         _weight = modifier._weight;
//     //
//     //         _alpha = modifier._alpha;
//     //         _alphaTransition = modifier._alphaTransition;
//     //
//     //         _backgroundColor = modifier._backgroundColor;
//     //         _backgroundTransition = modifier._backgroundTransition;
//     //         _background = modifier._background;
//     //
//     //         _topLeftBorderRadius = modifier._topLeftBorderRadius;
//     //         _topLeftBorderRadiusTransition = modifier._topLeftBorderRadiusTransition;
//     //         _topRightBorderRadius = modifier._topRightBorderRadius;
//     //         _topRightBorderRadiusTransition = modifier._topRightBorderRadiusTransition;
//     //         _bottomLeftBorderRadius = modifier._bottomLeftBorderRadius;
//     //         _bottomLeftBorderRadiusTransition = modifier._bottomLeftBorderRadiusTransition;
//     //         _bottomRightBorderRadius = modifier._bottomRightBorderRadius;
//     //         _bottomRightBorderRadiusTransition = modifier._bottomRightBorderRadiusTransition;
//     //
//     //         _topBorderWidth = modifier._topBorderWidth;
//     //         _topBorderWidthTransition = modifier._topBorderWidthTransition;
//     //         _bottomBorderWidth = modifier._bottomBorderWidth;
//     //         _bottomBorderWidthTransition = modifier._bottomBorderWidthTransition;
//     //         _leftBorderWidth = modifier._leftBorderWidth;
//     //         _leftBorderWidthTransition = modifier._leftBorderWidthTransition;
//     //         _rightBorderWidth = modifier._rightBorderWidth;
//     //         _rightBorderWidthTransition = modifier._rightBorderWidthTransition;
//     //
//     //         _topBorderColor = modifier._topBorderColor;
//     //         _topBorderColorTransition = modifier._topBorderColorTransition;
//     //         _bottomBorderColor = modifier._bottomBorderColor;
//     //         _bottomBorderColorTransition = modifier._bottomBorderColorTransition;
//     //         _leftBorderColor = modifier._leftBorderColor;
//     //         _leftBorderColorTransition = modifier._leftBorderColorTransition;
//     //         _rightBorderColor = modifier._rightBorderColor;
//     //         _rightBorderColorTransition = modifier._rightBorderColorTransition;
//     //
//     //         _topMargin = modifier._topMargin;
//     //         _topMarginTransition = modifier._topMarginTransition;
//     //         _bottomMargin = modifier._bottomMargin;
//     //         _bottomMarginTransition = modifier._bottomMarginTransition;
//     //         _leftMargin = modifier._leftMargin;
//     //         _leftMarginTransition = modifier._leftMarginTransition;
//     //         _rightMargin = modifier._rightMargin;
//     //         _rightMarginTransition = modifier._rightMarginTransition;
//     //
//     //         _topPadding = modifier._topPadding;
//     //         _topPaddingTransition = modifier._topPaddingTransition;
//     //         _bottomPadding = modifier._bottomPadding;
//     //         _bottomPaddingTransition = modifier._bottomPaddingTransition;
//     //         _leftPadding = modifier._leftPadding;
//     //         _leftPaddingTransition = modifier._leftPaddingTransition;
//     //         _rightPadding = modifier._rightPadding;
//     //         _rightPaddingTransition = modifier._rightPaddingTransition;
//     //
//     //         _widthFraction = modifier._widthFraction;
//     //         _heightFraction = modifier._heightFraction;
//     //         _minWidth = modifier._minWidth;
//     //         _maxWidth = modifier._maxWidth;
//     //         _minHeight = modifier._minHeight;
//     //         _maxHeight = modifier._maxHeight;
//     //         _height = modifier._height;
//     //         _width = modifier._width;
//     //
//     //         _offset = modifier._offset;
//     //         _rotation = modifier._rotation;
//     //         _rotationTransition = modifier._rotationTransition;
//     //         _scale = modifier._scale;
//     //         _scaleTransition = modifier._scaleTransition;
//     //         _transformOrigin = modifier._transformOrigin;
//     //
//     //         _customModifier = modifier._customModifier;
//     //     }
//     //
//     //     public ComposeModifier Build()
//     //     {
//     //         return new ComposeModifier();
//     //     }
//     //
//     //     public void AddMask(ModifierMask1 mask1)
//     //     {
//     //         _mask1 = mask1 &&;
//     //     }
//     // }
//
//     private bool _hasValue;
//     private long _mask1;
//     private long _mask2;
//     private Alignment.Horizontal _horizontalAlignment;
//     private Alignment.Vertical _verticalAlignment;
//
//     private LayoutLength _top;
//     private LayoutLength _bottom;
//     private LayoutLength _left;
//     private LayoutLength _right;
//     private float _weight;
//
//     private float _alpha;
//     private ComposeTransition _alphaTransition;
//
//     private Color _backgroundColor;
//     private ComposeTransition _backgroundTransition;
//     private Background _background;
//
//     private LayoutLength _topLeftBorderRadius;
//     private ComposeTransition _topLeftBorderRadiusTransition;
//     private LayoutLength _topRightBorderRadius;
//     private ComposeTransition _topRightBorderRadiusTransition;
//     private LayoutLength _bottomRightBorderRadius;
//     private ComposeTransition _bottomRightBorderRadiusTransition;
//     private LayoutLength _bottomLeftBorderRadius;
//     private ComposeTransition _bottomLeftBorderRadiusTransition;
//
//     private float _topBorderWidth;
//     private ComposeTransition _topBorderWidthTransition;
//     private float _bottomBorderWidth;
//     private ComposeTransition _bottomBorderWidthTransition;
//     private float _leftBorderWidth;
//     private ComposeTransition _leftBorderWidthTransition;
//     private float _rightBorderWidth;
//     private ComposeTransition _rightBorderWidthTransition;
//
//     private Color _topBorderColor;
//     private ComposeTransition _topBorderColorTransition;
//     private Color _bottomBorderColor;
//     private ComposeTransition _bottomBorderColorTransition;
//     private Color _leftBorderColor;
//     private ComposeTransition _leftBorderColorTransition;
//     private Color _rightBorderColor;
//     private ComposeTransition _rightBorderColorTransition;
//
//     private LayoutLength _topMargin;
//     private ComposeTransition _topMarginTransition;
//     private LayoutLength _bottomMargin;
//     private ComposeTransition _bottomMarginTransition;
//     private LayoutLength _leftMargin;
//     private ComposeTransition _leftMarginTransition;
//     private LayoutLength _rightMargin;
//     private ComposeTransition _rightMarginTransition;
//
//     private LayoutLength _topPadding;
//     private ComposeTransition _topPaddingTransition;
//     private LayoutLength _bottomPadding;
//     private ComposeTransition _bottomPaddingTransition;
//     private LayoutLength _leftPadding;
//     private ComposeTransition _leftPaddingTransition;
//     private LayoutLength _rightPadding;
//     private ComposeTransition _rightPaddingTransition;
//
//     private float _widthFraction;
//     private float _heightFraction;
//     private LayoutLength _minWidth;
//     private LayoutLength _maxWidth;
//     private LayoutLength _minHeight;
//     private LayoutLength _maxHeight;
//     private LayoutLength _height;
//     private LayoutLength _width;
//
//     private (LayoutLength X, LayoutLength Y) _offset;
//     private float _rotation;
//     private ComposeTransition _rotationTransition;
//     private Vector2 _scale;
//     private ComposeTransition _scaleTransition;
//     private (LayoutLength X, LayoutLength Y) _transformOrigin;
//
//     private IModifier? _customModifier;
//
//     public void Apply(VisualElement element)
//     {
//         if (!_hasValue)
//             return;
//         if (_mask1 == 0)
//             return;
//         if (Contains(ModifierMask1.HorizontalAlignment))
//             element.ApplyHorizontalAlignment(_horizontalAlignment);
//         if (Contains(ModifierMask1.Top))
//             element.style.top = _top;
//         if (Contains(ModifierMask1.Bottom))
//             element.style.top = _bottom;
//         if (Contains(ModifierMask1.Left))
//             element.style.top = _left;
//         if (Contains(ModifierMask1.Right))
//             element.style.top = _right;
//         if (Contains(ModifierMask1.VerticalAlignment))
//             element.ApplyVerticalAlignment(_verticalAlignment);
//         if (Contains(ModifierMask1.Weight))
//             element.style.flexGrow = _weight;
//         if (Contains(ModifierMask1.Alpha))
//             element.style.opacity = _alpha;
//         if (Contains(ModifierMask1.AlphaTransition))
//             element.AddTransition(_alphaTransition, "opacity");
//         if (Contains(ModifierMask1.BackgroundColor))
//             element.style.backgroundColor = _backgroundColor;
//         if (Contains(ModifierMask1.BackgroundColorTransition))
//             element.AddTransition(_backgroundTransition, "background-color");
//         if (Contains(ModifierMask1.Background))
//             element.style.backgroundImage = _background;
//
//         if (Contains(ModifierMask1.TopLeftBorderRadius))
//             element.style.borderTopLeftRadius = _topLeftBorderRadius;
//         if (Contains(ModifierMask1.TopLeftBorderRadiusTransition))
//             element.AddTransition(_topLeftBorderRadiusTransition, "border-top-left-radius");
//         if (Contains(ModifierMask1.TopRightBorderRadius))
//             element.style.borderTopRightRadius = _topRightBorderRadius;
//         if (Contains(ModifierMask1.TopRightBorderRadiusTransition))
//             element.AddTransition(_topRightBorderRadiusTransition, "border-top-right-radius");
//         if (Contains(ModifierMask1.BottomLeftBorderRadius))
//             element.style.borderBottomLeftRadius = _bottomLeftBorderRadius;
//         if (Contains(ModifierMask1.BottomLeftBorderRadiusTransition))
//             element.AddTransition(_bottomLeftBorderRadiusTransition, "border-bottom-left-radius");
//         if (Contains(ModifierMask1.BottomRightBorderRadius))
//             element.style.borderBottomRightRadius = _bottomRightBorderRadius;
//         if (Contains(ModifierMask1.BottomRightBorderRadiusTransition))
//             element.AddTransition(_bottomRightBorderRadiusTransition, "border-bottom-right-radius");
//
//         if (Contains(ModifierMask1.TopBorderWidth))
//             element.style.borderTopWidth = _topBorderWidth;
//         if (Contains(ModifierMask1.TopBorderWidthTransition))
//             element.AddTransition(_topBorderWidthTransition, "border-top-width");
//         if (Contains(ModifierMask1.BottomBorderWidth))
//             element.style.borderBottomWidth = _bottomBorderWidth;
//         if (Contains(ModifierMask1.BottomBorderWidthTransition))
//             element.AddTransition(_bottomBorderWidthTransition, "border-bottom-width");
//         if (Contains(ModifierMask1.LeftBorderWidth))
//             element.style.borderLeftWidth = _leftBorderWidth;
//         if (Contains(ModifierMask1.LeftBorderWidthTransition))
//             element.AddTransition(_leftBorderWidthTransition, "border-left-width");
//         if (Contains(ModifierMask1.RightBorderWidth))
//             element.style.borderRightWidth = _rightBorderWidth;
//         if (Contains(ModifierMask1.RightBorderWidthTransition))
//             element.AddTransition(_rightBorderWidthTransition, "border-right-width");
//
//         if (Contains(ModifierMask1.TopBorderColor))
//             element.style.borderTopColor = _topBorderColor;
//         if (Contains(ModifierMask1.TopBorderColorTransition))
//             element.AddTransition(_topBorderColorTransition, "border-top-color");
//         if (Contains(ModifierMask1.BottomBorderColor))
//             element.style.borderBottomColor = _bottomBorderColor;
//         if (Contains(ModifierMask1.BottomBorderColorTransition))
//             element.AddTransition(_bottomBorderColorTransition, "border-bottom-color");
//         if (Contains(ModifierMask1.LeftBorderColor))
//             element.style.borderLeftColor = _leftBorderColor;
//         if (Contains(ModifierMask1.LeftBorderColorTransition))
//             element.AddTransition(_leftBorderColorTransition, "border-left-color");
//         if (Contains(ModifierMask1.RightBorderColor))
//             element.style.borderRightColor = _rightBorderColor;
//         if (Contains(ModifierMask1.RightBorderColorTransition))
//             element.AddTransition(_rightBorderColorTransition, "border-right-color");
//
//         if (Contains(ModifierMask1.Clip))
//             element.style.overflow = Overflow.Hidden;
//         if (Contains(ModifierMask1.Float))
//             element.style.position = UnityEngine.UIElements.Position.Absolute;
//
//         if (Contains(ModifierMask1.MarginTop))
//             element.style.marginTop = _topMargin;
//         if (Contains(ModifierMask1.MarginTopTransition))
//             element.AddTransition(_topMarginTransition, "margin-top");
//         if (Contains(ModifierMask1.MarginBottom))
//             element.style.marginBottom = _bottomMargin;
//         if (Contains(ModifierMask1.MarginBottomTransition))
//             element.AddTransition(_bottomMarginTransition, "margin-bottom");
//         if (Contains(ModifierMask1.MarginLeft))
//             element.style.marginLeft = _leftMargin;
//         if (Contains(ModifierMask1.MarginLeftTransition))
//             element.AddTransition(_leftMarginTransition, "margin-left");
//         if (Contains(ModifierMask1.MarginRight))
//             element.style.marginRight = _rightMargin;
//         if (Contains(ModifierMask1.MarginRightTransition))
//             element.AddTransition(_rightMarginTransition, "margin-right");
//
//         if (Contains(ModifierMask1.PaddingTop))
//             element.style.paddingTop = _topPadding;
//         if (Contains(ModifierMask1.PaddingTopTransition))
//             element.AddTransition(_topPaddingTransition, "padding-top");
//         if (Contains(ModifierMask1.PaddingBottom))
//             element.style.paddingBottom = _bottomPadding;
//         if (Contains(ModifierMask1.PaddingBottomTransition))
//             element.AddTransition(_bottomPaddingTransition, "padding-bottom");
//         if (Contains(ModifierMask1.PaddingLeft))
//             element.style.paddingLeft = _leftPadding;
//         if (Contains(ModifierMask1.PaddingLeftTransition))
//             element.AddTransition(_leftPaddingTransition, "padding-left");
//         if (Contains(ModifierMask1.PaddingRight))
//             element.style.paddingRight = _rightPadding;
//         if (Contains(ModifierMask1.PaddingRightTransition))
//             element.AddTransition(_rightPaddingTransition, "padding-right");
//
//         if (Contains(ModifierMask1.WidthFraction))
//             element.style.width = (_widthFraction * 100).Percent().ToLength();
//         if (Contains(ModifierMask1.HeightFraction))
//             element.style.height = (_heightFraction * 100).Percent().ToLength();
//         if (Contains(ModifierMask1.MinWidth))
//             element.style.minWidth = _minWidth;
//         if (Contains(ModifierMask1.MaxWidth))
//             element.style.maxWidth = _maxWidth;
//         if (Contains(ModifierMask1.MinHeight))
//             element.style.minHeight = _minHeight;
//         if (Contains(ModifierMask1.MaxHeight))
//             element.style.maxHeight = _maxHeight;
//         if (Contains(ModifierMask1.Width))
//             element.style.width = _width;
//         if (Contains(ModifierMask1.Height))
//             element.style.height = _height;
//
//         if (Contains(ModifierMask1.Offset))
//             element.style.translate = new Translate(_offset.X, _offset.Y);
//         if (Contains(ModifierMask2.Rotation))
//             element.style.rotate = new Rotate(new Angle(_rotation, AngleUnit.Degree));
//         if (Contains(ModifierMask2.RotationTransition))
//             element.AddTransition(_rotationTransition, "rotate");
//         if (Contains(ModifierMask2.Scale))
//             element.style.scale = _scale;
//         if (Contains(ModifierMask2.ScaleTransition))
//             element.AddTransition(_scaleTransition, "scale");
//         if (Contains(ModifierMask2.TransformOrigin))
//             element.style.transformOrigin = new TransformOrigin(_transformOrigin.X, _transformOrigin.Y);
//
//         if (Contains(ModifierMask2.CustomModifier))
//             _customModifier?.Apply(element);
//     }
//
//     public void Revert(VisualElement element)
//     {
//         if (!_hasValue)
//             return;
//         if (_mask1 == 0)
//             return;
//         if (Contains(ModifierMask1.HorizontalAlignment))
//             element.RevertHorizontalAlignment();
//         if (Contains(ModifierMask1.Top))
//             element.style.top = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Bottom))
//             element.style.top = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Left))
//             element.style.top = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Right))
//             element.style.top = StyleKeyword.Null;
//         if (Contains(ModifierMask1.VerticalAlignment))
//             element.RevertVerticalAlignment();
//         if (Contains(ModifierMask1.Weight))
//             element.style.flexShrink = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Alpha))
//             element.style.opacity = StyleKeyword.Null;
//         if (Contains(ModifierMask1.AlphaTransition))
//             element.RemoveTransition("opacity");
//         if (Contains(ModifierMask1.BackgroundColor))
//             element.style.backgroundColor = StyleKeyword.Null;
//         if (Contains(ModifierMask1.BackgroundColorTransition))
//             element.RemoveTransition("background-color");
//         if (Contains(ModifierMask1.Background))
//             element.style.backgroundImage = StyleKeyword.Null;
//
//         if (Contains(ModifierMask1.TopLeftBorderRadius))
//             element.style.borderTopLeftRadius = StyleKeyword.Null;
//         if (Contains(ModifierMask1.TopLeftBorderRadiusTransition))
//             element.RemoveTransition("border-top-left-radius");
//         if (Contains(ModifierMask1.TopRightBorderRadius))
//             element.style.borderTopRightRadius = StyleKeyword.Null;
//         if (Contains(ModifierMask1.TopRightBorderRadiusTransition))
//             element.RemoveTransition("border-top-right-radius");
//         if (Contains(ModifierMask1.BottomLeftBorderRadius))
//             element.style.borderBottomLeftRadius = StyleKeyword.Null;
//         if (Contains(ModifierMask1.BottomLeftBorderRadiusTransition))
//             element.RemoveTransition("border-bottom-left-radius");
//         if (Contains(ModifierMask1.BottomRightBorderRadius))
//             element.style.borderBottomRightRadius = StyleKeyword.Null;
//         if (Contains(ModifierMask1.BottomRightBorderRadiusTransition))
//             element.RemoveTransition("border-bottom-right-radius");
//
//         if (Contains(ModifierMask1.TopBorderWidth))
//             element.style.borderTopWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.TopBorderWidthTransition))
//             element.RemoveTransition("border-top-width");
//         if (Contains(ModifierMask1.BottomBorderWidth))
//             element.style.borderBottomWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.BottomBorderWidthTransition))
//             element.RemoveTransition("border-bottom-width");
//         if (Contains(ModifierMask1.LeftBorderWidth))
//             element.style.borderLeftWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.LeftBorderWidthTransition))
//             element.RemoveTransition("border-left-width");
//         if (Contains(ModifierMask1.RightBorderWidth))
//             element.style.borderRightWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.RightBorderWidthTransition))
//             element.RemoveTransition("border-right-width");
//
//         if (Contains(ModifierMask1.TopBorderColor))
//             element.style.borderTopColor = StyleKeyword.Null;
//         if (Contains(ModifierMask1.TopBorderColorTransition))
//             element.RemoveTransition("border-top-color");
//         if (Contains(ModifierMask1.BottomBorderColor))
//             element.style.borderBottomColor = StyleKeyword.Null;
//         if (Contains(ModifierMask1.BottomBorderColorTransition))
//             element.RemoveTransition("border-bottom-color");
//         if (Contains(ModifierMask1.LeftBorderColor))
//             element.style.borderLeftColor = StyleKeyword.Null;
//         if (Contains(ModifierMask1.LeftBorderColorTransition))
//             element.RemoveTransition("border-left-color");
//         if (Contains(ModifierMask1.RightBorderColor))
//             element.style.borderRightColor = StyleKeyword.Null;
//         if (Contains(ModifierMask1.RightBorderColorTransition))
//             element.RemoveTransition("border-right-color");
//
//         if (Contains(ModifierMask1.Clip))
//             element.style.overflow = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Float))
//             element.style.position = StyleKeyword.Null;
//
//         if (Contains(ModifierMask1.MarginTop))
//             element.style.marginTop = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MarginTopTransition))
//             element.RemoveTransition("margin-top");
//         if (Contains(ModifierMask1.MarginBottom))
//             element.style.marginBottom = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MarginBottomTransition))
//             element.RemoveTransition("margin-bottom");
//         if (Contains(ModifierMask1.MarginLeft))
//             element.style.marginLeft = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MarginLeftTransition))
//             element.RemoveTransition("margin-left");
//         if (Contains(ModifierMask1.MarginRight))
//             element.style.marginRight = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MarginRightTransition))
//             element.RemoveTransition("margin-right");
//
//         if (Contains(ModifierMask1.PaddingTop))
//             element.style.paddingTop = StyleKeyword.Null;
//         if (Contains(ModifierMask1.PaddingTopTransition))
//             element.RemoveTransition("padding-top");
//         if (Contains(ModifierMask1.PaddingBottom))
//             element.style.paddingBottom = StyleKeyword.Null;
//         if (Contains(ModifierMask1.PaddingBottomTransition))
//             element.RemoveTransition("padding-bottom");
//         if (Contains(ModifierMask1.PaddingLeft))
//             element.style.paddingLeft = StyleKeyword.Null;
//         if (Contains(ModifierMask1.PaddingLeftTransition))
//             element.RemoveTransition("padding-left");
//         if (Contains(ModifierMask1.PaddingRight))
//             element.style.paddingRight = StyleKeyword.Null;
//         if (Contains(ModifierMask1.PaddingRightTransition))
//             element.RemoveTransition("padding-right");
//
//         if (Contains(ModifierMask1.WidthFraction))
//             element.style.width = StyleKeyword.Null;
//         if (Contains(ModifierMask1.HeightFraction))
//             element.style.height = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MinWidth))
//             element.style.minWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MaxWidth))
//             element.style.maxWidth = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MinHeight))
//             element.style.minHeight = StyleKeyword.Null;
//         if (Contains(ModifierMask1.MaxHeight))
//             element.style.maxHeight = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Width))
//             element.style.width = StyleKeyword.Null;
//         if (Contains(ModifierMask1.Height))
//             element.style.height = StyleKeyword.Null;
//
//         if (Contains(ModifierMask1.Offset))
//             element.style.translate = StyleKeyword.Null;
//         if (Contains(ModifierMask2.Rotation))
//             element.style.rotate = StyleKeyword.Null;
//         if (Contains(ModifierMask2.RotationTransition))
//             element.RemoveTransition("rotate");
//         if (Contains(ModifierMask2.Scale))
//             element.style.scale = StyleKeyword.Null;
//         if (Contains(ModifierMask2.ScaleTransition))
//             element.RemoveTransition("scale");
//         if (Contains(ModifierMask2.TransformOrigin))
//             element.style.transformOrigin = StyleKeyword.Null;
//
//         if (Contains(ModifierMask2.CustomModifier))
//             _customModifier?.Revert(element);
//     }
//
//
//     #region Alignment
//
//     public ComposeModifier Align(Alignment.Horizontal horizontalAlignment)
//     {
//         var newModifier = this;
//         newModifier._horizontalAlignment = horizontalAlignment;
//         newModifier.AddMask(ModifierMask1.HorizontalAlignment);
//         return newModifier;
//     }
//
//     public ComposeModifier Align(Alignment.Vertical verticalAlignment)
//     {
//         var newModifier = this;
//         newModifier._verticalAlignment = verticalAlignment;
//         newModifier.AddMask(ModifierMask1.VerticalAlignment);
//         return newModifier;
//     }
//
//     public ComposeModifier Weight(int weight)
//     {
//         var newModifier = this;
//         newModifier._weight = weight;
//         newModifier.AddMask(ModifierMask1.Weight);
//         return newModifier;
//     }
//
//     public ComposeModifier Position(
//         LayoutLength top = default,
//         LayoutLength bottom = default,
//         LayoutLength left = default,
//         LayoutLength right = default
//     )
//     {
//         var newModifier = this;
//         newModifier._top = top;
//         newModifier.SwitchMask(ModifierMask1.Top, top.HasValue);
//
//         newModifier._bottom = bottom;
//         newModifier.SwitchMask(ModifierMask1.Bottom, bottom.HasValue);
//
//         newModifier._left = left;
//         newModifier.SwitchMask(ModifierMask1.Left, left.HasValue);
//
//         newModifier._right = right;
//         newModifier.SwitchMask(ModifierMask1.Right, right.HasValue);
//         return newModifier;
//     }
//
//     #endregion
//
//     #region Appearance
//
//     public ComposeModifier Alpha(
//         float alpha,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         var newModifier = this;
//         newModifier._alpha = alpha;
//         newModifier.AddMask(ModifierMask1.Alpha);
//         if (transition.HasValue)
//             newModifier._alphaTransition = transition.Value;
//         newModifier.SwitchMask(ModifierMask1.AlphaTransition, transition.HasValue);
//         return newModifier;
//     }
//
//     public ComposeModifier Background(
//         Color color,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         var newModifier = this;
//         newModifier._backgroundColor = color;
//         newModifier.AddMask(ModifierMask1.BackgroundColor);
//         if (transition.HasValue)
//             newModifier._backgroundTransition = transition.Value;
//         newModifier.SwitchMask(ModifierMask1.BackgroundColorTransition, transition.HasValue);
//         return newModifier;
//     }
//
//     public ComposeModifier Background(Sprite image)
//     {
//         var newModifier = this;
//         newModifier._background = UnityEngine.UIElements.Background.FromSprite(image);
//         newModifier.AddMask(ModifierMask1.Background);
//         return newModifier;
//     }
//
//     public ComposeModifier Background(Texture2D image)
//     {
//         var newModifier = this;
//         newModifier._background = UnityEngine.UIElements.Background.FromTexture2D(image);
//         newModifier.AddMask(ModifierMask1.Background);
//         return newModifier;
//     }
//
//     public ComposeModifier Background(VectorImage image)
//     {
//         var newModifier = this;
//         newModifier._background = UnityEngine.UIElements.Background.FromVectorImage(image);
//         newModifier.AddMask(ModifierMask1.Background);
//         return newModifier;
//     }
//
//     public ComposeModifier Background(RenderTexture image)
//     {
//         var newModifier = this;
//         newModifier._background = UnityEngine.UIElements.Background.FromRenderTexture(image);
//         newModifier.AddMask(ModifierMask1.Background);
//         return newModifier;
//     }
//
//     private ComposeModifier Background(Background background)
//     {
//         var newModifier = this;
//         newModifier._background = background;
//         newModifier.AddMask(ModifierMask1.Background);
//         return newModifier;
//     }
//
//     public ComposeModifier Border(
//         LayoutLength radius = default,
//         LayoutLength verticalRadius = default,
//         LayoutLength horizontalRadius = default,
//         LayoutLength topLeftRadius = default,
//         LayoutLength topRightRadius = default,
//         LayoutLength bottomLeftRadius = default,
//         LayoutLength bottomRightRadius = default,
//         float width = -1,
//         float verticalWidth = -1,
//         float horizontalWidth = -1,
//         float topWidth = -1,
//         float bottomWidth = -1,
//         float leftWidth = -1,
//         float rightWidth = -1,
//         Optional<Color> color = default,
//         Optional<Color> verticalColor = default,
//         Optional<Color> horizontalColor = default,
//         Optional<Color> topColor = default,
//         Optional<Color> bottomColor = default,
//         Optional<Color> leftColor = default,
//         Optional<Color> rightColor = default,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         var newModifier = this;
//         newModifier._topLeftBorderRadius =
//             ParamUtils.Resolve(topLeftRadius, verticalRadius, horizontalRadius, radius);
//         newModifier.SwitchMask(ModifierMask1.TopLeftBorderRadius, newModifier._topLeftBorderRadius.HasValue);
//         if (transition.HasValue && newModifier._topLeftBorderRadius.HasValue)
//         {
//             newModifier._topLeftBorderRadiusTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.TopLeftBorderRadiusTransition);
//         }
//
//         newModifier._topRightBorderRadius =
//             ParamUtils.Resolve(topRightRadius, verticalRadius, horizontalRadius, radius);
//         newModifier.SwitchMask(ModifierMask1.TopRightBorderRadius, newModifier._topRightBorderRadius.HasValue);
//         if (transition.HasValue && newModifier._topRightBorderRadius.HasValue)
//         {
//             newModifier._topRightBorderRadiusTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.TopRightBorderRadiusTransition);
//         }
//
//         newModifier._bottomLeftBorderRadius =
//             ParamUtils.Resolve(bottomLeftRadius, verticalRadius, horizontalRadius, radius);
//         newModifier.SwitchMask(ModifierMask1.BottomLeftBorderRadius, newModifier._bottomLeftBorderRadius.HasValue);
//         if (transition.HasValue && newModifier._bottomLeftBorderRadius.HasValue)
//         {
//             newModifier._bottomLeftBorderRadiusTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.BottomLeftBorderRadiusTransition);
//         }
//
//         newModifier._bottomRightBorderRadius =
//             ParamUtils.Resolve(bottomRightRadius, verticalRadius, horizontalRadius, radius);
//         newModifier.SwitchMask(ModifierMask1.BottomRightBorderRadius, newModifier._bottomRightBorderRadius.HasValue);
//         if (transition.HasValue && newModifier._bottomRightBorderRadius.HasValue)
//         {
//             newModifier._bottomRightBorderRadiusTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.BottomRightBorderRadiusTransition);
//         }
//
//         newModifier._topBorderWidth = ParamUtils.Resolve(topWidth, verticalWidth, width);
//         newModifier.SwitchMask(ModifierMask1.TopBorderWidth, newModifier._topBorderWidth >= 0);
//         if (transition.HasValue && newModifier._topBorderWidth >= 0)
//         {
//             newModifier._topBorderWidthTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.TopBorderWidthTransition);
//         }
//
//         newModifier._bottomBorderWidth = ParamUtils.Resolve(bottomWidth, verticalWidth, width);
//         newModifier.SwitchMask(ModifierMask1.BottomBorderWidth, newModifier._bottomBorderWidth >= 0);
//         if (transition.HasValue && newModifier._bottomBorderWidth >= 0)
//         {
//             newModifier._bottomBorderWidthTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.BottomBorderWidthTransition);
//         }
//
//         newModifier._leftBorderWidth = ParamUtils.Resolve(leftWidth, horizontalWidth, width);
//         newModifier.SwitchMask(ModifierMask1.LeftBorderWidth, newModifier._leftBorderWidth >= 0);
//         if (transition.HasValue && newModifier._leftBorderWidth >= 0)
//         {
//             newModifier._leftBorderWidthTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.LeftBorderWidthTransition);
//         }
//
//         newModifier._rightBorderWidth = ParamUtils.Resolve(rightWidth, horizontalWidth, width);
//         newModifier.SwitchMask(ModifierMask1.RightBorderWidth, newModifier._rightBorderWidth >= 0);
//         if (transition.HasValue && newModifier._rightBorderWidth >= 0)
//         {
//             newModifier._rightBorderWidthTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.RightBorderWidthTransition);
//         }
//
//         var topColorOptional = ParamUtils.Resolve(topColor, verticalColor, color);
//         if (topColorOptional.HasValue)
//             newModifier._topBorderColor = topColorOptional.Value;
//         newModifier.SwitchMask(ModifierMask1.TopBorderColor, topColorOptional.HasValue);
//         if (transition.HasValue && topColorOptional.HasValue)
//         {
//             newModifier._topBorderColorTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.TopBorderColorTransition);
//         }
//
//         var bottomColorOptional = ParamUtils.Resolve(bottomColor, verticalColor, color);
//         if (bottomColorOptional.HasValue)
//             newModifier._bottomBorderColor = bottomColorOptional.Value;
//         newModifier.SwitchMask(ModifierMask1.BottomBorderColor, bottomColorOptional.HasValue);
//         if (transition.HasValue && bottomColorOptional.HasValue)
//         {
//             newModifier._bottomBorderColorTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.BottomBorderColorTransition);
//         }
//
//         var leftColorOptional = ParamUtils.Resolve(leftColor, horizontalColor, color);
//         if (leftColorOptional.HasValue)
//             newModifier._leftBorderColor = leftColorOptional.Value;
//         newModifier.SwitchMask(ModifierMask1.LeftBorderColor, leftColorOptional.HasValue);
//         if (transition.HasValue && leftColorOptional.HasValue)
//         {
//             newModifier._leftBorderColorTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.LeftBorderColorTransition);
//         }
//
//         var rightColorOptional = ParamUtils.Resolve(rightColor, horizontalColor, color);
//         if (rightColorOptional.HasValue)
//             newModifier._rightBorderColor = rightColorOptional.Value;
//         newModifier.SwitchMask(ModifierMask1.RightBorderColor, rightColorOptional.HasValue);
//         if (transition.HasValue && rightColorOptional.HasValue)
//         {
//             newModifier._rightBorderColorTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.RightBorderColorTransition);
//         }
//
//         return newModifier;
//     }
//
//     public ComposeModifier Clip()
//     {
//         var newModifier = this;
//         newModifier.AddMask(ModifierMask1.Clip);
//         return newModifier;
//     }
//
//     public ComposeModifier Float()
//     {
//         var newModifier = this;
//         newModifier.AddMask(ModifierMask1.Float);
//         return newModifier;
//     }
//
//     #endregion
//
//     #region Insets
//
//     public ComposeModifier Margin(
//         LayoutLength all = default,
//         LayoutLength horizontal = default,
//         LayoutLength vertical = default,
//         LayoutLength top = default,
//         LayoutLength bottom = default,
//         LayoutLength left = default,
//         LayoutLength right = default,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         var newModifier = this;
//
//         newModifier._topMargin = ParamUtils.Resolve(top, vertical, all);
//         newModifier.SwitchMask(ModifierMask1.MarginTop, newModifier._topMargin.HasValue);
//         if (transition.HasValue && newModifier._topMargin.HasValue)
//         {
//             newModifier._topMarginTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.MarginTopTransition);
//         }
//
//         newModifier._bottomMargin = ParamUtils.Resolve(bottom, vertical, all);
//         newModifier.SwitchMask(ModifierMask1.MarginBottom, newModifier._bottomMargin.HasValue);
//         if (transition.HasValue && newModifier._bottomMargin.HasValue)
//         {
//             newModifier._bottomMarginTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.MarginBottomTransition);
//         }
//
//         newModifier._leftMargin = ParamUtils.Resolve(left, horizontal, all);
//         newModifier.SwitchMask(ModifierMask1.MarginLeft, newModifier._leftMargin.HasValue);
//         if (transition.HasValue && newModifier._leftMargin.HasValue)
//         {
//             newModifier._leftMarginTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.MarginLeftTransition);
//         }
//
//         newModifier._rightMargin = ParamUtils.Resolve(right, horizontal, all);
//         newModifier.SwitchMask(ModifierMask1.MarginRight, newModifier._rightMargin.HasValue);
//         if (transition.HasValue && newModifier._rightMargin.HasValue)
//         {
//             newModifier._rightMarginTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.MarginRightTransition);
//         }
//
//         return newModifier;
//     }
//
//     public ComposeModifier Padding(
//         LayoutLength all = default,
//         LayoutLength horizontal = default,
//         LayoutLength vertical = default,
//         LayoutLength top = default,
//         LayoutLength bottom = default,
//         LayoutLength left = default,
//         LayoutLength right = default,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         var newModifier = this;
//
//         newModifier._topPadding = ParamUtils.Resolve(top, vertical, all);
//         newModifier.SwitchMask(ModifierMask1.PaddingTop, newModifier._topPadding.HasValue);
//         if (transition.HasValue && newModifier._topPadding.HasValue)
//         {
//             newModifier._topPaddingTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.PaddingTopTransition);
//         }
//
//         newModifier._bottomPadding = ParamUtils.Resolve(bottom, vertical, all);
//         newModifier.SwitchMask(ModifierMask1.PaddingBottom, newModifier._bottomPadding.HasValue);
//         if (transition.HasValue && newModifier._bottomPadding.HasValue)
//         {
//             newModifier._bottomPaddingTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.PaddingBottomTransition);
//         }
//
//         newModifier._leftPadding = ParamUtils.Resolve(left, horizontal, all);
//         newModifier.SwitchMask(ModifierMask1.PaddingLeft, newModifier._leftPadding.HasValue);
//         if (transition.HasValue && newModifier._leftPadding.HasValue)
//         {
//             newModifier._leftPaddingTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.PaddingLeftTransition);
//         }
//
//         newModifier._rightPadding = ParamUtils.Resolve(right, horizontal, all);
//         newModifier.SwitchMask(ModifierMask1.PaddingRight, newModifier._rightPadding.HasValue);
//         if (transition.HasValue && newModifier._rightPadding.HasValue)
//         {
//             newModifier._rightPaddingTransition = transition.Value;
//             newModifier.AddMask(ModifierMask1.PaddingRightTransition);
//         }
//
//         return newModifier;
//     }
//
//     #endregion
//
//     #region Size
//
//     public ComposeModifier FillMaxSize(float fraction = 1)
//     {
//         var newModifier = this;
//         newModifier._width = (fraction * 100).Percent();
//         newModifier.AddMask(ModifierMask1.Width);
//         newModifier._height = (fraction * 100).Percent();
//         newModifier.AddMask(ModifierMask1.HeightFraction);
//         return newModifier;
//     }
//
//     public ComposeModifier FillMaxWidth(float fraction = 1)
//     {
//         var newModifier = this;
//         newModifier._width = (fraction * 100).Percent();
//         newModifier.AddMask(ModifierMask1.Width);
//         return newModifier;
//     }
//
//     public ComposeModifier FillMaxHeight(float fraction = 1)
//     {
//         var newModifier = this;
//         newModifier._height = (fraction * 100).Percent();
//         newModifier.AddMask(ModifierMask1.HeightFraction);
//         return newModifier;
//     }
//
//     public ComposeModifier SizeIn(
//         LayoutLength min = default,
//         LayoutLength max = default,
//         LayoutLength minWidth = default,
//         LayoutLength maxWidth = default,
//         LayoutLength minHeight = default,
//         LayoutLength maxHeight = default
//     )
//     {
//         var newModifier = this;
//         newModifier._minWidth = ParamUtils.Resolve(minWidth, min);
//         newModifier.SwitchMask(ModifierMask1.MinWidth, newModifier._minWidth.HasValue);
//
//         newModifier._maxWidth = ParamUtils.Resolve(maxWidth, max);
//         newModifier.SwitchMask(ModifierMask1.MaxWidth, newModifier._maxWidth.HasValue);
//
//         newModifier._minHeight = ParamUtils.Resolve(minHeight, min);
//         newModifier.SwitchMask(ModifierMask1.MinHeight, newModifier._minHeight.HasValue);
//
//         newModifier._maxHeight = ParamUtils.Resolve(maxHeight, max);
//         newModifier.SwitchMask(ModifierMask1.MaxHeight, newModifier._maxHeight.HasValue);
//
//         return newModifier;
//     }
//
//     public ComposeModifier WidthIn(
//         LayoutLength min = default,
//         LayoutLength max = default
//     )
//     {
//         var newModifier = this;
//         newModifier._minWidth = min;
//         newModifier.SwitchMask(ModifierMask1.MinWidth, newModifier._minWidth.HasValue);
//
//         newModifier._maxWidth = max;
//         newModifier.SwitchMask(ModifierMask1.MaxWidth, newModifier._maxWidth.HasValue);
//
//         return newModifier;
//     }
//
//     public ComposeModifier HeightIn(
//         LayoutLength min = default,
//         LayoutLength max = default
//     )
//     {
//         var newModifier = this;
//
//         newModifier._minHeight = min;
//         newModifier.SwitchMask(ModifierMask1.MinHeight, newModifier._minHeight.HasValue);
//
//         newModifier._maxHeight = max;
//         newModifier.SwitchMask(ModifierMask1.MaxHeight, newModifier._maxHeight.HasValue);
//
//         return newModifier;
//     }
//
//     public ComposeModifier Size(
//         LayoutLength size = default,
//         LayoutLength width = default,
//         LayoutLength height = default
//     )
//     {
//         var newModifier = this;
//         newModifier._width = ParamUtils.Resolve(width, size);
//         newModifier.SwitchMask(ModifierMask1.Width, newModifier._width.HasValue);
//
//         newModifier._height = ParamUtils.Resolve(height, size);
//         newModifier.SwitchMask(ModifierMask1.Height, newModifier._height.HasValue);
//
//         return newModifier;
//     }
//
//     public ComposeModifier Width(LayoutLength width)
//     {
//         var newModifier = this;
//         newModifier._width = width;
//         newModifier.SwitchMask(ModifierMask1.Width, newModifier._width.HasValue);
//
//         return newModifier;
//     }
//
//     public ComposeModifier Height(LayoutLength height)
//     {
//         var newModifier = this;
//         newModifier._height = height;
//         newModifier.SwitchMask(ModifierMask1.Height, newModifier._height.HasValue);
//
//         return newModifier;
//     }
//
//     #endregion
//
//     #region Transform
//
//     public ComposeModifier Offset(
//         LayoutLength x = default,
//         LayoutLength y = default,
//         Optional<(LayoutLength X, LayoutLength Y)> offset = default
//     )
//     {
//         var newModifier = this;
//
//         var xLength = x.HasValue ? x : offset.HasValue ? offset.Value.X : default;
//         var yLength = y.HasValue ? y : offset.HasValue ? offset.Value.Y : default;
//         if (xLength.HasValue || yLength.HasValue)
//         {
//             var length = (xLength, yLength);
//             newModifier._offset = length;
//             newModifier.AddMask(ModifierMask1.Offset);
//         }
//
//         return newModifier;
//     }
//     
//     public ComposeModifier Rotate(
//         float degrees,
//         Optional<ComposeTransition> transition = default
//     )
//     {
//         return modifier + new RotateModifierImpl(degrees, transition);
//     }
//
//     #endregion
//
//
//     private bool Contains(ModifierMask1 mask1)
//     {
//         return (_mask1 & (long)mask1) != 0;
//     }
//
//     private bool Contains(ModifierMask2 mask2)
//     {
//         return (_mask2 & (long)mask2) != 0;
//     }
//
//     private void SwitchMask(ModifierMask1 mask1, bool enabled)
//     {
//         _mask1 &= enabled ? (long)mask1 : ~(long)mask1;
//     }
//
//     private void AddMask(ModifierMask1 mask1)
//     {
//         _mask1 &= (long)mask1;
//     }
//
//     private void SwitchMask(ModifierMask2 mask2, bool enabled)
//     {
//         _mask2 &= enabled ? (long)mask2 : ~(long)mask2;
//     }
//
//     private void AddMask(ModifierMask2 mask2)
//     {
//         _mask2 &= (long)mask2;
//     }
//
//     public bool Equals(ComposeModifier other)
//     {
//         return _hasValue == other._hasValue && _mask1 == other._mask1 && _mask2 == other._mask2 &&
//                _horizontalAlignment.Equals(other._horizontalAlignment) &&
//                _verticalAlignment.Equals(other._verticalAlignment) && _top.Equals(other._top) &&
//                _bottom.Equals(other._bottom) && _left.Equals(other._left) && _right.Equals(other._right) &&
//                _weight.Equals(other._weight) && _alpha.Equals(other._alpha) &&
//                _alphaTransition.Equals(other._alphaTransition) && _backgroundColor.Equals(other._backgroundColor) &&
//                _backgroundTransition.Equals(other._backgroundTransition) && _background.Equals(other._background) &&
//                _topLeftBorderRadius.Equals(other._topLeftBorderRadius) &&
//                _topLeftBorderRadiusTransition.Equals(other._topLeftBorderRadiusTransition) &&
//                _topRightBorderRadius.Equals(other._topRightBorderRadius) &&
//                _topRightBorderRadiusTransition.Equals(other._topRightBorderRadiusTransition) &&
//                _bottomRightBorderRadius.Equals(other._bottomRightBorderRadius) &&
//                _bottomRightBorderRadiusTransition.Equals(other._bottomRightBorderRadiusTransition) &&
//                _bottomLeftBorderRadius.Equals(other._bottomLeftBorderRadius) &&
//                _bottomLeftBorderRadiusTransition.Equals(other._bottomLeftBorderRadiusTransition) &&
//                _topBorderWidth.Equals(other._topBorderWidth) &&
//                _topBorderWidthTransition.Equals(other._topBorderWidthTransition) &&
//                _bottomBorderWidth.Equals(other._bottomBorderWidth) &&
//                _bottomBorderWidthTransition.Equals(other._bottomBorderWidthTransition) &&
//                _leftBorderWidth.Equals(other._leftBorderWidth) &&
//                _leftBorderWidthTransition.Equals(other._leftBorderWidthTransition) &&
//                _rightBorderWidth.Equals(other._rightBorderWidth) &&
//                _rightBorderWidthTransition.Equals(other._rightBorderWidthTransition) &&
//                _topBorderColor.Equals(other._topBorderColor) &&
//                _topBorderColorTransition.Equals(other._topBorderColorTransition) &&
//                _bottomBorderColor.Equals(other._bottomBorderColor) &&
//                _bottomBorderColorTransition.Equals(other._bottomBorderColorTransition) &&
//                _leftBorderColor.Equals(other._leftBorderColor) &&
//                _leftBorderColorTransition.Equals(other._leftBorderColorTransition) &&
//                _rightBorderColor.Equals(other._rightBorderColor) &&
//                _rightBorderColorTransition.Equals(other._rightBorderColorTransition) &&
//                _topMargin.Equals(other._topMargin) && _topMarginTransition.Equals(other._topMarginTransition) &&
//                _bottomMargin.Equals(other._bottomMargin) &&
//                _bottomMarginTransition.Equals(other._bottomMarginTransition) && _leftMargin.Equals(other._leftMargin) &&
//                _leftMarginTransition.Equals(other._leftMarginTransition) && _rightMargin.Equals(other._rightMargin) &&
//                _rightMarginTransition.Equals(other._rightMarginTransition) && _topPadding.Equals(other._topPadding) &&
//                _topPaddingTransition.Equals(other._topPaddingTransition) &&
//                _bottomPadding.Equals(other._bottomPadding) &&
//                _bottomPaddingTransition.Equals(other._bottomPaddingTransition) &&
//                _leftPadding.Equals(other._leftPadding) && _leftPaddingTransition.Equals(other._leftPaddingTransition) &&
//                _rightPadding.Equals(other._rightPadding) &&
//                _rightPaddingTransition.Equals(other._rightPaddingTransition) &&
//                _widthFraction.Equals(other._widthFraction) && _heightFraction.Equals(other._heightFraction) &&
//                _minWidth.Equals(other._minWidth) && _maxWidth.Equals(other._maxWidth) &&
//                _minHeight.Equals(other._minHeight) && _maxHeight.Equals(other._maxHeight) &&
//                _height.Equals(other._height) && _width.Equals(other._width) && _offset.Equals(other._offset) &&
//                _rotation.Equals(other._rotation) && _rotationTransition.Equals(other._rotationTransition) &&
//                _scale.Equals(other._scale) && _scaleTransition.Equals(other._scaleTransition) &&
//                _transformOrigin.Equals(other._transformOrigin) && Equals(_customModifier, other._customModifier);
//     }
//
//     public override bool Equals(object? obj)
//     {
//         return obj is ComposeModifier other && Equals(other);
//     }
//
//     public override int GetHashCode()
//     {
//         var hashCode = new HashCode();
//         hashCode.Add(_hasValue);
//         hashCode.Add(_mask1);
//         hashCode.Add(_mask2);
//         hashCode.Add(_horizontalAlignment);
//         hashCode.Add(_verticalAlignment);
//         hashCode.Add(_top);
//         hashCode.Add(_bottom);
//         hashCode.Add(_left);
//         hashCode.Add(_right);
//         hashCode.Add(_weight);
//         hashCode.Add(_alpha);
//         hashCode.Add(_alphaTransition);
//         hashCode.Add(_backgroundColor);
//         hashCode.Add(_backgroundTransition);
//         hashCode.Add(_background);
//         hashCode.Add(_topLeftBorderRadius);
//         hashCode.Add(_topLeftBorderRadiusTransition);
//         hashCode.Add(_topRightBorderRadius);
//         hashCode.Add(_topRightBorderRadiusTransition);
//         hashCode.Add(_bottomRightBorderRadius);
//         hashCode.Add(_bottomRightBorderRadiusTransition);
//         hashCode.Add(_bottomLeftBorderRadius);
//         hashCode.Add(_bottomLeftBorderRadiusTransition);
//         hashCode.Add(_topBorderWidth);
//         hashCode.Add(_topBorderWidthTransition);
//         hashCode.Add(_bottomBorderWidth);
//         hashCode.Add(_bottomBorderWidthTransition);
//         hashCode.Add(_leftBorderWidth);
//         hashCode.Add(_leftBorderWidthTransition);
//         hashCode.Add(_rightBorderWidth);
//         hashCode.Add(_rightBorderWidthTransition);
//         hashCode.Add(_topBorderColor);
//         hashCode.Add(_topBorderColorTransition);
//         hashCode.Add(_bottomBorderColor);
//         hashCode.Add(_bottomBorderColorTransition);
//         hashCode.Add(_leftBorderColor);
//         hashCode.Add(_leftBorderColorTransition);
//         hashCode.Add(_rightBorderColor);
//         hashCode.Add(_rightBorderColorTransition);
//         hashCode.Add(_topMargin);
//         hashCode.Add(_topMarginTransition);
//         hashCode.Add(_bottomMargin);
//         hashCode.Add(_bottomMarginTransition);
//         hashCode.Add(_leftMargin);
//         hashCode.Add(_leftMarginTransition);
//         hashCode.Add(_rightMargin);
//         hashCode.Add(_rightMarginTransition);
//         hashCode.Add(_topPadding);
//         hashCode.Add(_topPaddingTransition);
//         hashCode.Add(_bottomPadding);
//         hashCode.Add(_bottomPaddingTransition);
//         hashCode.Add(_leftPadding);
//         hashCode.Add(_leftPaddingTransition);
//         hashCode.Add(_rightPadding);
//         hashCode.Add(_rightPaddingTransition);
//         hashCode.Add(_widthFraction);
//         hashCode.Add(_heightFraction);
//         hashCode.Add(_minWidth);
//         hashCode.Add(_maxWidth);
//         hashCode.Add(_minHeight);
//         hashCode.Add(_maxHeight);
//         hashCode.Add(_height);
//         hashCode.Add(_width);
//         hashCode.Add(_offset);
//         hashCode.Add(_rotation);
//         hashCode.Add(_rotationTransition);
//         hashCode.Add(_scale);
//         hashCode.Add(_scaleTransition);
//         hashCode.Add(_transformOrigin);
//         hashCode.Add(_customModifier);
//         return hashCode.ToHashCode();
//     }
// }
//
// [Flags]
// internal enum ModifierMask1 : long
// {
//     HorizontalAlignment = 1 << 0,
//     Top = 1 << 1,
//     Bottom = 1 << 2,
//     Left = 1 << 3,
//     Right = 1 << 4,
//     VerticalAlignment = 1 << 5,
//     Weight = 1 << 6,
//     Alpha = 1 << 7,
//     AlphaTransition = 1 << 8,
//     BackgroundColor = 1 << 9,
//     BackgroundColorTransition = 1 << 10,
//     Background = 1 << 11,
//     TopLeftBorderRadius = 1 << 12,
//     TopLeftBorderRadiusTransition = 1 << 13,
//     TopRightBorderRadius = 1 << 14,
//     TopRightBorderRadiusTransition = 1 << 15,
//     BottomLeftBorderRadius = 1 << 16,
//     BottomLeftBorderRadiusTransition = 1 << 17,
//     BottomRightBorderRadius = 1 << 18,
//     BottomRightBorderRadiusTransition = 1 << 19,
//     TopBorderWidth = 1 << 20,
//     TopBorderWidthTransition = 1 << 21,
//     BottomBorderWidth = 1 << 22,
//     BottomBorderWidthTransition = 1 << 23,
//     LeftBorderWidth = 1 << 24,
//     LeftBorderWidthTransition = 1 << 25,
//     RightBorderWidth = 1 << 26,
//     RightBorderWidthTransition = 1 << 27,
//     TopBorderColor = 1 << 28,
//     TopBorderColorTransition = 1 << 29,
//     BottomBorderColor = 1 << 30,
//     BottomBorderColorTransition = 1 << 31,
//     LeftBorderColor = 1L << 32,
//     LeftBorderColorTransition = 1L << 33,
//     RightBorderColor = 1L << 34,
//     RightBorderColorTransition = 1L << 35,
//     Clip = 1L << 36,
//     Float = 1L << 37,
//     MarginTop = 1L << 38,
//     MarginTopTransition = 1L << 39,
//     MarginBottom = 1L << 40,
//     MarginBottomTransition = 1L << 41,
//     MarginLeft = 1L << 42,
//     MarginLeftTransition = 1L << 43,
//     MarginRight = 1L << 44,
//     MarginRightTransition = 1L << 45,
//     PaddingTop = 1L << 46,
//     PaddingTopTransition = 1L << 47,
//     PaddingBottom = 1L << 48,
//     PaddingBottomTransition = 1L << 49,
//     PaddingLeft = 1L << 50,
//     PaddingLeftTransition = 1L << 51,
//     PaddingRight = 1L << 52,
//     PaddingRightTransition = 1L << 53,
//     WidthFraction = 1L << 54,
//     HeightFraction = 1L << 55,
//     MinWidth = 1L << 56,
//     MaxWidth = 1L << 57,
//     MinHeight = 1L << 58,
//     MaxHeight = 1L << 59,
//     Width = 1L << 60,
//     Height = 1L << 61,
//     Offset = 1L << 62,
// }
//
// [Flags]
// internal enum ModifierMask2 : long
// {
//     Rotation = 1L << 0,
//     RotationTransition = 1L << 1,
//     Scale = 1L << 2,
//     ScaleTransition = 1L << 3,
//     TransformOrigin = 1L << 4,
//     CustomModifier = 1L << 5,
// }