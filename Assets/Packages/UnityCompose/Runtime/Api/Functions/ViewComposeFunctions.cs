using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using Box = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Box;
using Column = UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.Column;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private static readonly ICompositionLocal<(IModifier? Before, IModifier? After)> LocalStyle =
        CompositionLocalOf<(IModifier? Before, IModifier? After)>(() => (null, null));

    public static readonly ICompositionLocal<VisualElement> LocalVisualElement =
        CompositionLocalOf<VisualElement>(() => throw new ArgumentException("No LocalVisualElement provided!"));

    public static CompositionLocalProvides Provides(
        this ICompositionLocal<(IModifier? Before, IModifier? After)> localStyle,
        IModifier? before = null,
        IModifier? after = null
    )
    {
        return localStyle.Provides((before, after));
    }

    [Composable]
    public static void ReusableComposeView<T>(
        IModifier? style = null,
        Action<T>? initializer = null,
        [Composable] Action? content = null
    ) where T : VisualElement, new()
    {
        var resolvedStyle = style;
        var localStyle = LocalStyle.Current;
        if (localStyle.Before != null)
            resolvedStyle = localStyle.Before.Then(resolvedStyle.OrEmpty());
        if (localStyle.After != null)
            resolvedStyle = resolvedStyle.OrEmpty().Then(localStyle.After);
        var visualElement = CurrentComposer.GetOrCreateVisualElement<T>();

        var currentStyle = Remember(() => IMutableStableProperty.Create<IModifier?>(null));
        var currentProperties = Remember(() =>
            IMutableStableProperty.Create<IStableSet<ComposeModifiedProperty>>(
                IImmutableStableSet.Empty<ComposeModifiedProperty>()
            )
        );
        var newProperties = IMutableStableSet.Create<ComposeModifiedProperty>();
        resolvedStyle?.Apply(newProperties);
        var propertiesToRevert = currentProperties.Value
            .Where(it => !newProperties.Contains(it));
        foreach (var property in propertiesToRevert)
            property.Revert(visualElement);
        currentProperties.Value = newProperties;
        currentStyle.Value = resolvedStyle;
        visualElement.ClearCallbacks();
        visualElement.style.transitionDelay.value?.Clear();
        visualElement.style.transitionDuration.value?.Clear();
        visualElement.style.transitionProperty.value?.Clear();
        visualElement.style.transitionTimingFunction.value?.Clear();
        visualElement.pickingMode = PickingMode.Ignore;
        visualElement.style.overflow = Overflow.Visible;
        resolvedStyle?.Apply(visualElement);

        if (initializer != null)
        {
            var currentInitializer = Remember(() => IMutableStableProperty.Create<Action<T>?>(null));
            if (currentInitializer.Value != initializer)
            {
                currentInitializer.Value = initializer;
                initializer(visualElement);
            }
        }

        if (content != null)
        {
            CompositionLocalProvider(
                provides: IImmutableStableList.Create(
                    LocalStyle.Provides((null, null)),
                    LocalVisualElement.Provides(visualElement)
                ),
                content: content
            );
        }
    }

    [Composable]
    public static void Column(
        [Composable] Action<IColumnScope> content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Column>(
            style: style,
            initializer: it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            },
            content: () =>
            {
                var scope = Remember(() => new ColumnScopeImpl());
                content(scope);
            }
        );
    }

    [Composable]
    public static void Column(
        [Composable] Action content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        Column(
            style: style,
            horizontalAlignment: horizontalAlignment,
            verticalAlignment: verticalAlignment,
            content: _ => content()
        );
    }

    [Composable]
    public static void Row(
        [Composable] Action<IRowScope> content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Row>(
            style: style,
            initializer: it =>
            {
                it.style.flexDirection = FlexDirection.Row;
                it.style.alignItems = verticalAlignment.ToAlign();
                it.style.justifyContent = horizontalAlignment.ToJustify();
            },
            content: () =>
            {
                var scope = Remember(() => new RowScopeImpl());
                content(scope);
            }
        );
    }

    [Composable]
    public static void Row(
        [Composable] Action content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        Row(
            style: style,
            horizontalAlignment: horizontalAlignment,
            verticalAlignment: verticalAlignment,
            content: _ => content()
        );
    }

    [Composable]
    public static void Box(
        [Composable] Action<IBoxScope> content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        ReusableComposeView<Box>(
            style: style,
            initializer: it =>
            {
                it.style.alignItems = horizontalAlignment.ToAlign();
                it.style.justifyContent = verticalAlignment.ToJustify();
            },
            content: () =>
            {
                var scope = Remember(() => new BoxScopeImpl());
                content(scope);
            }
        );
    }

    [Composable]
    public static void Box(
        [Composable] Action content,
        IModifier? style = null,
        Alignment.Horizontal horizontalAlignment = Alignment.Horizontal.Left,
        Alignment.Vertical verticalAlignment = Alignment.Vertical.Top
    )
    {
        Box(
            style: style,
            horizontalAlignment: horizontalAlignment,
            verticalAlignment: verticalAlignment,
            content: _ => content()
        );
    }

    [Composable]
    public static void Spacer(
        IModifier style
    )
    {
        ReusableComposeView<Spacer>(
            style: style
        );
    }

    [Composable]
    public static void Text(
        string text,
        Optional<TextStyle> textStyle = default,
        Optional<float> fontSize = default,
        Optional<FontStyle> fontStyle = default,
        Optional<Color> textColor = default,
        WhiteSpace whiteSpace = WhiteSpace.Normal,
        TextAnchor align = TextAnchor.UpperLeft,
        IModifier? style = null
    )
    {
        ReusableComposeView<Text>(
            style: style,
            initializer: it =>
            {
                it.text = text;
                it.style.whiteSpace = whiteSpace;
                it.style.unityFontStyleAndWeight = fontStyle
                    .GetOrDefault(textStyle.HasValue ? textStyle.Value.FontStyle : FontStyle.Normal);
                it.style.unityTextAlign = align;
                it.style.fontSize = fontSize.GetOrDefault(textStyle.HasValue ? textStyle.Value.FontSize : 14f);
                it.style.color = textColor.GetOrDefault(textStyle.HasValue ? textStyle.Value.Color : Color.white);
            }
        );
    }

    [Composable]
    public static void Image(
        Background image,
        Color? tint = null,
        IModifier? style = null
    )
    {
        ReusableComposeView<Image>(
            initializer: it =>
            {
                it.sprite = image.sprite;
                it.image = image.renderTexture as Texture ?? image.texture;
                it.tintColor = tint ?? Color.white;
            },
            style: style
        );
    }
}