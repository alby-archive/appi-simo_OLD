namespace AppiSimo.Client.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Pager;
    using AppiSimo.Shared.Model;

    public class IndexComponent : PagerBaseComponent
    {
        [Inject]
        AppiSimoClient Client { get; set; }

        protected IEnumerable<User> Users { get; private set; } = new List<User>();

        protected override async Task OnInitAsync()
        {
            await GetUsers();
        }

        protected override async Task PageChangedHandler() => await GetUsers();
        
        async Task GetUsers()
        {
            var response = await Client.Users.Entities.IncludeTotalCount().OrderBy(user => user.Name).Skip(CurrentPage * PageSize).Take(PageSize).ToListAsync(Client.Client);

            Users = response.Value;
            TotalItems = response.Count;
        }
    }
}