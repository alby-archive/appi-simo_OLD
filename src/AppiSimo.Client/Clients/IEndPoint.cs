namespace AppiSimo.Client.Clients
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IEndPoint<T> : IQueryable<T>
    {
        Task Save(T entity);
        Task Remove(Guid id);
    }
}