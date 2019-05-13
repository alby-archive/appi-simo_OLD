namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Factories.Abstract;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

    public class EndPoint<TEntity> : IEndPoint<TEntity>
        where TEntity : class, IEntity, new()
    {
        public readonly IAsyncFactory<HttpClient> _factory;

        readonly DataServiceContext _context;
        protected readonly string _uri;

        public EndPoint(DataServiceContext context, IAsyncFactory<HttpClient> factory, string uri)
        {
            _context = context;
            _factory = factory;
            _uri = uri;
        }

        public DataServiceQuery<TEntity> Entities => _context.CreateQuery<TEntity>(_uri);

        public async Task<TEntity> Entity(Guid id, Func<DataServiceQuery<TEntity>, IQueryable<TEntity>> selector) =>
            (await selector(Entities).Where(u => u.Id == id).ToListAsync(await _factory.CreateAsync())).Value.FirstOrDefault();

        public async Task Save(TEntity entity)
        {
            var factory = await _factory.CreateAsync();

            if (entity.Id == Guid.Empty)
            {
                await factory.SendJsonAsync<TEntity>(HttpMethod.Post, $"{_uri}/Post", entity);
            }
            else
            {
                await factory.SendJsonAsync<TEntity>(HttpMethod.Put, $"{_uri}/Put", entity);
            }
        }

        public async Task Delete(Guid id) =>
            await (await _factory.CreateAsync()).DeleteAsync($"{_uri}/Delete/{id}");
    }
}