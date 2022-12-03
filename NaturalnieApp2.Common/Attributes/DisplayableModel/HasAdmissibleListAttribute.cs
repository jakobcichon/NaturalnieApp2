namespace NaturalnieApp2.Common.Attributes.DisplayableModel
{
    using System;
    using System.Collections;
    using System.Diagnostics;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class HasAdmissibleListAttribute : Attribute
    {
        public string Name { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="propertyName">Property name that provides admissible list</param>
        public HasAdmissibleListAttribute(string propertyName)
        {
            Name = propertyName;
        }

    }
}
