namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;

    public abstract class BaseComponent<TEntity> : BlazorComponent
        where TEntity : class, IEntity, new()
    {
        [Inject]
        protected EndPoint<TEntity> EndPoint { get; set; }

        protected async Task<List<TEntity>> Get() => (await EndPoint.Entities.ToListAsync(EndPoint._client)).Value;

        protected async Task<TEntity> GetById(Guid id) => await EndPoint.Entity(id);

        protected async Task Save(TEntity entity)
        {
            await EndPoint.Save(entity);
        }

        protected async Task Delete(Guid id)
        {
            await EndPoint.Delete(id);
        }
    }
}