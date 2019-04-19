namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.OData.Client;
    
    public abstract class BaseDetailComponent<TEntity> : BaseDetailComponent<TEntity, EndPoint<TEntity>>
        where TEntity : class, IEntity, new()
    {}

    public abstract class BaseDetailComponent<TEntity, TEndPoint> : BlazorComponent
        where TEntity : class, IEntity, new() where TEndPoint : EndPoint<TEntity>
    {
        [Parameter]
        string Id { get; set; }

        [Inject]
        protected TEndPoint EndPoint { get; set; }

        protected TEntity Entity { get; set; } = new TEntity();

        protected abstract DataServiceQuery<TEntity> Selector(DataServiceQuery<TEntity> entity);

        protected override async Task OnInitAsync()
        {
            if ((Id != null) & Guid.TryParse(Id, out var id))
            {
                Entity = await EndPoint.Entity(id, Selector);
            }
        }

        protected virtual Task Save() => EndPoint.Save(Entity);

        protected virtual Task Delete() => EndPoint.Delete(Entity.Id);
    }
}