using System;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ModifierExtensions
{
    private class OnClickImpl : BaseModifier<OnClickImpl>
    {
        private readonly Action<ClickEvent> _onClick;

        public OnClickImpl(Action<ClickEvent> onClick)
        {
            _onClick = onClick;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<ClickEvent>().Add(_onClick);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnClickImpl other)
        {
            return _onClick == other._onClick;
        }
    }

    private class OnMouseEnterImpl : BaseModifier<OnMouseEnterImpl>
    {
        private readonly Action<MouseEnterEvent> _onMouseEnter;

        public OnMouseEnterImpl(Action<MouseEnterEvent> onMouseEnter)
        {
            _onMouseEnter = onMouseEnter;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<MouseEnterEvent>().Add(_onMouseEnter);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnMouseEnterImpl other)
        {
            return _onMouseEnter == other._onMouseEnter;
        }
    }

    private class OnMouseLeaveImpl : BaseModifier<OnMouseLeaveImpl>
    {
        private readonly Action<MouseLeaveEvent> _onMouseLeave;

        public OnMouseLeaveImpl(Action<MouseLeaveEvent> onMouseLeave)
        {
            _onMouseLeave = onMouseLeave;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<MouseLeaveEvent>().Add(_onMouseLeave);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnMouseLeaveImpl other)
        {
            return _onMouseLeave == other._onMouseLeave;
        }
    }

    private class OnMouseDownImpl : BaseModifier<OnMouseDownImpl>
    {
        private readonly Action<MouseDownEvent> _onMouseDown;

        public OnMouseDownImpl(Action<MouseDownEvent> onMouseDown)
        {
            _onMouseDown = onMouseDown;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<MouseDownEvent>().Add(_onMouseDown);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnMouseDownImpl other)
        {
            return _onMouseDown == other._onMouseDown;
        }
    }

    private class OnMouseUpImpl : BaseModifier<OnMouseUpImpl>
    {
        private readonly Action<MouseUpEvent> _onMouseUp;

        public OnMouseUpImpl(Action<MouseUpEvent> onMouseUp)
        {
            _onMouseUp = onMouseUp;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<MouseUpEvent>().Add(_onMouseUp);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnMouseUpImpl other)
        {
            return _onMouseUp == other._onMouseUp;
        }
    }

    private class OnMouseMoveImpl : BaseModifier<OnMouseMoveImpl>
    {
        private readonly Action<MouseMoveEvent> _onMouseMove;

        public OnMouseMoveImpl(Action<MouseMoveEvent> onMouseMove)
        {
            _onMouseMove = onMouseMove;
        }

        public override void Apply(VisualElement element)
        {
            if (!IsActive)
                return;
            element.pickingMode = PickingMode.Position;
            element.GetComposeCallback<MouseMoveEvent>().Add(_onMouseMove);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnMouseMoveImpl other)
        {
            return _onMouseMove == other._onMouseMove;
        }
    }

    private class OnGeometryChangedImpl : BaseModifier<OnGeometryChangedImpl>
    {
        private readonly Action<GeometryChangedEvent> _onGeometryChanged;

        public OnGeometryChangedImpl(Action<GeometryChangedEvent> onGeometryChanged)
        {
            _onGeometryChanged = onGeometryChanged;
        }

        public override void Apply(VisualElement element)
        {
            element.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged);
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(OnGeometryChangedImpl other)
        {
            return _onGeometryChanged == other._onGeometryChanged;
        }
    }

    public static IModifier OnClick(this IModifier style, Action onClick, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnClickImpl(_ => onClick()));
    }

    public static IModifier OnClick(this IModifier style, Action<ClickEvent> onClick, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnClickImpl(onClick));
    }

    public static IModifier OnMouseEnter(this IModifier style, Action onMouseEnter, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseEnterImpl(_ => onMouseEnter()));
    }

    public static IModifier OnMouseEnter(this IModifier style, Action<MouseEnterEvent> onMouseEnter,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseEnterImpl(onMouseEnter));
    }

    public static IModifier OnMouseLeave(this IModifier style, Action onMouseLeave, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseLeaveImpl(_ => onMouseLeave()));
    }

    public static IModifier OnMouseLeave(this IModifier style, Action<MouseLeaveEvent> onMouseLeave,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseLeaveImpl(onMouseLeave));
    }

    public static IModifier OnMouseDown(this IModifier style, Action onMouseDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseDownImpl(_ => onMouseDown()));
    }

    public static IModifier OnMouseDown(this IModifier style, Action<MouseDownEvent> onMouseDown,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseDownImpl(onMouseDown));
    }

    public static IModifier OnLmbDown(this IModifier style, Action onLmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 0)
                    return;
                onLmbDown();
            })
        );
    }
    
    public static IModifier OnLmbDown(this IModifier style, Action<MouseDownEvent> onLmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 0)
                    return;
                onLmbDown(it);
            })
        );
    }
    
    public static IModifier OnRmbDown(this IModifier style, Action onRmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 1)
                    return;
                onRmbDown();
            })
        );
    }
    
    public static IModifier OnRmbDown(this IModifier style, Action<MouseDownEvent> onRmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 1)
                    return;
                onRmbDown(it);
            })
        );
    }
    
    public static IModifier OnMmbDown(this IModifier style, Action onMmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 2)
                    return;
                onMmbDown();
            })
        );
    }
    
    public static IModifier OnMmbDown(this IModifier style, Action<MouseDownEvent> onMmbDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseDownImpl(it =>
            {
                if (it.button != 2)
                    return;
                onMmbDown(it);
            })
        );
    }

    public static IModifier OnMouseUp(this IModifier style, Action onMouseUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseUpImpl(_ => onMouseUp()));
    }

    public static IModifier OnMouseUp(this IModifier style, Action<MouseUpEvent> onMouseUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseUpImpl(onMouseUp));
    }

    public static IModifier OnLmbUp(this IModifier style, Action onLmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 0)
                    return;
                onLmbUp();
            })
        );
    }
    
    public static IModifier OnLmbUp(this IModifier style, Action<MouseUpEvent> onLmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 0)
                    return;
                onLmbUp(it);
            })
        );
    }
    
    public static IModifier OnRmbUp(this IModifier style, Action onRmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 1)
                    return;
                onRmbUp();
            })
        );
    }
    
    public static IModifier OnRmbUp(this IModifier style, Action<MouseUpEvent> onRmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 1)
                    return;
                onRmbUp(it);
            })
        );
    }
    
    public static IModifier OnMmbUp(this IModifier style, Action onMmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 2)
                    return;
                onMmbUp();
            })
        );
    }
    
    public static IModifier OnMmbDown(this IModifier style, Action<MouseUpEvent> onMmbUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(
            new OnMouseUpImpl(it =>
            {
                if (it.button != 2)
                    return;
                onMmbUp(it);
            })
        );
    }

    public static IModifier OnMouseMove(this IModifier style, Action onMouseMove, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseMoveImpl(_ => onMouseMove()));
    }

    public static IModifier OnMouseMove(this IModifier style, Action<MouseMoveEvent> onMouseMove,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseMoveImpl(onMouseMove));
    }

    public static IModifier OnGeometryChanged(
        this IModifier style,
        Action<GeometryChangedEvent> onGeometryChanged,
        bool enabled = true
    )
    {
        if (!enabled)
            return style;
        return style.Then(new OnGeometryChangedImpl(onGeometryChanged));
    }

    internal static IModifier OnSizeChanged(this IModifier style, Action<Vector2> onSizeChanged)
    {
        return style.Then(new OnGeometryChangedImpl(Callback));

        void Callback(GeometryChangedEvent it)
        {
            var resolvedStyle = it.currentTarget.CastTo<VisualElement>().resolvedStyle;
            var resolvedSize = it.newRect.size;
            resolvedSize += Vector2.right * (resolvedStyle.marginLeft + resolvedStyle.marginRight);
            resolvedSize += Vector2.up * (resolvedStyle.marginTop + resolvedStyle.marginBottom);
            onSizeChanged(resolvedSize);
        }
    }
}