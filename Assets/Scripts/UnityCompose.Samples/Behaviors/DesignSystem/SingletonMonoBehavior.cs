// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Unity.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    public class SingletonMonoBehavior : MonoBehaviour
    {
        private static SingletonMonoBehavior _instance = null!;

        public static SingletonMonoBehavior Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instanceHolder = new GameObject("SingletonMonoBehavior");
                    DontDestroyOnLoad(instanceHolder);
                    instanceHolder.hideFlags = HideFlags.HideAndDontSave;
                    instanceHolder.AddComponent<SingletonMonoBehavior>();
                }

                return _instance.NotNull();
            }
        }

        protected SingletonMonoBehavior()
        {
            _instance = this;
        }
    }
}