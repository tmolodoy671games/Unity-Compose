using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

public class Text : Label
{
    public override string ToString() => this.Format();
}