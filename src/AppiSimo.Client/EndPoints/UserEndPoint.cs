namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

    public class UserEndPoint : EndPoint<User>
    {
        public UserEndPoint(DataServiceContext context, HttpClient client, string resourceUri)
            : base(context, client, resourceUri)
        {
        }

        public async Task<decimal> GiveMeBackMyMoney(Guid key) => 
            await Client.GetJsonAsync<decimal>($"/odata/{ResourceUri}/GiveMeBackMyMoney?key={key}");
    }
}