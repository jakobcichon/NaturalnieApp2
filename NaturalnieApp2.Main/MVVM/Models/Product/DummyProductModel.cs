namespace NaturalnieApp2.Main.MVVM.Models.Product
{
    public record DummyProductModel : DisplayableModelBase
    {
        private string name;

        public string Name
        {
            get 
            { 
                return name; 
            }
            set 
            { 
                name = value; 
            }
        }
        public int Price { get; set; }

        public double Margin { get; set; }

    }
}
