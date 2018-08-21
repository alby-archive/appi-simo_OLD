namespace AppiSimo.Client.Clients
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Model;

    public class AppiSimoClient
    {
        public readonly HttpClient _client;
        readonly Uri _baseUri;

        public AppiSimoClient(HttpClient client, Uri baseUri)
        {
            _client = client;
            _baseUri = baseUri;
        }
        
        public EndPoint<User> Users => new EndPoint<User>(_baseUri, "users");
    }
}