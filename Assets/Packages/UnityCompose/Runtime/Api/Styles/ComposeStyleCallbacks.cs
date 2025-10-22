using System;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class OnClickImpl : ComposeStyle<OnClickImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnClickImpl other)
        {
            return _onClick == other._onClick;
        }
    }

    private class OnMouseEnterImpl : ComposeStyle<OnMouseEnterImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnMouseEnterImpl other)
        {
            return _onMouseEnter == other._onMouseEnter;
        }
    }

    private class OnMouseLeaveImpl : ComposeStyle<OnMouseLeaveImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnMouseLeaveImpl other)
        {
            return _onMouseLeave == other._onMouseLeave;
        }
    }

    private class OnMouseDownImpl : ComposeStyle<OnMouseDownImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnMouseDownImpl other)
        {
            return _onMouseDown == other._onMouseDown;
        }
    }

    private class OnMouseUpImpl : ComposeStyle<OnMouseUpImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnMouseUpImpl other)
        {
            return _onMouseUp == other._onMouseUp;
        }
    }

    private class OnMouseMoveImpl : ComposeStyle<OnMouseMoveImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnMouseMoveImpl other)
        {
            return _onMouseMove == other._onMouseMove;
        }
    }

    private class OnGeometryChangedImpl : ComposeStyle<OnGeometryChangedImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(OnGeometryChangedImpl other)
        {
            return _onGeometryChanged == other._onGeometryChanged;
        }
    }

    public static ComposeStyle OnClick(this ComposeStyle style, Action onClick, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnClickImpl(_ => onClick()));
    }

    public static ComposeStyle OnClick(this ComposeStyle style, Action<ClickEvent> onClick, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnClickImpl(onClick));
    }

    public static ComposeStyle OnMouseEnter(this ComposeStyle style, Action onMouseEnter, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseEnterImpl(_ => onMouseEnter()));
    }

    public static ComposeStyle OnMouseEnter(this ComposeStyle style, Action<MouseEnterEvent> onMouseEnter,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseEnterImpl(onMouseEnter));
    }

    public static ComposeStyle OnMouseLeave(this ComposeStyle style, Action onMouseLeave, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseLeaveImpl(_ => onMouseLeave()));
    }

    public static ComposeStyle OnMouseLeave(this ComposeStyle style, Action<MouseLeaveEvent> onMouseLeave,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseLeaveImpl(onMouseLeave));
    }

    public static ComposeStyle OnMouseDown(this ComposeStyle style, Action onMouseDown, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseDownImpl(_ => onMouseDown()));
    }

    public static ComposeStyle OnMouseDown(this ComposeStyle style, Action<MouseDownEvent> onMouseDown,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseDownImpl(onMouseDown));
    }

    public static ComposeStyle OnLmbDown(this ComposeStyle style, Action onLmbDown, bool enabled = true)
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
    
    public static ComposeStyle OnLmbDown(this ComposeStyle style, Action<MouseDownEvent> onLmbDown, bool enabled = true)
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
    
    public static ComposeStyle OnRmbDown(this ComposeStyle style, Action onRmbDown, bool enabled = true)
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
    
    public static ComposeStyle OnRmbDown(this ComposeStyle style, Action<MouseDownEvent> onRmbDown, bool enabled = true)
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
    
    public static ComposeStyle OnMmbDown(this ComposeStyle style, Action onMmbDown, bool enabled = true)
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
    
    public static ComposeStyle OnMmbDown(this ComposeStyle style, Action<MouseDownEvent> onMmbDown, bool enabled = true)
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

    public static ComposeStyle OnMouseUp(this ComposeStyle style, Action onMouseUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseUpImpl(_ => onMouseUp()));
    }

    public static ComposeStyle OnMouseUp(this ComposeStyle style, Action<MouseUpEvent> onMouseUp, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseUpImpl(onMouseUp));
    }

    public static ComposeStyle OnLmbUp(this ComposeStyle style, Action onLmbUp, bool enabled = true)
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
    
    public static ComposeStyle OnLmbUp(this ComposeStyle style, Action<MouseUpEvent> onLmbUp, bool enabled = true)
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
    
    public static ComposeStyle OnRmbUp(this ComposeStyle style, Action onRmbUp, bool enabled = true)
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
    
    public static ComposeStyle OnRmbUp(this ComposeStyle style, Action<MouseUpEvent> onRmbUp, bool enabled = true)
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
    
    public static ComposeStyle OnMmbUp(this ComposeStyle style, Action onMmbUp, bool enabled = true)
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
    
    public static ComposeStyle OnMmbDown(this ComposeStyle style, Action<MouseUpEvent> onMmbUp, bool enabled = true)
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

    public static ComposeStyle OnMouseMove(this ComposeStyle style, Action onMouseMove, bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseMoveImpl(_ => onMouseMove()));
    }

    public static ComposeStyle OnMouseMove(this ComposeStyle style, Action<MouseMoveEvent> onMouseMove,
        bool enabled = true)
    {
        if (!enabled)
            return style;
        return style.Then(new OnMouseMoveImpl(onMouseMove));
    }

    public static ComposeStyle OnGeometryChanged(
        this ComposeStyle style,
        Action<GeometryChangedEvent> onGeometryChanged,
        bool enabled = true
    )
    {
        if (!enabled)
            return style;
        return style.Then(new OnGeometryChangedImpl(onGeometryChanged));
    }

    internal static ComposeStyle OnSizeChanged(this ComposeStyle style, Action<Vector2> onSizeChanged)
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