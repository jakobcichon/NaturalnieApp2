namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    using NaturalnieApp2.SharedInterfaces.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelToPropertyPresenterConverter
    {
        void AddPresenterForPropertyType(Type propType, IPropertyPresenter propertyPresenter);
        void AddPresenterForPropertyName(string propertyName, IPropertyPresenter propertyPresenter);
        IEnumerable<IPropertyPresenter> GetPropertyPresenterForModel(IModel model);
        IPropertyPresenter? GetPropertyPresenter(PropertyInfo propertyInfo);
    }
}
