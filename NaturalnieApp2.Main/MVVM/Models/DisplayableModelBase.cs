namespace NaturalnieApp2.Main.MVVM.Models
{
    using NaturalnieApp2.SharedInterfaces.Models;
    using System;

    public record DisplayableModelBase : IModel
    {
        public event EventHandler? ModelUpdated;

        public object? GetValue(string propertyName)
        {
            var test = this.GetType().UnderlyingSystemType?.GetProperty(propertyName)?.GetValue(this);
            return test;
        }

        public void OnModelUpdated()
        {
            ModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
