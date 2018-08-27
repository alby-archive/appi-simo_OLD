namespace AppiSimo.Client.Clients
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;

    public class AppiSimoClient
    {
        public readonly HttpClient Client;
        readonly DataServiceContext _context;

        public AppiSimoClient(HttpClient client, Uri baseUri)
        {
            Client = client;
            Client.BaseAddress = baseUri;
            
            _context = new DataServiceContext(baseUri);
        }
        
        public EndPoint<User> Users => new EndPoint<User>(_context, Client, "users");
    }
}