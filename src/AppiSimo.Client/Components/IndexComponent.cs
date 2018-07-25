
namespace AppiSimo.Client.Components
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using AppiSimo.Shared.Model;

    public class IndexComponent : BlazorComponent
    {
        [Inject]
        HttpClient Http { get; set; }

        protected IEnumerable<User> Users { get; private set; } = new List<User>();

        protected override async Task OnInitAsync()
        {
            Users = await Http.GetJsonAsync<IEnumerable<User>>("http://localhost:5001/api/users");
        }
    }
}