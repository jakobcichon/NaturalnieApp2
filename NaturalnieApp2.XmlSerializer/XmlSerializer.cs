namespace NaturalnieApp2.XmlSerializer
{
    using Microsoft.Win32.SafeHandles;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using NaturalnieApp2.SharedInterfaces.Xml;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;
    using BaseSerializer = System.Xml.Serialization;

    public class XmlSerializer : IXmlSerializer
    {
        private string mainDirectory;

        public XmlSerializer(string mainDirectory, ILogger logger)
        {
            this.mainDirectory = mainDirectory;
        }

        public T Deserialize<T>(object source)
        {
            T retObject = default;

            string fullPath = GetFullPathToModel(source);
            Directory.CreateDirectory(GetModelDirectory(source));

            if(!File.Exists(fullPath))
            {
                return default;
            }

            using (Stream reader = File.OpenRead(fullPath))
            {
                using (XmlReader streamReader = XmlReader.Create(reader))
                {
                    BaseSerializer.XmlSerializer serializer = CreateSerializer(typeof(T));

                    retObject = (T)serializer.Deserialize(streamReader);
                }
            }

            return retObject;
        }

        public Task Serialize(object model, object source)
        {
            string fullPath = GetFullPathToModel(source);
            Directory.CreateDirectory(GetModelDirectory(source));

            using (Stream writer = File.OpenWrite(fullPath))
            {
                using (XmlTextWriter streamWriter = new XmlTextWriter(writer, Encoding.UTF8))
                {
                    streamWriter.Formatting = Formatting.Indented;

                    BaseSerializer.XmlSerializer serializer = CreateSerializer(model.GetType());

                    serializer.Serialize(streamWriter, model);
                }
            }

            return Task.CompletedTask;
        }

        private static BaseSerializer.XmlSerializer CreateSerializer(Type type)
        {
            return new(type, new Type[] { typeof(object) });
        }

        private string GetFullPathToModel(object source)
        {
            return Path.Combine(GetModelDirectory(source), GetModelName(source) + ".xml");
        }

        private string GetModelDirectory(object source)
        {
            Type type = source.GetType();
            if (type.Namespace is null)
            {
                return String.Empty;
            }

            string temp = Path.Combine(type.Namespace.Split("."));
            return Path.Combine(temp, this.mainDirectory);
        }

        private static string GetModelName(object source)
        {
            Type type = source.GetType();
            return type.Name;

        }
    }
}