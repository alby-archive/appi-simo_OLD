namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

    public class EndPoint<TEntity> : IEndPoint<TEntity>
        where TEntity : class, IEntity, new()
    {
        public readonly HttpClient Client;

        readonly DataServiceContext _context;
        readonly string _resourceUri;

        public EndPoint(DataServiceContext context, HttpClient client, string resourceUri)
        {
            _context = context;
            Client = client;
    
            _resourceUri = resourceUri;
        }

        public DataServiceQuery<TEntity> Entities => _context.CreateQuery<TEntity>(_resourceUri);

        public async Task<TEntity> Entity(Guid id, Func<DataServiceQuery<TEntity>, IQueryable<TEntity>> selector) => 
            (await selector(Entities).Where(u => u.Id == id).ToListAsync(Client)).Value.FirstOrDefault();
        
        // TODO: move odata uri from string builder.

        public async Task Save(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                await Client.SendJsonAsync<TEntity>(HttpMethod.Post, $"/odata/{_resourceUri}/Post", entity);
            }
            else
            {
                await Client.SendJsonAsync<TEntity>(HttpMethod.Put, $"/odata/{_resourceUri}/Put", entity);
            }
        }

        public async Task Delete(Guid id) => await Client.DeleteAsync($"/odata/{_resourceUri}/Delete/{id}");
    }
}