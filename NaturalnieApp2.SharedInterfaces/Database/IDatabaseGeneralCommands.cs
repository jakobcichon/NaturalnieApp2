﻿namespace NaturalnieApp2.SharedInterfaces.Database
{
    using System.Collections;

    public interface IDatabaseGeneralCommands<T>: IDisposable
    {
        Task<ICollection<T>> GetAllElementsAsync();
        Task SaveAsync();
        Task EditAsync(T entity);
        Task AddAsync(T entity);
    }
}
