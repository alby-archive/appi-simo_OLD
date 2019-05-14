namespace AppiSimo.Client.Providers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class ContractProvider<TEntity, TResolver> : IContractProvider<TEntity, TResolver>
        where TEntity : class, new()
        where TResolver : IContractResolver
    {
        readonly TResolver _resolver;

        public ContractProvider(TResolver resolver)
        {
            _resolver = resolver;
        }

        public TEntity Normalize(string response) => JsonConvert.DeserializeObject<TEntity>(response, new JsonSerializerSettings { ContractResolver = _resolver });
    }
}