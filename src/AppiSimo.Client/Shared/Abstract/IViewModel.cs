namespace AppiSimo.Client.Shared.Abstract
{
    public interface IViewModel<out T>
    {
        T Event { get; }
    }
}