using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable ArrangeNamespaceBody
namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample
    {
        [Composable]
        private static void __Layout()
        {
            if (CurrentComposer.BeginComposeGroup(-1449944650, true))
                return;
            try
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier, content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-2140312613, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("reordarable-list-sample").Padding(top: 100).FillMaxHeight().Width(800), content: CurrentComposer.HasRememberedValue<bool, UnityCompose.ComposableContent>(-664965041, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<bool, UnityCompose.ComposableContent>(() =>
                    {
                        // var items = Remember(() => MutableStateListOf(1, 2));
                        Text(text: "Add Item", color: Color.white, fontSize: 40, modifier: Modifier.Name("add-item-button").Align(Alignment.Right).Background(Color.blue).Padding(horizontal: 32, vertical: 16).Border(radius: 16)// .OnClick(() =>
                        // {
                        // for (var i = 1; i <= items.Count + 1; i++)
                        // {
                        //     if (!items.Contains(i))
                        //     {
                        //         items.Add(i);
                        //         return;
                        //     }
                        // }
                        // })
                        );
                    // Column(
                    //     horizontalAlignment: Alignment.Horizontal.Center,
                    //     modifier: Modifier
                    //         .Name("nested-column")
                    //         .FillMaxWidth(),
                    //     content: () =>
                    //     {
                    //         foreach (var item in items)
                    //         {
                    //             Key(
                    //                 key: item,
                    //                 content: () =>
                    //                 {
                    //                     Item(
                    //                         state: item,
                    //                         onMoveUpClick: () =>
                    //                         {
                    //                             var oldIndex = items.IndexOf(item);
                    //                             if (oldIndex == 0) return;
                    //                             var newIndex = oldIndex - 1;
                    //                             items.RemoveAt(oldIndex);
                    //                             items.Insert(newIndex, item);
                    //                         },
                    //                         onMoveDownClick: () =>
                    //                         {
                    //                             var oldIndex = items.IndexOf(item);
                    //                             if (oldIndex == items.Count - 1) return;
                    //                             var newIndex = oldIndex + 1;
                    //                             items.RemoveAt(oldIndex);
                    //                             items.Insert(newIndex, item);
                    //                         },
                    //                         onRemoveClick: () => { items.Remove(item); }
                    //                     );
                    //                 }
                    //             );
                    //         }
                    //     }
                    // );
                    }));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(-1449844650, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Layout()));
            }
        }

        [Composable]
        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick)
        {
            var(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick) = (state, onMoveUpClick, onMoveDownClick, onRemoveClick);
            if (CurrentComposer.BeginComposeGroup(1605183758, (__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick)))
                return;
            try
            {
                Row(verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("item-row").Background(Color.cyan).FillMaxWidth().Padding(all: 4).Border(radius: 12).Margin(vertical: 4).Name(state.ToString()), content: CurrentComposer.HasRememberedValue<ValueTuple<int, System.Action, System.Action, System.Action>, UnityCompose.ComposableContent>(324724880, (state, onMoveUpClick, onMoveDownClick, onRemoveClick)) ? CurrentComposer.RememberedValue<ValueTuple<int, System.Action, System.Action, System.Action>, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<ValueTuple<int, System.Action, System.Action, System.Action>, UnityCompose.ComposableContent>(() =>
                {
                    Text(text: $"Item no. {state}", color: Color.black, fontSize: 40, modifier: Modifier.Name("item-name-label").Weight(1).Margin(left: 32));
                    Column(content: CurrentComposer.HasRememberedValue<ValueTuple<System.Action, System.Action>, UnityCompose.ComposableContent>(481865297, (onMoveUpClick, onMoveDownClick)) ? CurrentComposer.RememberedValue<ValueTuple<System.Action, System.Action>, UnityCompose.ComposableContent>() : CurrentComposer.WriteComposableLambda<ValueTuple<System.Action, System.Action>, UnityCompose.ComposableContent>(() =>
                    {
                        Text(text: "↑", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("up-arrow-button").Background(Color.green).Padding(horizontal: 6, vertical: 4).Border(radius: 16).OnClick(onMoveUpClick));
                        Text(text: "↓", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("down-arrow-button").Background(Color.green).Padding(horizontal: 6, vertical: 4).Border(radius: 16).OnClick(onMoveDownClick));
                    }));
                    Text(text: "X", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("remove-button").Background(Color.red).Padding(horizontal: 16, vertical: 4).Border(radius: 16).OnClick(onRemoveClick));
                }));
            }
            finally
            {
                CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<int, Action, Action, Action>, Action>(1605283758, (__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick)) ? CurrentComposer.RememberedValue<ValueTuple<int, Action, Action, Action>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<int, Action, Action, Action>, Action>(() => __Item(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick)));
            }
        }
    }
}