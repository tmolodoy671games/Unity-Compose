using UnityCompose;

namespace System.Runtime.CompilerServices;

public partial class SampleClass
{
    [Composable]
    public void Foo()
    {
    }

    public void Bar()
    {
        // Foo();
    }
}