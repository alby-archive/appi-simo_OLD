namespace AppiSimo.Shared.Validators
{
    using System.Collections.Generic;
    using Abstract;
    using Event;

    public static class ValidatorProxy
    {
        public static IValidator<Shared.Model.Event> EventsValidator
        {
            get
            {
                var validators = new List<IValidator<Shared.Model.Event>>
                {
                    new DateConsistencyValidator()
                };
                
                return new Validator<Shared.Model.Event>(validators);
            }
        }
    }
}