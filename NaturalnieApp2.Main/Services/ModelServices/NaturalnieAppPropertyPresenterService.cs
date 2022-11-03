namespace NaturalnieApp2.Main.Services.ModelServices
{
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class NaturalnieAppPropertyPresenterService
    {
        public static void ConfigureConverter(IModelToPropertyPresenterConverter converter)
        {
            converter?.AddPresenterForPropertyType<TestEnum>((name, context) =>
            {
                PropertyPresenterListViewModel model = new() { ProxyProperty = new(context, name) };

                foreach (string? element in Enum.GetNames(typeof(TestEnum)).ToList())
                {
                    model.HintList.Add(element);
                }

                return model;
            });

        }
    }
}
