using System.Collections.Generic;
using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeSelection2D<T>
{
    T? Current { get; set; }

    void GoLeft();
    void GoRight();
    void GoUp();
    void GoDown();
    IComposeSelectionIndex Index(T index);
}

internal class ComposeSelection2DImpl<T> : IComposeSelection2D<T>
{
    private class ComposeSelectionIndexImpl : IComposeSelectionIndex
    {
        private readonly T _index;
        private readonly IComposeSelection2D<T> _selection;

        public ComposeSelectionIndexImpl(T index, IComposeSelection2D<T> selection)
        {
            _index = index;
            _selection = selection;
        }

        public bool IsSelected => EqualityComparer<T>.Default.Equals(_selection.Current!, _index);
        public void Select() => _selection.Current = _index;

        public void ClearSelection()
        {
            if (IsSelected)
                _selection.Current = default;
        }
    }

    private readonly IMutableStableArray2D<T> _grid;
    private readonly IMutableState<T?> _current;
    private readonly bool _canBeCycled;

    public ComposeSelection2DImpl(IImmutableStableArray2D<T> grid, T? initialIndex, bool canBeCycled)
    {
        _grid = grid.ToMutableStableArray2D();
        _current = MutableStateOf(initialIndex);
        _canBeCycled = canBeCycled;
    }

    public T? Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }

    public void GoLeft()
    {
        var currentIndex = _grid.IndexOf(_current.Value!);
        if (currentIndex.X < 0 || currentIndex.Y < 0) return;
        if (!_canBeCycled && currentIndex.X == 0) return;
        _current.Value = _grid[currentIndex.X - 1, currentIndex.Y];
    }

    public void GoRight()
    {
        var currentIndex = _grid.IndexOf(_current.Value!);
        if (currentIndex.X < 0 || currentIndex.Y < 0) return;
        if (!_canBeCycled && currentIndex.X == _grid.Size.Width - 1) return;
        _current.Value = _grid[currentIndex.X + 1, currentIndex.Y];
    }

    public void GoUp()
    {
        var currentIndex = _grid.IndexOf(_current.Value!);
        if (currentIndex.X < 0 || currentIndex.Y < 0) return;
        if (!_canBeCycled && currentIndex.Y == 0) return;
        _current.Value = _grid[currentIndex.X, currentIndex.Y - 1];
    }

    public void GoDown()
    {
        var currentIndex = _grid.IndexOf(_current.Value!);
        if (currentIndex.X < 0 || currentIndex.Y < 0) return;
        if (!_canBeCycled && currentIndex.Y == _grid.Size.Height - 1) return;
        _current.Value = _grid[currentIndex.X, currentIndex.Y + 1];
    }

    public IComposeSelectionIndex Index(T index)
    {
        return new ComposeSelectionIndexImpl(index, this);
    }
}