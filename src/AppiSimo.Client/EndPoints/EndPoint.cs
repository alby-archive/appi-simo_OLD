namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Abstract;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;
    using Shared.Services;

    public class EndPoint<TEntity> : IEndPoint<TEntity>
        where TEntity : class, IEntity, new()
    {
        public readonly HttpClient _client;
        readonly AuthService _auth;

        readonly DataServiceContext _context;
        protected readonly string _resourceUri;

        public EndPoint(DataServiceContext context, HttpClient client, AuthService auth, string resourceUri)
        {
            _context = context;
            _client = client;
            _auth = auth;

            _resourceUri = resourceUri;
        }

        public DataServiceQuery<TEntity> Entities => _context.CreateQuery<TEntity>(_resourceUri);

        public async Task<TEntity> Entity(Guid id, Func<DataServiceQuery<TEntity>, IQueryable<TEntity>> selector)
        {
            SetHeaders(_client, _auth);
            return (await selector(Entities).Where(u => u.Id == id).ToListAsync(_client)).Value.FirstOrDefault();
        }

        public async Task Save(TEntity entity)
        {
            SetHeaders(_client, _auth);
            if (entity.Id == Guid.Empty)
            {
                await _client.SendJsonAsync<TEntity>(HttpMethod.Post, $"{_resourceUri}/Post", entity);
            }
            else
            {
                await _client.SendJsonAsync<TEntity>(HttpMethod.Put, $"{_resourceUri}/Put", entity);
            }
        }

        public async Task Delete(Guid id)
        {
            SetHeaders(_client, _auth);
            await _client.DeleteAsync($"{_resourceUri}/Delete/{id}");
        }

        void SetHeaders(HttpClient client, AuthService auth)
        {
//            TODO: Add token in headers 

            Console.WriteLine("QUI");
            Console.WriteLine($"TOKEN_1: {auth.CurrentUser.id_token}");
            
            var user = auth.CurrentUser;
            if (user != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.access_token}");
            }
        }
    }
}