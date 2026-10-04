using System;
using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose.Samples.Behaviors.LazyList;

public static partial class LazyColumnFunctions
{
    [Composable]
    public static ILazyListState RememberLazyListState()
    {
        return Remember(() => new LazyListStateImpl());
    }

    [Composable]
    public static void LazyColumn(
        Action<ILazyListScope> content,
        ILazyListState? state = null,
        IModifier? modifier = null,
        Arrangement.Vertical? verticalArrangement = null,
        Alignment.Horizontal? horizontalAlignment = null,
        bool userScrollEnabled = true
    )
    {
        state ??= RememberLazyListState();
        var mutableState = state as IMutableLazyListState;
        if (mutableState == null)
            return;
        Box(
            modifier: modifier.OrEmpty()
                .VerticalScroll(state),
            content: () =>
            {
                var scope = Remember(mutableState, () => new LazyListScopeImpl(mutableState));
                SideEffect(content, () =>
                {
                    mutableState.Clear();
                    content(scope);
                });
                Column(
                    verticalArrangement: verticalArrangement,
                    horizontalAlignment: horizontalAlignment,
                    content: () =>
                    {
                        foreach (var item in mutableState.Items)
                        {
                            Box(
                                modifier: Modifier.FillMaxWidth()
                                    .OnGloballyPositioned(it =>
                                    {
                                        mutableState.SyncPosition(item.Index, it.PositionInParent().Y);
                                    }),
                                content: item.Content
                            );
                        }
                    }
                );
            }
        );
    }
}