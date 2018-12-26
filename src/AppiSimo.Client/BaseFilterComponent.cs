namespace AppiSimo.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public abstract class BaseFilterComponent<TEntity> : BlazorComponent
        where TEntity : class, IEntity, new()
    {
        [Inject]
        AppiSimoClient<TEntity> Client { get; set; }

        [Inject]
        protected BaseRxService<Pager> PagerService { get; set; }

        [Inject]
        protected BaseRxService<Searcher> SearcherService { get; set; }

        protected IEnumerable<TEntity> Entities = new List<TEntity>();

        protected int TotalItems { get; set; }

        protected override void OnInit()
        {
            SearcherService.Subscribe(searcher => PagerService.OnNext(new Pager()));
            PagerService.Subscribe(async pager => await Call());
        }

        protected abstract IQueryable<TEntity> Selector(IQueryable<TEntity> entities);

        async Task Call()
        {
            var endPoint = Client.GetEndPoint();

            var builder = endPoint.Entities.IncludeTotalCount().AsQueryable();

            builder = Selector(builder);

            var response = await builder
                .Skip(PagerService.Value.CurrentPage * PagerService.Value.PageSize)
                .Take(PagerService.Value.PageSize)
                .ToListAsync(Client.Client);

            Entities = response.Value;
            TotalItems = response.Count;

            StateHasChanged();
        }
    }
}