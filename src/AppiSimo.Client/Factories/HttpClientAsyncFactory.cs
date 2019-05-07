namespace AppiSimo.Client.Factories
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Abstract;
    using Environment;
    using Shared.Services.Abstract;

    public class HttpClientAsyncFactory : IAsyncFactory<HttpClient>
    {
        readonly IAsyncFactory<IAuthService> _factory;
        readonly HttpMessageHandler _handler;
        readonly Api _api;

        public HttpClientAsyncFactory(IAsyncFactory<IAuthService> factory, HttpMessageHandler handler, Api api)
        {
            _factory = factory;
            _handler = handler;
            _api = api;
        }

        public async Task<HttpClient> CreateAsync()
        {
            var auth = await _factory.CreateAsync();

            var client = _handler != null ? new HttpClient(_handler) : new HttpClient();

            client.BaseAddress = _api.Url;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{auth?.CurrentUser?.IdToken ?? "NULL"}");

            return client;
        }
    }
}