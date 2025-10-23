using System;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class NameImpl : BaseModifier<NameImpl>
    {
        private readonly string _name;

        public NameImpl(string name)
        {
            _name = name;
        }

        public override void Apply(VisualElement element)
        {
            try
            {
                element.name = _name;
            }
            catch (Exception)
            {
            }
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Name);
        }

        public override void Revert(VisualElement element)
        {
            element.name = null;
        }

        protected override bool Equals(NameImpl other)
        {
            return _name == other._name;
        }
    }

    public static IModifier Name(this IModifier style, string name)
    {
        return style.Then(new NameImpl(name));
    }
}