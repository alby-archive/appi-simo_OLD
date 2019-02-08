namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.OData.Client;
    using Pager;
    using Services;

    public abstract class BasePagerComponent<TEntity> : BasePagerComponent<TEntity, EndPoint<TEntity>>
        where TEntity : class, IEntity, new()
    {
    }

    public abstract class BasePagerComponent<TEntity, TEndPoint> :BlazorComponent
        where TEntity : class, IEntity, new() 
        where TEndPoint : EndPoint<TEntity>
    {
        [Inject]
        protected TEndPoint EndPoint { get; set; }
        
        [Inject]
        protected BaseRxService<Pager> PagerService { get; set; }

        protected IEnumerable<TEntity> Entities = new List<TEntity>();
        protected int TotalItems { get; private set; }

        protected override void OnInit()
        {
            PagerService.Subscribe(async pager => await Call());
        }
        
        protected abstract IQueryable<TEntity> Selector(DataServiceQuery<TEntity> entities);

        async Task Call()
        {
            var builder = Selector(EndPoint.Entities.IncludeTotalCount());
            
            var response = await builder
                .Skip(PagerService.Value.CurrentPage * PagerService.Value.PageSize)
                .Take(PagerService.Value.PageSize)
                .ToListAsync(EndPoint.Client);

            Entities = response.Value;
            TotalItems = response.Count;

            StateHasChanged();
        }
    }
}