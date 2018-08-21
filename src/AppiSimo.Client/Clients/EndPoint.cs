namespace AppiSimo.Client.Clients
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.OData.Client;

    public class EndPoint<T> : IEndPoint<T>
    {
        public EndPoint(Uri baseUri, string resourceUri)
        {
            var queryable = new DataServiceContext(baseUri).CreateQuery<T>(resourceUri);

            ElementType = queryable.ElementType;
            Expression = queryable.Expression;
            Provider = queryable.Provider;
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator() => throw new NotSupportedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}