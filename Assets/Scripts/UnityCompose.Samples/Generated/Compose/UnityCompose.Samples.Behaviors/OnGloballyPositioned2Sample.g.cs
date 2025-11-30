using System;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class OnGloballyPositioned2Sample
    {
        [Composable]
        private void __Content()
        {
            if (CurrentComposer.BeginComposeGroup(-591915576, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-591815576, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
            }
        }

        [Composable]
        private void __Preview()
        {
            if (CurrentComposer.BeginComposeGroup(238988961, true))
                return;
            try
            {
                Layout();
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(239088961, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Preview()));
            }
        }

        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(-1546365619, true))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.FillMaxSize(), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(1794919710, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    var positions = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>(-1229295761, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>>(static () => MutableStateDictionaryOf<int, Vector2>());
                    Row(CurrentComposer.HasRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, UnityCompose.ComposableContent>(1151995684, positions) ? CurrentComposer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, UnityCompose.ComposableContent>(() =>
                    {
                        var selectionIndex = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<int>>(-1796191961, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<int>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<int>>(static () => MutableStateOf(0));
                        Tab(selected: selectionIndex.Value == 0, modifier: Modifier.OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(-478716000, selectionIndex) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => selectionIndex.Value = 0)).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(353735901, positions) ? CurrentComposer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it => positions[0] = it.GlobalPosition)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-2010060726, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() => Text(text: "First")));
                        Tab(selected: selectionIndex.Value == 1, modifier: Modifier.OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(170767934, selectionIndex) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => selectionIndex.Value = 1)).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(529783232, positions) ? CurrentComposer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it => positions[1] = it.GlobalPosition)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-1678886377, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() => Text(text: "Second")));
                        Tab(selected: selectionIndex.Value == 2, modifier: Modifier.OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(177927277, selectionIndex) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => selectionIndex.Value = 2)).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(1292131799, positions) ? CurrentComposer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it => positions[2] = it.GlobalPosition)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(921451707, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() => Text(text: "Third")));
                        Tab(selected: selectionIndex.Value == 3, modifier: Modifier.OnClick(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<int>?, System.Action>(1242947581, selectionIndex) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<int>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<int>?, System.Action>(() => selectionIndex.Value = 3)).OnGloballyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(-1105618769, positions) ? CurrentComposer.RememberedValue<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableStateDictionary<int, UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it => positions[3] = it.GlobalPosition)), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(136418836, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() => Text(text: "Fourth")));
                    }));
                    foreach (var position in positions.Values)
                    {
                        var measurer = LocalLayoutMeasurer.Current;
                        Spacer(modifier: Modifier.Background(Color.red).Size(16).Border(4, topLeftRadius: 0).Float().Position(left: measurer.GlobalToLocal(position).x, top: measurer.GlobalToLocal(position).y));
                    }
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-1546265619, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }

        [Composable]
        private static void __Tab(bool selected, ComposableContent content, IModifier? modifier = null)
        {
            var(__selected, __content, __modifier) = (selected, content, modifier);
            if (CurrentComposer.BeginComposeGroup(356085281, (__selected, __content, __modifier)))
                return;
            try
            {
                Box(modifier: modifier.OrEmpty().Background(Color.grey).Padding(vertical: 8, horizontal: AnimateFloatAsState(selected ? 160 : 20).Value).Margin(horizontal: 2).Border(16, topLeftRadius: 0).Scale(AnimateFloatAsState(selected ? 0.8f : 1).Value), content: CurrentComposer.HasRememberedValue<UnityCompose.ComposableContent, UnityCompose.ComposableContent>(1455726362, content) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<UnityCompose.ComposableContent, UnityCompose.ComposableContent>(() =>
                {
                    CompositionLocalProvider(LocalContentColor.Provides(Color.white), LocalTextStyle.Provides(new TextStyle(Color: Color.white, FontSize: 32)), content: content);
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<bool, ComposableContent, IModifier?>, Action>(356185281, (__selected, __content, __modifier)) ? CurrentComposer.RememberedValue<ValueTuple<bool, ComposableContent, IModifier?>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<bool, ComposableContent, IModifier?>, Action>(() => __Tab(__selected, __content, __modifier)));
            }
        }
    }
}