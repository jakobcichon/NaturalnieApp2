namespace NaturalnieApp2.SharedInterfaces.Database
{
    using System.Collections;

    public interface IDatabaseGeneralCommands<T>
    {
        Task<ICollection<T>> GetAllElements();
    }
}
