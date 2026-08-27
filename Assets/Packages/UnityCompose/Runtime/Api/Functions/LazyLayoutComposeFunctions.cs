// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [Composable]
    public static LazyListState RememberLazyListState()
    {
        return Remember(() => new LazyListState(0f));
    }

    [Composable]
    public static void LazyColumn(
        Action<ILazyListScope> content,
        LazyListState? state = null,
        float scrollStrength = 1f,
        Alignment.Horizontal? horizontalAlignment = null,
        Arrangement.Vertical? verticalArrangement = null,
        IModifier? modifier = null
    )
    {
        var resolvedState = state ?? Remember(() => new LazyListState(0f));
        var scope = Remember(resolvedState, () => new LazyListScopeImpl(resolvedState));
        ReusableComposeView<LazyColumn>(
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
                ReusableComposeView<LazyColumnContent>(
                    initializer: it =>
                    {
                        it.style.flexDirection = FlexDirection.Column;
                        it.style.alignItems = (horizontalAlignment ?? Alignment.Left).ToAlign();
                        it.style.justifyContent = (verticalArrangement ?? Arrangement.Top).ToJustify();
                    },
                    modifier: Modifier
                        .Offset(y: -resolvedState.Value.Px())
                        .OnGloballyPositioned(it => resolvedState.ContentSize = it.Height),
                    content: () =>
                    {
                        SideEffect((scope, content), () =>
                        {
                            scope.Clear();
                            content(scope);
                        });
                        var items = resolvedState.Items;
                        for (var i = 0; i < items.Count; i++)
                        {
                            var currentI = i;
                            var item = items[i];
                            Key(
                                key: item.Key,
                                content: () =>
                                {
                                    ReusableComposeView<LazyListItem>(
                                        modifier: Modifier
                                            .OnLocallyPositioned(it => resolvedState.SyncOffset(currentI, it.LocalTop)),
                                        content: () => item.Content(currentI)
                                    );
                                }
                            );
                        }
                    }
                );
            }
        );
    }

    [Composable]
    public static void LazyRow(
        Action<ILazyListScope> content,
        LazyListState? state = null,
        float scrollStrength = 1f,
        Arrangement.Horizontal? horizontalArrangement = null,
        Alignment.Vertical? verticalAlignment = null,
        IModifier? modifier = null
    )
    {
        var resolvedState = state ?? Remember(() => new LazyListState(0f));
        var scope = Remember(resolvedState, () => new LazyListScopeImpl(resolvedState));
        ReusableComposeView<LazyRow>(
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Column;
                it.style.alignItems = Align.FlexStart;
                it.style.justifyContent = Justify.FlexStart;
            },
            modifier: modifier.OrEmpty()
                .Clip()
                .OnGloballyPositioned(it => resolvedState.ViewportSize = it.Width)
                .OnVerticalScroll(
                    onVerticalScroll: it => resolvedState.AnimateScrollBy(scrollStrength * DefaultScrollMultiplier * it)
                ),
            content: () =>
            {
                ReusableComposeView<LazyRowContent>(
                    initializer: it =>
                    {
                        it.style.flexDirection = FlexDirection.Row;
                        it.style.alignItems = (verticalAlignment ?? Alignment.Top).ToAlign();
                        it.style.justifyContent = (horizontalArrangement ?? Arrangement.Left).ToJustify();
                    },
                    modifier: Modifier
                        .Offset(x: -resolvedState.Value.Px())
                        .OnGloballyPositioned(it => resolvedState.ContentSize = it.Width),
                    content: () =>
                    {
                        SideEffect((scope, content), () =>
                        {
                            scope.Clear();
                            content(scope);
                        });
                        var items = resolvedState.Items;
                        for (var i = 0; i < items.Count; i++)
                        {
                            var currentI = i;
                            var item = items[i];
                            Key(
                                key: item.Key,
                                content: () =>
                                {
                                    ReusableComposeView<LazyListItem>(
                                        modifier: Modifier
                                            .OnLocallyPositioned(it =>
                                                resolvedState.SyncOffset(currentI, it.LocalLeft)),
                                        content: () => item.Content(currentI)
                                    );
                                }
                            );
                        }
                    }
                );
            }
        );
    }
}

public interface ILazyListScope
{
    void Item(
        ComposableContent content,
        Func<int, object>? key = null
    );

    void Items(
        int count,
        ComposableContent<int> content,
        Func<int, object>? key = null
    );
}

internal class LazyListScopeImpl : ILazyListScope
{
    private readonly LazyListState _state;

    public LazyListScopeImpl(LazyListState state)
    {
        _state = state;
    }

    internal void Clear()
    {
        _state.Clear();
    }

    public void Item(ComposableContent content, Func<int, object>? key = null)
    {
        _state.AddItem(key, _ => content());
    }

    public void Items(int count, ComposableContent<int> content, Func<int, object>? key = null)
    {
        for (var i = 0; i < count; i++)
            _state.AddItem(key, content);
    }
}

internal class LazyColumn : VisualElement
{
}

internal class LazyColumnContent : VisualElement
{
}

internal class LazyRow : VisualElement
{
}

internal class LazyRowContent : VisualElement
{
}

internal class LazyListItem : VisualElement
{
}

internal readonly record struct LazyListRecord(
    Func<int, object>? Key,
    ComposableContent<int> Content
);

public class LazyListState : IComposeDisposable
{
    private readonly IMutableState<float> _value;
    private readonly IMutableState<float> _viewportSize = MutableStateOf(0f);
    private readonly IMutableState<float> _contentSize = MutableStateOf(0f);
    private readonly IMutableStateDictionary<int, float> _itemsOffsets = MutableStateDictionaryOf<int, float>();
    private readonly IMutableStateList<LazyListRecord> _items = MutableStateListOf<LazyListRecord>();
    private IDisposable? _coroutine;
    private float _animationTargetValue;

    internal LazyListState(float initialValue)
    {
        _value = MutableStateOf(initialValue);
    }

    internal IStableList<LazyListRecord> Items => _items;

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

    public void ScrollToItem(int itemIndex)
    {
        if (!_itemsOffsets.TryGet(itemIndex, out var targetOffset))
            return;
        ScrollTo(targetOffset);
    }

    public void AnimateScrollToItem(int itemIndex, float offset = 0f, Optional<AnimationSpec> animationSpec = default)
    {
        if (!_itemsOffsets.TryGet(itemIndex, out var targetOffset))
            return;
        targetOffset += offset;
        AnimateScrollTo(targetOffset, animationSpec);
    }

    private void ScrollTo(float value, float offset = 0f)
    {
        Value = Mathf.Clamp(value + offset, 0, MaxValue);
    }

    internal void Clear()
    {
        _items.Clear();
    }

    internal void AddItem(Func<int, object>? key, ComposableContent<int> content)
    {
        _items.Add(new LazyListRecord(key, content));
    }

    internal void SyncOffset(int index, float value)
    {
        _itemsOffsets[index] = value;
    }

    internal void AnimateScrollBy(float value, Optional<AnimationSpec> animationSpec = default) =>
        AnimateScrollTo(_value.GetValue() + value, animationSpec);

    private void AnimateScrollTo(float value, Optional<AnimationSpec> animationSpec = default)
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