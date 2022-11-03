namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelToPropertyPresenterConverter
    {
        void AddPresenterForPropertyType<T>(IPropertyPresenterDataField propertyPresenter);
        void AddPresenterForPropertyType<T>(Func<string, object, IPropertyPresenterDataField> func);
        void AddPresenterForPropertyName(string propertyName, IPropertyPresenterDataField propertyPresenter);
        IEnumerable<IPropertyPresenter> GetPropertyPresenterForModel(object model);
        IPropertyPresenter? GetPropertyPresenter(PropertyInfo propertyInfo, object model);
    }
}
