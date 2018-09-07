namespace AppiSimo.Client.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public class IndexComponent : BlazorComponent
    {
        [Inject]
        AppiSimoClient Client { get; set; }

        protected BaseRxService<Pager> PagerService { get; set; } = new PagerService();
        protected BaseRxService<Searcher> SearcherService { get; set; } = new SearcherService();

        protected int TotalItems { get; private set; }
        protected IEnumerable<User> Users { get; private set; } = new List<User>();

        protected override void OnInit()
        {
            SearcherService.Subscribe(searcher => PagerService.OnNext(new Pager()));
            PagerService.Subscribe(async pager => await GetUsers());
        }

        async Task GetUsers()
        {
            var pager = PagerService.Value;

            var response = await Client.Users.Entities
                .IncludeTotalCount()
                .Where(user => user.Surname.Contains(SearcherService.Value.Filter))
                .OrderBy(user => user.Name)
                .Skip(pager.CurrentPage * pager.PageSize)
                .Take(pager.PageSize)
                .ToListAsync(Client.Client);

            Users = response.Value;
            TotalItems = response.Count;

            StateHasChanged();
        }
    }
}