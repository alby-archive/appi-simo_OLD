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

        public async Task Enable(Guid key) =>
            await _client.PostAsync($"{_resourceUri}/Enable?key={key}", content: null);

        public async Task Disable(Guid key) =>
            await _client.PostAsync($"{_resourceUri}/Disable?key={key}", content: null);

        public async Task ResetPassword(Guid key) =>
            await _client.PostAsync($"{_resourceUri}/ResetPassword?key={key}", content: null);

        public async Task<decimal> GiveMeBackMyMoney(Guid key) =>
            await _client.GetJsonAsync<decimal>($"{_resourceUri}/GiveMeBackMyMoney?key={key}");
    }
}