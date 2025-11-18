// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;

namespace UnityCompose
{
    [DefaultExecutionOrder(-1_000_000)]
    [DisallowMultipleComponent, ExecuteAlways]
    internal class ComposeInvalidator : MonoBehaviour
    {
        private class CoroutineDisposableImpl : IDisposable
        {
            private readonly Coroutine? _coroutine;

            public CoroutineDisposableImpl(Coroutine coroutine)
            {
                _coroutine = coroutine;
            }

            public void Dispose()
            {
                if (_coroutine != null)
                    Instance.StopCoroutine(_coroutine);
            }
        }

        private class EmptyDisposableImpl : IDisposable
        {
            public void Dispose()
            {
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
                    // _instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
                    if (ApplicationUtils.IsPlaying)
                        DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance!;
            }
        }

        private readonly ISet<IComposeGroup> _invalidatedGroups = new HashSet<IComposeGroup>();
        private readonly ISet<IComposeGroup> _instantInvalidatedGroups = new HashSet<IComposeGroup>();

        public ComposeInvalidator()
        {
            _instance = this;
        }

        private void Awake()
        {
            _instance = this;
            // gameObject.hideFlags = HideFlags.HideAndDontSave;
            if (ApplicationUtils.IsPlaying)
                DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (_invalidatedGroups.Count == 0) return;
            var groupsToInvalidate = _invalidatedGroups.ToImmutableStableList();
            _invalidatedGroups.Clear();
            foreach (var group in groupsToInvalidate)
                CurrentComposer.Invalidate(group);
        }

        internal static IDisposable StartCoroutineAsDisposable(IEnumerator coroutine)
        {
            if (!ApplicationUtils.IsPlaying) return new EmptyDisposableImpl();
            return new CoroutineDisposableImpl(Instance.StartCoroutine(coroutine));
        }

        internal static void RequestInvalidate(IComposeGroup group)
        {
            if (!ApplicationUtils.IsPlaying) return;
            if (Instance._instantInvalidatedGroups.Contains(group)) return;
            Instance._invalidatedGroups.Add(group);
        }

        internal static void CancelInvalidate(IComposeGroup group)
        {
            if (!ApplicationUtils.IsPlaying) return;
            Instance._instantInvalidatedGroups.Remove(group);
            Instance._invalidatedGroups.Remove(group);
        }

        internal static void RequestInstantInvalidate(IComposeGroup group)
        {
            Instance._instantInvalidatedGroups.Add(group);
            Instance._invalidatedGroups.Remove(group);
            // CurrentComposer.Invalidate(group);
        }

        internal static void InstantInvalidate()
        {
            // if (!ApplicationUtils.IsPlaying) return;
            if (Instance._instantInvalidatedGroups.Count == 0) return;
            var groupsToInvalidate = Instance._instantInvalidatedGroups.ToImmutableStableList();
            Instance._instantInvalidatedGroups.Clear();
            foreach (var group in groupsToInvalidate)
                CurrentComposer.Invalidate(group);
        }
    }
}