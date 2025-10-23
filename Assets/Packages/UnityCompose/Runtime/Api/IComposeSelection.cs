using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeSelection<T>
{
    T? Current { get; set; }

    void GoToPrevious();
    void GoToNext();
}

internal class ComposeSelectionImpl<T> : IComposeSelection<T>
{
    private readonly IImmutableStableList<T> _list;
    private readonly IMutableState<T?> _current;
    private readonly bool _canBeCycled;

    public T? Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }

    public ComposeSelectionImpl(IImmutableStableList<T> list, T? initialIndex, bool canBeCycled)
    {
        _list = list;
        _current = MutableStateOf(initialIndex);
        _canBeCycled = canBeCycled;
    }

    public void GoToPrevious()
    {
        var currentIndex = _list.IndexOf(_current.Value!);
        if (currentIndex == -1)
        {
            _current.Value = _list[0];
            return;
        }
        if (!_canBeCycled && currentIndex == 0)
            return;
        _current.Value = _list[(currentIndex - 1 + _list.Count) % _list.Count];
    }

    public void GoToNext()
    {
        var currentIndex = _list.IndexOf(_current.Value!);
        if (currentIndex == -1)
        {
            _current.Value = _list[0];
            return;
        }
        if (!_canBeCycled && currentIndex == _list.Count - 1)
            return;
        _current.Value = _list[(currentIndex + 1) % _list.Count];
    }
}