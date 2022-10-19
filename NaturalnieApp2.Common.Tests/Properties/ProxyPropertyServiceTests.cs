namespace NaturalnieApp2.Common.Tests.Properties
{
    using NaturalnieApp2.Common.Properties;
    using Newtonsoft.Json.Linq;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public enum DummyEnum
    {
        One,
        Two,
        Three
    }

    public record DummyModel : INotifyPropertyChanged
    {
        private string? name;
        private double price;
        private int margin;
        private int tax;
        private List<int> taxList;
        private DummyEnum options;
        private bool onTheStock;


        public DummyModel()
        {
            TaxList = new();
        }

        private int myVar;

        public string? Name
        {
            get 
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public int Margin
        {
            get
            {
                return margin;
            }

            set
            {
                margin = value;
                OnPropertyChanged();
            }
        }
        public int Tax
        {
            get
            {
                return tax;
            }

            set
            {
                tax = value;
                OnPropertyChanged();
            }
        }
        public List<int> TaxList
        {
            get
            {
                return taxList;
            }

            set
            {
                taxList = value;
                OnPropertyChanged();
            }
        }
        public DummyEnum Options
        {
            get
            {
                return options;
            }

            set
            {
                options = value;
                OnPropertyChanged();
            }
        }

        public bool OnTheStock
        {
            get
            {
                return onTheStock;
            }

            set
            {
                onTheStock = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
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
            Dictionary<string, object?> setValues = FillModelWithDummyValues(this.dummyModel);

            foreach(ProxyPropertyService proxy in this.proxyProperties)
            {
                var proxVal = proxy.GetValue();
                var modelVal = setValues.GetValueOrDefault(proxy.PropertyName);
                Assert.That(proxVal, Is.EqualTo(modelVal));
            }
        }

        [TestCase]
        public void SetValue_SetValueThroughtProxy_ValueSavedToModel()
        {
            Dictionary<string, object?> dummyValues = GetModelDummyValues(this.dummyModel);

            foreach (ProxyPropertyService proxy in this.proxyProperties)
            {
                var dummyValue = dummyValues.GetValueOrDefault(proxy.PropertyName);
                proxy.SetValue(dummyValue);

                var modelValue = this.dummyModel.GetType().GetProperty(proxy.PropertyName)?.GetValue(this.dummyModel);

                Assert.That(dummyValue, Is.EqualTo(modelValue));
            }
        }

        [TestCase]
        public void PropertyChanged_SetValueOnModel_ProxyPropertyChangedCalled()
        {
            Dictionary<string, object?> dummyValues = GetModelDummyValues(this.dummyModel);

            foreach (ProxyPropertyService proxy in this.proxyProperties)
            {
                string? propertyNameFromCallback = string.Empty;

                proxy.PropertyChanged += (s, e) =>
                {
                    propertyNameFromCallback = e.PropertyName;
                };

                var dummyValue = dummyValues.GetValueOrDefault(proxy.PropertyName);
                proxy.SetValue(dummyValue);

                Assert.That(propertyNameFromCallback, Is.EqualTo(proxy.PropertyName));
            }
        }

        private void Proxy_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
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
                    PropertyContext = model
                });
            }

            return retList;
        }

        private Dictionary<string, object?> FillModelWithDummyValues(object Model)
        {
            Dictionary<string, object?> setValues = new();
            List<PropertyInfo>? properties = Model?.GetType().GetProperties().ToList();

            if(properties == null)
            {
                return setValues;
            }

            foreach(PropertyInfo prop in properties)
            {
                object? value = GetDummyValueToSet(prop.PropertyType);
                prop.SetValue(Model, value);
                setValues.Add(prop.Name, value);
            }

            return setValues;
        }

        private Dictionary<string, object?> GetModelDummyValues(object Model)
        {
            Dictionary<string, object?> setValues = new();
            List<PropertyInfo>? properties = Model?.GetType().GetProperties().ToList();

            if (properties == null)
            {
                return setValues;
            }

            foreach (PropertyInfo prop in properties)
            {
                object? value = GetDummyValueToSet(prop.PropertyType);
                setValues.Add(prop.Name, value);
            }

            return setValues;
        }

        private object? GetDummyValueToSet(Type propertyType)
        {
            if (propertyType == null)
            {
                return null;
            }

            if(IsString(propertyType))
            {
                Random random = new Random(20);
                return RandomString(random.Next(255));
            }

            if(propertyType.IsEnum)
            {
                Enum value = GetRandomEnumValue(propertyType);
                return value;
            }

            var test = propertyType.GetInterfaces();

            if (propertyType.IsPrimitive && propertyType.GetInterfaces().Any( i => i.Name == nameof(IConvertible)))
            {
                Random random = new Random();

                if (IsNumeric(propertyType) && !IsDecimal(propertyType))
                {

                    int randomValue = random.Next();
                    return (randomValue as IConvertible).ToType(propertyType, null);
                }

                if (IsDecimal(propertyType))
                {
                    double randomValue = random.Next(2000);
                    double randomValueD = random.NextSingle();
                    randomValue += randomValueD;
                    return (randomValue as IConvertible).ToType(propertyType, null);
                }

                if(IsBoolean(propertyType))
                {
                    return random.Next(9) % 3 == 0 ? true : false;
                }
            }

            return null;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static Enum GetRandomEnumValue(Type type)
        {
            Random random = new Random();

            var v = Enum.GetValues(type);
            return (Enum)v?.GetValue(random.Next(v.Length));
        }


        bool IsNumeric(Type type)
            => type == typeof(decimal)
            || type == typeof(int)
            || type == typeof(uint)
            || type == typeof(long)
            || type == typeof(ulong)
            || type == typeof(short)
            || type == typeof(ushort)
            || type == typeof(byte)
            || type == typeof(sbyte)
            || type == typeof(float)
            || type == typeof(double);

        bool IsDecimal(Type type)
            => type == typeof(decimal)
            || type == typeof(float)
            || type == typeof(double);

        bool IsBoolean(Type type)
            => type == typeof(bool);

        bool IsString(Type type)
            => type == typeof(string);


    }
}
