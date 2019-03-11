namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;
    using Shared.Services;

    public class UserEndPoint : EndPoint<User>
    {
        public UserEndPoint(DataServiceContext context, HttpClient client, AuthService authService, string resourceUri)
            : base(context, client, authService, resourceUri)
        {
        }

        public async Task<decimal> GiveMeBackMyMoney(Guid key) =>
            await Client.GetJsonAsync<decimal>($"{ResourceUri}/GiveMeBackMyMoney?key={key}");
    }
}