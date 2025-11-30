using System.Collections.Generic;
using System.Text;
using Packages.UnityCompose.Impl.SlotTableModels.Models;
using Packages.UnityCompose.Impl.SlotTableWriting.Wrappers;

namespace Packages.UnityCompose.Impl.SlotTableWriting.Writer;

public class SlotTableWriter
{
    private readonly SlotTable _table;
    private readonly Groups _groups;
    private readonly Slots _slots;
    private readonly Anchors _anchors;
    private readonly Stack<int> _parentAnchorsIds = new();

    private int _currentParentIndex;
    private int _currentGroupIndex;
    private int _currentSlotIndex;

    public SlotTableWriter(SlotTable table)
    {
        _table = table;
        _groups = new Groups(table.Groups);
        _slots = new Slots(table.Slots);
        _anchors = new Anchors(table.Anchors);
    }
    
    private ComposeGroup CurrentParent => _groups[_currentParentIndex];

    #region Root Group

    public void StartRootGroup()
    {
        _currentGroupIndex = 0;
        _currentParentIndex = -1;
        _currentSlotIndex = 0;
        if (_groups.Count == 0)
        {
            _anchors.AllocateAnchor(0);
            var dataAnchor = _anchors.AllocateAnchor(0);
            var newGroup = new ComposeGroup(
                Key: 0,
                Type: ComposeGroupType.Root,
                ParentAnchorId: -1,
                Size: 1,
                DataAnchorId: dataAnchor
            );
            _groups.Add(newGroup);
        }
        EnterRootGroup();
    }

    public IScopeUpdateScope? EndRootGroup()
    {
        var currentParent = CurrentParent;
        var slotIndex = _anchors[currentParent.DataAnchorId].Location;
        var restartScope = _slots.GetRestartScope(slotIndex);
        ExitRootGroup(currentParent);
        return restartScope;
    }

    private void EnterRootGroup()
    {
        _currentParentIndex = _currentGroupIndex;
        _currentGroupIndex++;
        _parentAnchorsIds.Push(0);
    }

    private void ExitRootGroup(ComposeGroup group)
    {
        _currentParentIndex = _anchors[group.ParentAnchorId].Location;
        _parentAnchorsIds.Pop();
    }

    #endregion


    #region Replace Group

    

    #endregion


    #region Restart Group

    

    #endregion


    #region Remember

    

    #endregion


    #region CompositionLocal

    public override string ToString()
    {
        var builder = new StringBuilder();
        
        builder.AppendLine("Groups:");
        builder.AppendLine(_groups.ToString());
        
        builder.AppendLine("Slots:");
        builder.AppendLine(_slots.ToString(_currentSlotIndex));
        
        builder.AppendLine("Anchors:");
        builder.AppendLine(_anchors.ToString());
        
        return builder.ToString();
    }

    #endregion
}