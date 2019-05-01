namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.OData.Client;
    using Pager;
    using Searcher;
    using Services;

    public abstract class BaseFilterComponent<TEntity> : BaseFilterComponent<TEntity, EndPoint<TEntity>>
        where TEntity : class, IEntity, new()
    {
    }

    public abstract class BaseFilterComponent<TEntity, TEndPoint> : BlazorComponent, IDisposable
        where TEntity : class, IEntity, new()
        where TEndPoint : EndPoint<TEntity>
    {
        [Inject]
        protected TEndPoint EndPoint { get; set; }

        [Inject]
        protected BaseRxService<Pager> PagerService { get; set; }

        [Inject]
        protected BaseRxService<Searcher> SearcherService { get; set; }

        protected IEnumerable<TEntity> Entities = new List<TEntity>();
        protected int TotalItems { get; private set; }

        protected override void OnInit()
        {
            SearcherService.Subscribe(searcher => PagerService.OnNext(new Pager()));
            PagerService.Subscribe(async pager => await Call());
        }

        protected abstract IQueryable<TEntity> Selector(DataServiceQuery<TEntity> entities, Searcher searcher);

        async Task Call()
        {
            var builder = Selector(EndPoint.Entities.IncludeTotalCount(), SearcherService.Value);

            var response = await builder
                .Skip(PagerService.Value.CurrentPage * PagerService.Value.PageSize)
                .Take(PagerService.Value.PageSize)
                .ToListAsync(EndPoint._client);

            Entities = response.Value;
            TotalItems = response.Count;

            StateHasChanged();
        }

        public void Dispose()
        {
            SearcherService.Dispose();
            PagerService.Dispose();
        }
    }
}