namespace AppiSimo.Client.EndPoints
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.OData.Client;

    public interface IEndPoint<T>
    {
        DataServiceQuery<T> Entities { get; }
        Task Save(T entity);
        Task Delete(Guid id);
    }
}