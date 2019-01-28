namespace AppiSimo.Shared.Validators.Abstract
{
    using Model;

    public interface IValidator<in T>
    {
        Result Validate(T entity);
    }
}