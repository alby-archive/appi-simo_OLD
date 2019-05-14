namespace AppiSimo.Client.Providers
{
    using Newtonsoft.Json.Serialization;

    public interface IContractProvider<out TEntity, TResolver>
        where TEntity : class, new()
        where TResolver : IContractResolver
    {
        TEntity Normalize(string response);
    }
}