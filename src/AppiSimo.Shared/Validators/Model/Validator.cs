namespace AppiSimo.Shared.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Model;

    public class Validator<T> : IValidator<T>
    {
        readonly IEnumerable<IValidator<T>> _validators;

        public Validator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public Result Validate(T entity) => 
            _validators.Aggregate(Result.Valid, (current, validator) => Result.Merge(current, validator.Validate(entity)));
    }
}