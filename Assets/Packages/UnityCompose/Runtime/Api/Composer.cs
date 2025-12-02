// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IComposer
{
    void StartRestartGroup(int key);
    bool ShouldExecute<T>(T state);
    void SkipToGroupEnd();
    IScopeUpdateScope? EndRestartGroup(int key);

    void StartReplaceGroup(int key);
    void EndReplaceGroup(int key);
    
    void StartReusableGroup(int key);
    void EndReusableGroup(int key);

    bool RememberedKeyChanged<T>(int groupKey, T state);
    T RememberedValue<T>();
    T UpdateRememberedValue<T>(T value);
    T UpdateRememberedValue<T>(Func<T> value) => UpdateRememberedValue(value());
    TValue UpdateLambda<TValue>(TValue value) => UpdateRememberedValue(value);
    TValue UpdateComposableLambda<TValue>(TValue value) => UpdateRememberedValue(value);
    
    T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory);
    void UpdateCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides);
    
    T GetOrCreateVisualElement<T>() where T : VisualElement, new();
    void SetVisualElement(VisualElement visualElement);
    int GetElementIndex();
    void EnterVisualElement();

    void Capture(BaseMutableStateImpl state);

    // Debug:
    void Clear();
    void ResetTo(int groupIndex);
}