namespace NaturalnieApp2.Common.Attributes.DisplayableModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class DisplayableModel
    {
        [AttributeUsage(AttributeTargets.Property)]
        public sealed class CanBeDisplayed : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class NameToBeDisplayed : Attribute
        {
            public string Name { get; init; }

            public NameToBeDisplayed(string name)
            {
                Name = name;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class VisualRepresenation : Attribute
        {
            public VisualRepresenationType Type { get; init; }
            public bool VisiableByDefault { get; init; }

            public VisualRepresenation(VisualRepresenationType type, bool visiableByDefault)
            {
                Type = type;
                VisiableByDefault = visiableByDefault;
            }
        }

        public enum VisualRepresenationType
        {
            Default,
            List,
            Field,
            LongField,
        }
    }
}
