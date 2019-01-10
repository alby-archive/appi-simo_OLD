namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;

    public abstract class BaseComponent<TEntity> : BlazorComponent
        where TEntity : class, IEntity, new()
    {
        [Inject]
        protected EndPoint<TEntity> EndPoint { get; set; }

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