using StableCollections;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IComposeSelection2D<T>
{
    T? Current { get; set; }

    void GoLeft();
    void GoRight();
    void GoUp();
    void GoDown();
}

internal class ComposeSelection2DImpl<T> : IComposeSelection2D<T>
{
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
}