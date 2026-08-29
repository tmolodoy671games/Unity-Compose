using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public interface IFocusManager
{
    void ClearFocus();
    void MoveFocus(FocusDirection focusDirection);
}

public enum FocusDirection
{
    // Next,
    // Previous,
    Left,
    Right,
    Up,
    Down,
    // Enter,
    // Exit,
}

internal static partial class VisualElementExtensions
{
    private const string FocusManagerKey = "UnityCompose_FocusManager";
    
    public static FocusManagerImpl FocusManager(this ComposeView composeView)
    {
        var value = composeView.UserData().GetOrNull(FocusManagerKey);
        if (value is not FocusManagerImpl focusManager)
        {
            focusManager = new FocusManagerImpl();
            composeView.UserData()[FocusManagerKey] = focusManager;
        }
        return focusManager;
    }
}

internal class FocusManagerImpl : IFocusManager
{
    private readonly IMutableStableList<VisualElement> _focusables = MutableStableListOf<VisualElement>();
    private VisualElement? _currentFocus;

    public void ClearFocus()
    {
        Unfocus();
    }

    public void MoveFocus(FocusDirection focusDirection)
    {
        if (_focusables.IsEmpty())
            return;
        if (_currentFocus == null)
        {
            Focus(
                _focusables
                    .OrderBy(it => it.layout.yMin)
                    .ThenBy(it => it.layout.xMin)
                    .FirstOrDefault()
            );
            return;
        }

        switch (focusDirection)
        {
            // case FocusDirection.Next:
            //     OnFocusNext();
            //     break;
            // case FocusDirection.Previous:
            //     OnFocusPrevious();
            //     break;
            case FocusDirection.Left:
                OnFocusLeft();
                break;
            case FocusDirection.Right:
                OnFocusRight();
                break;
            case FocusDirection.Up:
                OnFocusUp();
                break;
            case FocusDirection.Down:
                OnFocusDown();
                break;
            // case FocusDirection.Enter:
            //     throw new NotImplementedException();
            // case FocusDirection.Exit:
            //     throw new NotImplementedException();
            default:
                throw new ArgumentOutOfRangeException(nameof(focusDirection), focusDirection, null);
        }
    }

    public void Add(VisualElement focusable)
    {
        _focusables.Add(focusable);
    }

    public void Remove(VisualElement focusable)
    {
        _focusables.Remove(focusable);
        if (_currentFocus == focusable)
            Unfocus();
    }

    private void OnFocusLeft()
    {
        var currentBounds = _currentFocus.NotNull().worldBound;
        var next = _focusables
            .Where(it => it != _currentFocus)
            .Where(it => it.worldBound.xMax <= currentBounds.xMin)
            .OrderByDescending(it => it.worldBound.xMax)
            .ThenBy(it => Vector2.SqrMagnitude(it.worldBound.center - currentBounds.center))
            .FirstOrDefault() ?? _currentFocus;
        Focus(next);
    }
    
    private void OnFocusRight()
    {
        var currentBounds = _currentFocus.NotNull().worldBound;
        var next = _focusables
            .Where(it => it != _currentFocus)
            .Where(it => it.worldBound.xMin >= currentBounds.xMax)
            .OrderBy(it => it.worldBound.xMin)
            .ThenBy(it => Vector2.SqrMagnitude(it.worldBound.center - currentBounds.center))
            .FirstOrDefault() ?? _currentFocus;
        Focus(next);
    }

    private void OnFocusUp()
    {
        var currentBounds = _currentFocus.NotNull().worldBound;
        var next = _focusables
            .Where(it => it != _currentFocus)
            .Where(it => it.worldBound.yMax <= currentBounds.yMin)
            .OrderByDescending(it => it.worldBound.yMax)
            .ThenBy(it => Vector2.SqrMagnitude(it.worldBound.center - currentBounds.center))
            .FirstOrDefault() ?? _currentFocus;
        Focus(next);
    }
    
    private void OnFocusDown()
    {
        var currentBounds = _currentFocus.NotNull().worldBound;
        var next = _focusables
            .Where(it => it != _currentFocus)
            .Where(it => it.worldBound.yMin >= currentBounds.yMax)
            .OrderBy(it => it.worldBound.yMin)
            .ThenBy(it => Vector2.SqrMagnitude(it.worldBound.center - currentBounds.center))
            .FirstOrDefault() ?? _currentFocus;
        Focus(next);
    }

    private void OnFocusNext()
    {
        var focusOrderList = FocusOrder().ToImmutableStableList();
        var currentIndex = focusOrderList.IndexOf(_currentFocus.NotNull());
        if (currentIndex == focusOrderList.Count - 1)
            return;
        Focus(focusOrderList[currentIndex + 1]);
    }

    private void OnFocusPrevious()
    {
        var focusOrderList = FocusOrder().ToImmutableStableList();
        var currentIndex = focusOrderList.IndexOf(_currentFocus.NotNull());
        if (currentIndex == 0)
            return;
        Focus(focusOrderList[currentIndex - 1]);
    }

    public void Focus(VisualElement? element)
    {
        if (element == _currentFocus)
            return;
        Unfocus();
        if (element == null)
            return;
        _currentFocus = element;
        element.ComposeFocus().Focus();
    }

    public void Unfocus(VisualElement element)
    {
        if (_currentFocus != element)
            return;
        Unfocus();
    }

    private void Unfocus()
    {
        if (_currentFocus == null)
            return;
        _currentFocus.ComposeFocus().Unfocus();
        _currentFocus = null;
    }
    
    private IEnumerable<VisualElement> FocusOrder()
    {
        return _focusables
            .OrderBy(it => it.worldBound.yMin)
            .ThenBy(it => it.worldBound.xMin);
    }
}