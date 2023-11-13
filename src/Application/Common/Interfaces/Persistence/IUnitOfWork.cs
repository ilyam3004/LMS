﻿namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITaskRepository Tasks { get; }
    IGroupRepository Groups { get; }
    IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync();
    void Dispose();
}