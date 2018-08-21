namespace AppiSimo.Client.Clients
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;

    public class AppiSimoClient
    {
        public readonly HttpClient _client;
        readonly DataServiceContext _context;

        public AppiSimoClient(HttpClient client, Uri baseUri)
        {
            _client = client;
            _client.BaseAddress = baseUri;
            
            _context = new DataServiceContext(baseUri);
        }
        
        public EndPoint<User> Users => new EndPoint<User>(_context, "users", _client);
    }
}