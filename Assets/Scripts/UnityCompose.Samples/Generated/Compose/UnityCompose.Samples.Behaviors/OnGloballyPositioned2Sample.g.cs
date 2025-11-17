using System;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositioned2Sample : ComposeUI
    {
        [Composable]
        protected override void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Content());
            }
        }

        [Composable]
        protected override void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Preview());
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(string.Empty))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: static () =>
                {
                    var positions = Remember(CurrentComposer.WithState(positions).Remember<Action>(__ => () =>
                    {
                        var selectionIndex = Remember(CurrentComposer.WithState(__.selectionIndex).Remember<Action>(__ => () => selectionIndex.Value = 0));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick(CurrentComposer.WithState(__.positions).Remember<Action>(__ => it => positions[0] = it.GlobalPosition)).OnGloballyPositioned(static () => Text(text: "First")), content: CurrentComposer.WithState(__.selectionIndex).Remember<Action>(__ => () => selectionIndex.Value = 1));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick(CurrentComposer.WithState(__.positions).Remember<Action>(__ => it => positions[1] = it.GlobalPosition)).OnGloballyPositioned(static () => Text(text: "Second")), content: CurrentComposer.WithState(__.selectionIndex).Remember<Action>(__ => () => selectionIndex.Value = 2));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick(CurrentComposer.WithState(__.positions).Remember<Action>(__ => it => positions[2] = it.GlobalPosition)).OnGloballyPositioned(static () => Text(text: "Third")), content: CurrentComposer.WithState(__.selectionIndex).Remember<Action>(__ => () => selectionIndex.Value = 3));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick(CurrentComposer.WithState(__.positions).Remember<Action>(__ => it => positions[3] = it.GlobalPosition)).OnGloballyPositioned(static () => Text(text: "Fourth")), content: () => Text(text: "Fourth"));
                    }));
                    Row(() =>
                    {
                        var selectionIndex = Remember(static () => MutableStateOf(0));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick(() => selectionIndex.Value = 0).OnGloballyPositioned(it => positions[0] = it.GlobalPosition), content: () => Text(text: "First"));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick(() => selectionIndex.Value = 1).OnGloballyPositioned(it => positions[1] = it.GlobalPosition), content: () => Text(text: "Second"));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick(() => selectionIndex.Value = 2).OnGloballyPositioned(it => positions[2] = it.GlobalPosition), content: () => Text(text: "Third"));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick(() => selectionIndex.Value = 3).OnGloballyPositioned(it => positions[3] = it.GlobalPosition), content: () => Text(text: "Fourth"));
                    });
                    foreach (var position in positions.Values)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Background(Color.red).Size(16).Border(4, topLeftRadius: 0).Float().Position(left: measurer.GlobalToLocal(position).x, top: measurer.GlobalToLocal(position).y));
                    }
                });
            }
            finally
            {
                CurrentComposer.EndComposeGroup(static () => __Layout());
            }
        }

        [Composable]
        private static void __Tab(bool selected, [Composable] Action content, IModifier? modifier = null)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            if (CurrentComposer.BeginComposeGroup((__selected, __content, __modifier)))
                return;
            try
            {
                Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8, horizontal: AnimateFloatAsState(selected ? 160 : 20).Value).Margin(horizontal: 2).Border(16, topLeftRadius: 0).Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value), content: CurrentComposer.WithState(content).Remember<Action>(__ => () =>
                {
                    CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content);
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__selected, __content, __modifier)).Remember<Action>(static __ => () => __Tab(__.__selected, __.__content, __.__modifier)));
            }
        }
    }
}