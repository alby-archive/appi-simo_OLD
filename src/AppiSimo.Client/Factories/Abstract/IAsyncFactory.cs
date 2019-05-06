namespace AppiSimo.Client.Factories.Abstract
{
    using System.Threading.Tasks;

    public interface IAsyncFactory<T>
    {
        Task<T> CreateAsync();
    }
}