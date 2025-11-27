using System;

// ReSharper disable ArrangeNamespaceBody

namespace UnityCompose.Samples.Behaviors
{
    internal partial class ReordarableListSample : ComposeUI
    {
        protected override void Content()
        {
            Layout();
        }

        protected override void Preview()
        {
            Layout();
        }

        [Composable]
        private static void Layout()
        {
            Box(
                horizontalAlignment: Alignment.Horizontal.Center,
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier,
                content: () =>
                {
                    Column(
                        horizontalAlignment: Alignment.Horizontal.Center,
                        modifier: Modifier
                            .Name("reordarable-list-sample")
                            .Padding(top: 100)
                            .FillMaxHeight()
                            .Width(800),
                        content: () =>
                        {
                            // var items = Remember(() => MutableStateListOf(1, 2));
                            Text(
                                text: "Add Item",
                                color: Color.white,
                                fontSize: 40,
                                modifier: Modifier
                                    .Name("add-item-button")
                                    .Align(Alignment.Right)
                                    .Background(Color.blue)
                                    .Padding(horizontal: 32, vertical: 16)
                                    .Border(radius: 16)
                                    // .OnClick(() =>
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
                        }
                    );
                }
            );
        }

        [Composable]
        private static void Item(
            int state,
            Action onMoveUpClick,
            Action onMoveDownClick,
            Action onRemoveClick
        )
        {
            Row(
                verticalAlignment: Alignment.Vertical.Center,
                modifier: Modifier
                    .Name("item-row")
                    .Background(Color.cyan)
                    .FillMaxWidth()
                    .Padding(all: 4)
                    .Border(radius: 12)
                    .Margin(vertical: 4)
                    .Name(state.ToString()),
                content: () =>
                {
                    Text(
                        text: $"Item no. {state}",
                        color: Color.black,
                        fontSize: 40,
                        modifier: Modifier
                            .Name("item-name-label")
                            .Weight(1)
                            .Margin(left: 32)
                    );
                    Column(
                        content: () =>
                        {
                            Text(
                                text: "↑",
                                color: Color.white,
                                fontSize: 40,
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("up-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6, vertical: 4)
                                    .Border(radius: 16)
                                    .OnClick(onMoveUpClick)
                            );
                            Text(
                                text: "↓",
                                color: Color.white,
                                fontSize: 40,
                                fontWeight: FontWeight.Bold,
                                textAlign: TextAlign.MiddleCenter,
                                modifier: Modifier
                                    .Name("down-arrow-button")
                                    .Background(Color.green)
                                    .Padding(horizontal: 6, vertical: 4)
                                    .Border(radius: 16)
                                    .OnClick(onMoveDownClick)
                            );
                        }
                    );
                    Text(
                        text: "X",
                        color: Color.white,
                        fontSize: 40,
                        fontWeight: FontWeight.Bold,
                        textAlign: TextAlign.MiddleCenter,
                        modifier: Modifier
                            .Name("remove-button")
                            .Background(Color.red)
                            .Padding(horizontal: 16, vertical: 4)
                            .Border(radius: 16)
                            .OnClick(onRemoveClick)
                    );
                }
            );
        }
    }
}