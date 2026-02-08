using System;
using System.Collections;
using Core.Unity.Behaviors;
using UnityCompose;
using UnityEngine;

namespace UI.DesignSystem.Compose.Players;

public interface ISingleAnimationPlayer
{
    float Progress { get; }
    bool IsRunning { get; }

    void Start();
    void Stop();
}

internal class SingleAnimationPlayerImpl : ISingleAnimationPlayer, IComposeDisposable
{
    private readonly AnimationSpec _animationSpec;
    private bool _isRunning;
    private readonly IMutableState<float> _progress = MutableStateOf(0f);
    private Coroutine? _coroutine;
    private readonly bool _isDebuggable;

    public SingleAnimationPlayerImpl(AnimationSpec animationSpec, bool debuggable)
    {
        _isDebuggable = debuggable;
        _animationSpec = animationSpec;
    }

    public float Progress => _progress.Value;
    public bool IsRunning => _isRunning;

    public void Start()
    {
        if (_isRunning)
            return;
        _isRunning = true;
        _coroutine = SingletonMonoBehavior.Instance.StartCoroutine(UpdateValueCoroutine());
    }

    public void Stop()
    {
        if (!_isRunning)
        {
            _progress.Value = 0f;
            return;
        }

        SingletonMonoBehavior.Instance.StopCoroutine(_coroutine);
        _coroutine = null;
        _progress.Value = 0f;
        _isRunning = false;
    }

    private IEnumerator UpdateValueCoroutine()
    {
        _isRunning = true;
        var startTime = Time.time;
        _progress.Value = 0f;
        while (_progress.Value < 1f)
        {
            var elapsedTime = Time.time - startTime;
            _progress.Value = _animationSpec.GetProgress(elapsedTime);
            yield return null;
        }

        _isRunning = false;
        _coroutine = null;
    }

    public void Dispose() => Stop();
}