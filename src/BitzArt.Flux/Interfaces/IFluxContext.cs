﻿namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>(string? serviceName = null) where TEntity : class;
    public IFluxEntityContext<TEntity> Entity<TEntity>(string? serviceName = null) where TEntity : class;
    IFluxServiceContext Service(string serviceName);
}