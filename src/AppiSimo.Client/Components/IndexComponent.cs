
namespace AppiSimo.Client.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using AppiSimo.Shared.Model;
    using Clients;

    public class IndexComponent : BlazorComponent
    {
        [Inject]
        AppiSimoClient Client { get; set; }

        protected IEnumerable<User> Users { get; private set; } = new List<User>();

        protected override async Task OnInitAsync()
        {          
             Users = await Client.Users.OrderBy(user => user.Name).ToListAsync(Client._client);
        }
    }
}