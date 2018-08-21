namespace AppiSimo.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Reflection;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.OData.Client;

        

    public static class Queryables
    {
        static readonly Func<DataServiceQueryProvider, Expression, Uri> toUri;

        static Queryables()
        {
            try
            {
                var provider = Expression.Parameter(typeof(DataServiceQueryProvider));
                var expression = Expression.Parameter(typeof(Expression));

                var translate = typeof(DataServiceQueryProvider).GetMethod("Translate", BindingFlags.Instance | BindingFlags.NonPublic);
                var queryComponents = Type.GetType("Microsoft.OData.Client.QueryComponents, Microsoft.OData.Client");

                var uri = queryComponents?.GetProperty("Uri", BindingFlags.Instance | BindingFlags.NonPublic);

                if (translate == null || uri == null)
                {
                    toUri = (_, __) => throw new InvalidOperationException();
                    return;
                }

                var callTranslate = Expression.Call(provider, translate, expression);
                var body = Expression.Property(callTranslate, uri);

                toUri = Expression.Lambda<Func<DataServiceQueryProvider, Expression, Uri>>(body, provider, expression).Compile();
            }
            catch (Exception ex)
            {
                var exception = ExceptionDispatchInfo.Capture(ex);
                toUri = (_, __) =>
                {
                    exception.Throw();
                    return null;
                };
            }
        }

        class ODataResult<T>
        {
            public T Value { get; set; }
        }
        
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source, HttpClient http)
        {
            var provider = (DataServiceQueryProvider) source.Provider;
            var expression = source.Expression;

            var uri = toUri(provider, expression);

            var result = await http.GetJsonAsync<ODataResult<List<T>>>(uri.AbsoluteUri);
            return result.Value;
        }
    }
}