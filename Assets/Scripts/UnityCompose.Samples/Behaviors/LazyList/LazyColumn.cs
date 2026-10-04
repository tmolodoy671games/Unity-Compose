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
        var scope = Remember(mutableState, () => new LazyListScopeImpl(mutableState));
        SideEffect(content, () =>
        {
            mutableState.Clear();
            content(scope);
        });
        Column(
            verticalArrangement: verticalArrangement,
            horizontalAlignment: horizontalAlignment,
            modifier: modifier.OrEmpty()
                .VerticalScroll(state),
            content: () =>
            {
                for (var i = 0; i < mutableState.Items.Count; i++)
                {
                    var item = mutableState.Items[i];
                    Box(
                        modifier: Modifier.FillMaxWidth()
                            .OnPlaced(it =>
                                mutableState.SyncPosition(item.Index, it.PositionInParent().Y)
                            ),
                        content: item.Content
                    );
                }
            }
        );
    }

    [Composable]
    public static void LazyRow(
        Action<ILazyListScope> content,
        ILazyListState? state = null,
        IModifier? modifier = null,
        Alignment.Vertical? verticalAlignment = null,
        Arrangement.Horizontal? horizontalArrangement = null,
        bool userScrollEnabled = true
    )
    {
        state ??= RememberLazyListState();
        var mutableState = state as IMutableLazyListState;
        if (mutableState == null)
            return;
        var scope = Remember(mutableState, () => new LazyListScopeImpl(mutableState));
        SideEffect(content, () =>
        {
            mutableState.Clear();
            content(scope);
        });
        Row(
            verticalAlignment: verticalAlignment,
            horizontalArrangement: horizontalArrangement,
            modifier: modifier.OrEmpty()
                .HorizontalScroll(state),
            content: () =>
            {
                for (var i = 0; i < mutableState.Items.Count; i++)
                {
                    var item = mutableState.Items[i];
                    Box(
                        modifier: Modifier.FillMaxWidth()
                            .OnPlaced(it =>
                                mutableState.SyncPosition(item.Index, it.PositionInParent().X)
                            ),
                        content: item.Content
                    );
                }
            }
        );
    }
}