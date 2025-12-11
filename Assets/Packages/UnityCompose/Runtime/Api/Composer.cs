// ReSharper disable CheckNamespace

using System;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IComposer
{
    void StartRestartGroup(int key);
    bool ShouldExecute();
    bool ShouldExecute<T>(T state);
    bool ShouldExecuteAsStruct<T>(T state) where T : struct;
    void SkipToGroupEnd();
    IScopeUpdateScope? EndRestartGroup(int key);

    void StartReplaceGroup(int key);
    void EndReplaceGroup(int key);

    void StartReusableGroup(int key);
    void EndReusableGroup(int key);

    bool Changed();
    bool Changed<T>(T state);
    T RememberedValue<T>();
    T UpdateRememberedValue<T>(T value);
    T UpdateRememberedValue<T>(Func<T> value);

    void StartLocalGroup(int groupKey);
    T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory);
    void UpdateCompositionLocal(IImmutableStableList<CompositionLocalProvides> provides);
    void EndLocalGroup(int groupKey);

    T GetOrCreateVisualElement<T>() where T : VisualElement, new();
    void SetVisualElement(VisualElement visualElement);
    int GetElementIndex();
    void EnterVisualElement();

    void Capture(BaseMutableStateImpl state);
    void Reset();
    void Clear();
}