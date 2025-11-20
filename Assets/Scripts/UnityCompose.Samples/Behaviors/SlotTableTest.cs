using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

namespace UnityCompose.Samples.Behaviors
{
    [DisallowMultipleComponent, HideMonoScript]
    internal class SlotTableTest : MonoBehaviour
    {
        [Button]
        private void Test()
        {
            SlotTest.Test();
        }
    }
}