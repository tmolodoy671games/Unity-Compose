// ReSharper disable CheckNamespace
// #define LOGGING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;

namespace UnityCompose
{
    [DefaultExecutionOrder(-1_000_000)]
    [DisallowMultipleComponent, ExecuteAlways]
    internal class ComposeInvalidator : MonoBehaviour
    {
        private class CoroutineComposeDisposableImpl : IComposeDisposable
        {
            private readonly Coroutine? _coroutine;

            public CoroutineComposeDisposableImpl(Coroutine coroutine)
            {
                _coroutine = coroutine;
            }

            public void Dispose()
            {
                if (_coroutine != null)
                    Instance.StopCoroutine(_coroutine);
            }
        }

        private class ComposeDisposableImpl : IDisposable
        {
            private readonly Coroutine? _coroutine;

            public ComposeDisposableImpl(Coroutine coroutine)
            {
                _coroutine = coroutine;
            }

            public void Dispose()
            {
                if (_coroutine != null)
                    Instance.StopCoroutine(_coroutine);
            }
        }

        private static ComposeInvalidator? _instance;

        private static ComposeInvalidator Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("ComposeInvalidator").AddComponent<ComposeInvalidator>();
                    if (ApplicationUtils.IsPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }

                _instance!.gameObject.hideFlags = HideFlags.HideAndDontSave;
                return _instance!;
            }
        }

        private readonly List<ComposeRestartScope> _invalidatedGroups = new();
        private readonly List<ComposeRestartScope> _groupsToRestart = new();

        public ComposeInvalidator()
        {
            _instance = this;
        }

        private void Awake()
        {
            _instance = this;
            if (ApplicationUtils.IsPlaying)
                DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (_invalidatedGroups.Count == 0) return;
            _groupsToRestart.AddRange(_invalidatedGroups);
            _invalidatedGroups.Clear();
            foreach (var group in _groupsToRestart)
            {
#if LOGGING
                group.Writer.LogWarning("Restart");
#endif
                group.Restart();
            }

            _groupsToRestart.Clear();
        }

        internal static IComposeDisposable StartCoroutineAsComposeDisposable(IEnumerator coroutine)
        {
            return new CoroutineComposeDisposableImpl(Instance.StartCoroutine(coroutine));
        }
        
        internal static IDisposable StartCoroutineAsDisposable(IEnumerator coroutine)
        {
            return new ComposeDisposableImpl(Instance.StartCoroutine(coroutine));
        }

        internal static void RequestInvalidate(ComposeRestartScope scope)
        {
            Instance._invalidatedGroups.Add(scope);
        }

        internal static void CancelInvalidate(ComposeRestartScope scope)
        {
            Instance._invalidatedGroups.Remove(scope);
        }
    }
}