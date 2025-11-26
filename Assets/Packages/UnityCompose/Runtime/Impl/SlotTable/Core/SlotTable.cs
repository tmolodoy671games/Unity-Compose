using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Core;

internal class SlotTable
{
    public readonly ReusableComposeGroup Root = new ReusableComposeGroup<bool>(0, null, true, null!);

    public string ToString(ComposeGroup? currentParent, int currentIndex)
    {
        var builder = new StringBuilder();
        if (currentParent == null)
        {
            builder.Append(" < CURRENT_PARENT");
            builder.AppendLine();
        }

        if (currentIndex == -1 && currentParent == null)
        {
            builder.Append(" < CURRENT_INDEX");
            builder.AppendLine();
        }

        builder.Append(Root.ToString(currentParent, currentIndex));
        builder.AppendLine();
        if (currentIndex == 1 && currentParent == null)
        {
            builder.Append(" < CURRENT_INDEX");
            builder.AppendLine();
        }

        builder.AppendLine();
        builder.AppendLine();
        return builder.ToString();
    }
}