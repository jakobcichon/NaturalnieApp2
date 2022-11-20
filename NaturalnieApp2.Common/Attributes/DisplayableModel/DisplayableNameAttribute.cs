namespace NaturalnieApp2.Common.Attributes.DisplayableModel
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DisplayableNameAttribute: Attribute
    {
        public string DisplayName { get; set; }

        public DisplayableNameAttribute(string displayName)
        {
            this.DisplayName = displayName; 
        }
    }
}
