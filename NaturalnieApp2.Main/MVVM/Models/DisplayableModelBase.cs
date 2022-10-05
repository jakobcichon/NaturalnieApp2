namespace NaturalnieApp2.Main.MVVM.Models
{
    using NaturalnieApp2.SharedInterfaces.Models;
    using System;

    public record DisplayableModelBase : IModel
    {
        public event EventHandler? ModelUpdated;

        public void OnModelUpdated()
        {
            ModelUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
