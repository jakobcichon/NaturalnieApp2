namespace NaturalnieApp2.SharedInterfaces.Logger
{
    public interface ILogger
    {
        public void Info(string message);
        public void Warn(string message);  
        public void Error(string message);
        public void Exception(string message);
    }
}
