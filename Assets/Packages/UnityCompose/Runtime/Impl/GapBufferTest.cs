using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl
{
    [DisallowMultipleComponent]
    internal class GapBufferTest : MonoBehaviour
    {
        [Button]
        private void Test()
        {
            var list = new GapBufferList<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);
            for (var i = 100; i < 105; i++)
                list.InsertAt(0, i);
            Debug.Log(list);
        }
    }
}