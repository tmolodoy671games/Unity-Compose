// ReSharper disable CheckNamespace

using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public class AwaitPointerEventScopeImpl : IAwaitPointerEventScope
{
    private readonly VisualElement _element;

    public AwaitPointerEventScopeImpl(VisualElement element, CancellationToken token)
    {
        _element = element;
        Token = token;
    }

    public Size Size => _element.contentRect.size.ToSize();
    public CancellationToken Token { get; }
    public VisualElement Element => _element;

    public async Task<IPointerInputChange> AwaitPointerInputChange()
    {
        var tcs = new TaskCompletionSource<IPointerInputChange>(TaskCreationOptions.RunContinuationsAsynchronously);

        EventCallback<PointerDownEvent> onDown = it =>
        {
            _element.CapturePointer(it.pointerId);
            tcs.TrySetResult(PointerInputChange.Create(it));
        };
        EventCallback<PointerMoveEvent> onMove = it => tcs.TrySetResult(PointerInputChange.Create(it));
        EventCallback<PointerUpEvent> onUp = it =>
        {
            _element.ReleasePointer(it.pointerId);
            tcs.TrySetResult(PointerInputChange.Create(it));
        };
        EventCallback<PointerCancelEvent> onCancel = it => tcs.TrySetResult(PointerInputChange.Create(it));
        _element.RegisterCallbackOnce(onDown);
        _element.RegisterCallbackOnce(onMove);
        _element.RegisterCallbackOnce(onUp);
        _element.RegisterCallbackOnce(onCancel);

        void Clear()
        {
            _element.UnregisterCallback(onDown);
            _element.UnregisterCallback(onMove);
            _element.UnregisterCallback(onUp);
            _element.UnregisterCallback(onCancel);
        }

        Token.Register(() =>
        {
            Clear();
            tcs.TrySetCanceled(Token);
        });
        var result = await tcs.Task;
        Clear();
        return result;
    }
}