namespace AppiSimo.Shared.Validators.Event
{
    using System.Collections.Generic;
    using Abstract;
    using Model;
    using Shared.Model;

    public class DateConsistencyValidator : IValidator<Event>
    {
        public Result Validate(Event entity)
        {
            if (entity.StartDate >= entity.EndDate)
            {
                return new Result(new string[0], new Dictionary<string, Result>
                {
                    ["StartDate"] = new Result(new[] { "La data di inizio evento deve essere minore alla data di fine evento." }, new Dictionary<string, Result>()),
                    ["EndDate"] = new Result(new[] { "La data di fine evento deve essere maggiore alla data di inizio evento." }, new Dictionary<string, Result>())
                });
            }

            return Result.Valid;
        }
    }
}