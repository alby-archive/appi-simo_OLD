namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Factories.Abstract;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

    public class UserEndPoint : EndPoint<User>
    {
        public UserEndPoint(DataServiceContext context, IAsyncFactory<HttpClient> factory, string uri)
            : base(context, factory, uri)
        {
        }

        public async Task Enable(Guid key) =>
            await (await _factory.CreateAsync()).PostAsync($"{_uri}/Enable?key={key}", content: null);

        public async Task Disable(Guid key) =>
            await (await _factory.CreateAsync()).PostAsync($"{_uri}/Disable?key={key}", content: null);

        public async Task ResetPassword(Guid key) =>
            await (await _factory.CreateAsync()).PostAsync($"{_uri}/ResetPassword?key={key}", content: null);

        public async Task<decimal> GiveMeBackMyMoney(Guid key) =>
            await (await _factory.CreateAsync()).GetJsonAsync<decimal>($"{_uri}/GiveMeBackMyMoney?key={key}");
    }
}