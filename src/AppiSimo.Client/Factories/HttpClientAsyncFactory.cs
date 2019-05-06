namespace AppiSimo.Client.Factories
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Abstract;
    using Shared.Services.Abstract;

    public class HttpClientAsyncFactory : IAsyncFactory<HttpClient>
    {
        readonly IAsyncFactory<IAuthService> _factory;
        readonly HttpMessageHandler _handler;
        readonly Uri _baseAddress;

        public HttpClientAsyncFactory(IAsyncFactory<IAuthService> factory, HttpMessageHandler handler, Uri baseAddress)
        {
            _factory = factory;
            _handler = handler;
            _baseAddress = baseAddress;
        }

        public async Task<HttpClient> CreateAsync()
        {
            var _auth = await _factory.CreateAsync();

            var client = _handler != null ? new HttpClient(_handler) : new HttpClient();

            client.BaseAddress = _baseAddress;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{_auth?.CurrentUser?.IdToken ?? "NULL"}");

            return client;
        }
    }
}