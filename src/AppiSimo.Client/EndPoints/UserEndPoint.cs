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

        public async Task Enable(Guid key) =>
            await Client.PostAsync($"{ResourceUri}/Enable?key={key}", content: null);
        
        public async Task Disable(Guid key) =>
            await Client.PostAsync($"{ResourceUri}/Disable?key={key}", content: null);
        
        public async Task ResetPassword(Guid key) =>
            await Client.PostAsync($"{ResourceUri}/ResetPassword?key={key}", content: null);
        
        public async Task<decimal> GiveMeBackMyMoney(Guid key) =>
            await Client.GetJsonAsync<decimal>($"{ResourceUri}/GiveMeBackMyMoney?key={key}");
    }
}