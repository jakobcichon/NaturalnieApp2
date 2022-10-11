namespace NaturalnieApp2.Common.Tests.Properties
{
    using NaturalnieApp2.Common.Properties;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    public enum DummyEnum
    {
        One,
        Two,
        Three
    }

    public record DummyModel
    {
        public DummyModel()
        {
            TaxList = new();
        }

        public string? Name { get; set; }
        public double Price { get; set; }
        public int Margin { get; set; }
        public int Tax { get; set; }
        public List<int> TaxList { get; set; }
        public DummyEnum Options { get; set; }

    }

    public class ProxyPropertyServiceTests
    {
        DummyModel dummyModel;
        public List<ProxyPropertyService> proxyProperties;

        [SetUp]
        public void SetupClass()
        {
            this.dummyModel = new DummyModel();
            this.proxyProperties = CreateProxies(this.dummyModel);
        }

        [TestCase]
        public void GetValue_GetValueThroughtProxy_ValueFromModelReturned()
        {
            string value = "TestName";
            dummyModel.Name = value;

            string valueUnderTest = classUnderTest.GetValue()?.ToString() ?? string.Empty;

            Assert.That(valueUnderTest, Is.EqualTo(value));

        }

        [TestCase]
        public void SetValue_SetValueThroughtProxy_ValueSavedToModel()
        {
            string value = "TestName";

            classUnderTest.SetValue(value);

            Assert.That(dummyModel.Name, Is.EqualTo(value));


        }

        private List<ProxyPropertyService> CreateProxies(object model)
        {
            List<ProxyPropertyService> retList = new();

            List<PropertyInfo>? propertyInfos = model?.GetType().GetProperties().ToList() ?? null;

            if(propertyInfos == null)
            {
                return retList;
            }

            foreach(PropertyInfo propertyInfo in propertyInfos)
            {
                retList.Add(new()
                {
                    PropertyName = propertyInfo.Name,
                    PropertyOwnerObject = model
                });
            }

            return retList;
        }

        private object? GetDummyValueToSet(ProxyPropertyService proxy)
        {
            Type? propertyType = proxy.PropertyOwnerObject?.GetType().GetProperty(proxy.PropertyName)?.PropertyType ?? null;

            if(propertyType == null)
            {
                return null;
            }

            if(ty)
        }


    }
}
