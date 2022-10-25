namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPropertyPresenterInputField: IPropertyPresenterDataField
    {
        public string? CustomMask
        {
            get
            {
                return "*";
            }
        }
    }
}
