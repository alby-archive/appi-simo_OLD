namespace AppiSimo.Client.Clients
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

    public class EndPoint<TEntity> : IEndPoint<TEntity>
        where TEntity : class, IEntity, new()
    {
        readonly HttpClient _client;

        readonly DataServiceContext _context;
        readonly string _resourceUri;

        public EndPoint(DataServiceContext context, HttpClient client, string resourceUri)
        {
            _context = context;
            _client = client;

            _resourceUri = resourceUri;
        }

        public DataServiceQuery<TEntity> Entities => _context.CreateQuery<TEntity>(_resourceUri);

        public async Task Save(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                await _client.SendJsonAsync<TEntity>(HttpMethod.Post, _resourceUri, entity);
            }
            else
            {
                await _client.SendJsonAsync<TEntity>(HttpMethod.Put, $"{_resourceUri}({entity.Id})", entity);
            }
        }

        public async Task Remove(Guid id) => await _client.DeleteAsync($"{_resourceUri}/{id}");
    }
}