namespace AppiSimo.Client.Clients
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;
    using AppiSimo.Shared.Abstract;

    public class EndPoint<TEntity> : IEndPoint<TEntity>
        where TEntity : class, IEntity, new()
    {
        readonly HttpClient _client;
        readonly DataServiceContext _context;
        readonly string _resourceUri;
        readonly DataServiceQuery<TEntity> _queryable;

        public EndPoint(DataServiceContext context, string resourceUri, HttpClient client)
        {
            _context = context;
            _resourceUri = resourceUri;
            
            _queryable = context.CreateQuery<TEntity>(_resourceUri);

            _client = client;
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;

        public IEnumerator<TEntity> GetEnumerator() => throw new NotSupportedException();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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