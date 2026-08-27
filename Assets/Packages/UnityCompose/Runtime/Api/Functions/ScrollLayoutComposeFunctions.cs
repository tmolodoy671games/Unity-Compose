using System;
using System.Collections;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private const float DefaultScrollMultiplier = 30;

    [Composable]
    public static ScrollState RememberScrollState(float initialValue = 0) =>
        Remember(() => new ScrollState(initialValue));

    [Composable]
    public static void ScrollableColumn(
        ComposableContent content,
        ScrollState? state = null,
        float scrollStrength = 1f,
        IModifier? modifier = null
    )
    {
        var resolvedState = state ?? Remember(() => new ScrollState(0f));
        ReusableComposeView<ScrollableColumn>(
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            },
            modifier: modifier.OrEmpty()
                .Clip()
                .OnGloballyPositioned(it => resolvedState.ViewportSize = it.Height)
                .OnVerticalScroll(
                    onVerticalScroll: it => resolvedState.AnimateScrollBy(scrollStrength * DefaultScrollMultiplier * it)
                ),
            content: () =>
            {
                ReusableComposeView<ScrollableColumnContent>(
                    initializer: it =>
                    {
                        it.style.flexDirection = FlexDirection.Column;
                        it.style.alignItems = Align.FlexStart;
                        it.style.justifyContent = Justify.FlexStart;
                    },
                    modifier: Modifier
                        .Offset(y: -resolvedState.Value.Px())
                        .OnGloballyPositioned(it => resolvedState.ContentSize = it.Height),
                    content: content
                );
            }
        );
    }

    [Composable]
    public static void ScrollableRow(
        ComposableContent content,
        ScrollState? state = null,
        float scrollStrength = 1f,
        IModifier? modifier = null
    )
    {
        var resolvedState = state ?? Remember(() => new ScrollState(0f));
        ReusableComposeView<ScrollableRow>(
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Column;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            },
            modifier: modifier.OrEmpty()
                .Clip()
                .OnGloballyPositioned(it => resolvedState.ViewportSize = it.Width)
                .OnHorizontalScroll(
                    onHorizontalScroll: it => resolvedState.AnimateScrollBy(scrollStrength * DefaultScrollMultiplier * it)
                ),
            content: () =>
            {
                ReusableComposeView<ScrollableRowContent>(
                    initializer: it =>
                    {
                        it.style.flexDirection = FlexDirection.Row;
                        it.style.alignItems = Align.FlexStart;
                        it.style.justifyContent = Justify.FlexStart;
                    },
                    modifier: Modifier
                        .Offset(x: -resolvedState.Value.Px())
                        .OnGloballyPositioned(it => resolvedState.ContentSize = it.Width),
                    content: content
                );
            }
        );
    }
}

public class ScrollState : IComposeDisposable
{
    private readonly IMutableState<float> _value;
    private readonly IMutableState<float> _viewportSize = MutableStateOf(0f);
    private readonly IMutableState<float> _contentSize = MutableStateOf(0f);
    private IDisposable? _coroutine;
    private float _animationTargetValue;

    internal ScrollState(float initialValue)
    {
        _value = MutableStateOf(initialValue);
    }

    public float Value
    {
        get => _value.Value;
        private set => _value.Value = value;
    }

    public float MaxValue => ContentSize - ViewportSize;

    public float ViewportSize
    {
        get => _viewportSize.Value;
        internal set => _viewportSize.Value = value;
    }

    public float ContentSize
    {
        get => _contentSize.Value;
        internal set => _contentSize.Value = value;
    }

    public void ScrollTo(float value)
    {
        Value = Mathf.Clamp(value, 0, MaxValue);
    }

    public void ScrollBy(float value) => ScrollTo(_value.GetValue() + value);

    public void AnimateScrollTo(float value, Optional<AnimationSpec> animationSpec = default)
    {
        value = Mathf.Clamp(value, 0, MaxValue);
        if (_value.GetValue().AlmostEquals(value))
            return;
        _animationTargetValue = value;
        if (_coroutine != null)
            return;
        _coroutine = ComposeInvalidator.StartCoroutineAsDisposable(
            UpdatePropertyCoroutine(animationSpec.GetOrDefault())
        );
    }

    public void AnimateScrollBy(float value, Optional<AnimationSpec> animationSpec = default) =>
        AnimateScrollTo(_value.GetValue() + value, animationSpec);

    public void Dispose() => _coroutine?.Dispose();

    private IEnumerator UpdatePropertyCoroutine(AnimationSpec animationSpec)
    {
        yield return null;
        var startValue = _value.GetValue();
        if (EqualityUtils.FastEquals(startValue, _animationTargetValue)) yield break;
        if (animationSpec.Delay > 0)
            yield return new WaitForSeconds(animationSpec.Delay);
        var elapsed = animationSpec.Delay;
        while (elapsed < animationSpec.TotalDuration)
        {
            elapsed += Time.deltaTime;
            _value.Value = Mathf.LerpUnclamped(startValue, _animationTargetValue, animationSpec.GetProgress(elapsed));
            yield return null;
        }

        _value.Value = _animationTargetValue;
        _coroutine = null;
    }
}

internal class ScrollableColumn : VisualElement
{
}

internal class ScrollableColumnContent : VisualElement
{
}

internal class ScrollableRow : VisualElement
{
}

internal class ScrollableRowContent : VisualElement
{
}