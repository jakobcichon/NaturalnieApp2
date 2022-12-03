namespace NaturalnieApp2.Common.Attributes.DisplayableModel
{
    using System;

    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Class,
        AllowMultiple = false)]
    public sealed class DisplayableNameAttribute: Attribute
    {
        public string DisplayName { get; set; }

        public DisplayableNameAttribute(string displayName)
        {
            this.DisplayName = displayName; 
        }
    }
}
