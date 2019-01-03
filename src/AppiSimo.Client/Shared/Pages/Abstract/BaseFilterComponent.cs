namespace AppiSimo.Client.Shared.Pages.Abstract
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Pager;
    using Searcher;
    using Services;
    using Shared;

    public abstract class BaseFilterComponent<TEntity> : BaseComponent<TEntity>
        where TEntity : class, IEntity, new()
    {
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

        protected abstract IQueryable<TEntity> Selector(IQueryable<TEntity> entities);

        async Task Call()
        {
            var builder = EndPoint.Entities.IncludeTotalCount().AsQueryable();

            builder = Selector(builder);

            var response = await builder
                .Skip(PagerService.Value.CurrentPage * PagerService.Value.PageSize)
                .Take(PagerService.Value.PageSize)
                .ToListAsync(EndPoint._client);

            Entities = response.Value;
            TotalItems = response.Count;

            StateHasChanged();
        }

    }
}