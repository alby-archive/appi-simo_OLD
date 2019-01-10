namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Microsoft.AspNetCore.Blazor.Components;

    public abstract class BaseDetailComponent<TEntity> : BaseComponent<TEntity>
        where TEntity : class, IEntity, new()
    {
        [Parameter]
        string Id { get; set; }

        protected TEntity Entity { get; private set; } = new TEntity();

        protected override async Task OnInitAsync()
        {
            if ((Id != null) & Guid.TryParse(Id, out var id))
            {
                Entity = await EndPoint.Entity(id);
            }
        }

        protected virtual Task Save() => base.Save(Entity);

        protected virtual Task Delete() => base.Delete(Entity.Id);
    }
}