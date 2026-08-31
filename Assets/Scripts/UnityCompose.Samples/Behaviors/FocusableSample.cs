// ReSharper disable ArrangeNamespaceBody

using System.Collections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Samples.Behaviors
{
    internal partial class FocusableSample : ComposeUI
    {
        [Composable]
        protected override void Content()
        {
            var focusManager = LocalFocusManager.Current;
            StartCoroutine(InputCoroutine());
            Box(
                alignment: Alignment.Center,
                modifier: Modifier
                    .FillMaxSize(),
                content: () =>
                {
                    Row(
                        modifier: Modifier
                            .Background(Color.white)
                            .Clip(RoundedCornerShape(16.Dp()))
                            .Padding(all: 16.Dp()),
                        content: () =>
                        {
                            Repeat(3, it =>
                            {
                                Column(() =>
                                {
                                    FocusableItem($"{it} first", isDefault: it == 0);
                                    FocusableItem($"{it} second");
                                    FocusableItem($"{it} third");
                                    FocusableItem($"{it} fourth");
                                });
                            });
                        }
                    );
                }
            );

            return;

            IEnumerator InputCoroutine()
            {
                while (true)
                {
                    // if (Input.GetKeyDown(KeyCode.UpArrow))
                    //     focusManager.MoveFocus(FocusDirection.Up);
                    // else if (Input.GetKeyDown(KeyCode.DownArrow))
                    //     focusManager.MoveFocus(FocusDirection.Down);
                    // else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    //     focusManager.MoveFocus(FocusDirection.Left);
                    // else if (Input.GetKeyDown(KeyCode.RightArrow))
                    //     focusManager.MoveFocus(FocusDirection.Right);
                    yield return null;
                }
            }
        }

        [Composable]
        protected override void Preview()
        {
            Content();
        }

        [Composable]
        private static void FocusableItem(string name, bool isDefault = false)
        {
            var isFocused = Remember(() => MutableStateOf(false));
            var focusRequester = Remember(() => new FocusRequester());
            if (isDefault)
                SideEffect(0, focusRequester.RequestFocus);
            Spacer(
                Modifier
                    .Margin(all: 4.Dp())
                    .Size(
                        width: 100.Dp(),
                        height: 40.Dp()
                    )
                    .Clip(RoundedCornerShape(16.Dp()))
                    .Background(AnimateColorAsState(isFocused.Value ? Color.lightSeaGreen : Color.indianRed).Value)
                    .Scale(AnimateFloatAsState(isFocused.Value ? 1.1f : 1f).Value)
                    .Name(name)
                    .Focusable()
                    .OnFocusChanged(it => isFocused.Value = it.IsFocused)
                    .FocusRequester(focusRequester)
            );
        }
    }
}