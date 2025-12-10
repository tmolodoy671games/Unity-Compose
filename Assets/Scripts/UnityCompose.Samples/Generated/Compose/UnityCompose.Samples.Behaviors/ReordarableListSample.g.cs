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
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1530013095);
            if (__composer.ShouldExecute(true))
            {
                Box(horizontalAlignment: Alignment.Horizontal.Center, verticalAlignment: Alignment.Vertical.Center, modifier: Modifier, content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Column(horizontalAlignment: Alignment.Horizontal.Center, modifier: Modifier.Name("reordarable-list-sample").Padding(top: 100).FillMaxHeight().Width(800), content: !__composer.Changed() ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
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
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1530013095)?.UpdateScope(() => __Layout());
        }

        [Composable]
        private static void __Item(int state, Action onMoveUpClick, Action onMoveDownClick, Action onRemoveClick)
        {
            var(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick) = (state, onMoveUpClick, onMoveDownClick, onRemoveClick);
            var __composer = CurrentComposer;
            __composer.StartRestartGroup(-1524142510);
            if (__composer.ShouldExecute((__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick)))
            {
                Row(verticalAlignment: Alignment.Vertical.Center, modifier: Modifier.Name("item-row").Background(Color.cyan).FillMaxWidth().Padding(all: 4).Border(radius: 12).Margin(vertical: 4).Name(state.ToString()), content: !__composer.Changed((state, onMoveUpClick, onMoveDownClick, onRemoveClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                {
                    Text(text: $"Item no. {state}", color: Color.black, fontSize: 40, modifier: Modifier.Name("item-name-label").Weight(1).Margin(left: 32));
                    Column(content: !__composer.Changed((onMoveUpClick, onMoveDownClick)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                    {
                        Text(text: "↑", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("up-arrow-button").Background(Color.green).Padding(horizontal: 6, vertical: 4).Border(radius: 16).OnClick(onMoveUpClick));
                        Text(text: "↓", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("down-arrow-button").Background(Color.green).Padding(horizontal: 6, vertical: 4).Border(radius: 16).OnClick(onMoveDownClick));
                    }));
                    Text(text: "X", color: Color.white, fontSize: 40, fontWeight: FontWeight.Bold, textAlign: TextAlign.MiddleCenter, modifier: Modifier.Name("remove-button").Background(Color.red).Padding(horizontal: 16, vertical: 4).Border(radius: 16).OnClick(onRemoveClick));
                }));
            }
            else
            {
                __composer.SkipToGroupEnd();
            }

            __composer.EndRestartGroup(-1524142510)?.UpdateScope(() => __Item(__state, __onMoveUpClick, __onMoveDownClick, __onRemoveClick));
        }
    }
}