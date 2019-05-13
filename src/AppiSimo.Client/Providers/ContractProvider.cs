namespace AppiSimo.Client.Providers
{
    using System;
    using AppiSimo.Shared.Model;
    using Microsoft.JSInterop;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Shared.Model;

    public class ContractProvider<TEntity, TResolver> : IContractProvider<TEntity, TResolver>
        where TEntity : class, new()
        where TResolver : DefaultContractResolver
    {
        readonly TResolver _resolver;

        public ContractProvider(TResolver resolver)
        {
            _resolver = resolver;
        }

        public TEntity Normalize(string response)
        {
            Console.WriteLine($"RESPONSE: {response}");
            var result = JsonConvert.DeserializeObject<TEntity>(response, new JsonSerializerSettings { ContractResolver = _resolver });

            Console.WriteLine($"RESULT: {Json.Serialize(result)}");

            return result;
        }
    }
}