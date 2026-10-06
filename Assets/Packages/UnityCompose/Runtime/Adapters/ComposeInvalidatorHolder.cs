using System;
using System.Collections;
using System.Collections.Generic;
using Compose.Net;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable ArrangeNamespaceBody
// ReSharper disable CheckNamespace

namespace UnityCompose
{
    [ExecuteAlways]
    [DefaultExecutionOrder(-1_000)]
    internal class ComposeInvalidatorHolder : MonoBehaviour
    {
        private static ComposeInvalidatorHolder? _instance;
        private readonly ComposeInvalidator _invalidator = new();
        private static bool _destroyed = false;

        private static ComposeInvalidatorHolder? Instance
        {
            get
            {
                if ( _destroyed)
                    return null;
                if (!_instance)
                {
                    _instance = FindAnyObjectByType<ComposeInvalidatorHolder>() ??
                                new GameObject("Coroutine Runner").AddComponent<ComposeInvalidatorHolder>();
                    DontDestroyOnLoad(_instance);
                    _instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
                }

                return _instance;
            }
        }

        public static ComposeInvalidator? ComposeInvalidator
        {
            get
            {
                if (!ApplicationUtils.IsPlaying)
                    return null;
                return Instance?._invalidator;
            }
        }

        private void Awake()
        {
            _destroyed = false;
            _instance = this;
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            _instance = null;
            _destroyed = true;
        }

        private void Update()
        {
            _invalidator.Tick();
        }

        public static IDisposable StartCoroutineAsDisposable(IEnumerable<TimeSpan?>? coroutine)
        {
            if (Instance == null || coroutine == null)
                return new CustomDisposable(() => { });
            var coroutineHandle = Instance.StartCoroutine(CoroutineAdapter(coroutine));
            return new CustomDisposable(() => Instance?.StopCoroutine(coroutineHandle));
        }

        public static IDisposable StartCoroutineAsDisposable(IEnumerator? coroutine)
        {
            if (Instance == null || coroutine == null)
                return new CustomDisposable(() => { });
            var coroutineInstance = Instance.StartCoroutine(coroutine);
            return new CustomDisposable(() => Instance?.StopCoroutine(coroutineInstance));
        }

        private static IEnumerator CoroutineAdapter(IEnumerable<TimeSpan?>? coroutine)
        {
            if (coroutine == null)
                yield break;
            foreach (var step in coroutine)
            {
                if (step == null)
                    yield return null;
                else
                    yield return new WaitForSeconds(step.Value.TotalSeconds.ToFloat());
            }
        }
    }
}