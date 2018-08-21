namespace AppiSimo.Client.Clients
{
    using System.Linq;

    public interface IEndPoint<T> : IQueryable<T>
    {
    }
}