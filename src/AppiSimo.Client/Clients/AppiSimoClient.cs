namespace AppiSimo.Client.Clients
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Abstract;
    using Microsoft.OData.Client;

    public class AppiSimoClient<TEntity>
        where TEntity : class, IEntity, new()
    {
        public readonly HttpClient Client;
        readonly DataServiceContext _context;

        public AppiSimoClient(HttpClient client, Uri baseUri)
        {
            Client = client;
            Client.BaseAddress = baseUri;

            _context = new DataServiceContext(baseUri);
        }

        public EndPoint<TEntity> GetEndPoint() => new EndPoint<TEntity>(_context, Client, $@"{typeof(TEntity).Name}s");
    }
}