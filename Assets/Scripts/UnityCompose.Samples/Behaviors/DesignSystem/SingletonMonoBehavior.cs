// ReSharper disable ArrangeNamespaceBody

using SharpExtensions;

namespace UnityCompose.Samples.Behaviors.DesignSystem
{
    [DisallowMultipleComponent]
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