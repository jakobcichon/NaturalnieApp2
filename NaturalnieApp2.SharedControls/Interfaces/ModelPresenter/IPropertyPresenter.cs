namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    public interface IPropertyPresenter
    {
        public string? HeaderText { get; set; }
        public IPropertyPresenterDataField? PropertyPresenterDataField { get; set; }
    }
}
