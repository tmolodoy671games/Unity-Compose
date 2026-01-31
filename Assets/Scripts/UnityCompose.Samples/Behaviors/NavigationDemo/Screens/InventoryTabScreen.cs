namespace UnityCompose.Samples.Behaviors.NavigationDemo.Screens;

internal partial class InventoryTabScreen : ComposeScreen
{
    [Composable]
    public override void Content(IModifier modifier)
    {
        Spacer(
            modifier
                .FillMaxSize()
        );
        // Box(
        //     alignment: Alignment.Center,
        //     modifier: modifier.OrEmpty()
        //         .FillMaxSize(),
        //     content: () =>
        //     {
        //         Column(
        //             modifier: Modifier
        //                 .Background(Color.black)
        //                 .Padding(8.Px())
        //                 .Border(16.Px()),
        //             content: () =>
        //             {
        //                 Row(() =>
        //                 {
        //                     InventoryItem();    
        //                     InventoryItem();    
        //                     InventoryItem();
        //                     InventoryItem();
        //                 });
        //                 Row(() =>
        //                 {
        //                     InventoryItem();    
        //                     InventoryItem();    
        //                     InventoryItem();
        //                     InventoryItem();
        //                 });
        //             }
        //         );
        //     }
        // );
    }

    [Composable]
    private static void InventoryItem()
    {
        Spacer(
            Modifier
                .Size(100.Px())
                .Border(16.Px())
                .Background(Color.grey)
                .Margin(2.Px())
        );
    }
}