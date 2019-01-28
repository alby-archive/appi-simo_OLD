namespace AppiSimo.Shared.Validators.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Result
    {
        public Result( IReadOnlyCollection<string> errors, IReadOnlyDictionary<string, Result> properties)
        {
            Properties = properties;
            Errors = errors;
        }

        public IReadOnlyDictionary<string, Result> Properties { get; }
        public IReadOnlyCollection<string> Errors { get; }
        public bool IsValid => Errors.Count == 0 && Properties.All(prop => prop.Value.IsValid);

        public static readonly Result Valid = new Result(Array.Empty<string>(), new Dictionary<string, Result>());
        public static Result Merge(Result left, Result right)
        {
            var groupBy = left.Properties.Concat(right.Properties)
                .GroupBy(pair => pair.Key, (key, pairs) => (key, pairs.Select(pair => pair.Value).Aggregate(Valid, Merge)));
            
            return new Result
            (
                left.Errors.Concat(right.Errors).ToList(),
                groupBy.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2)
            );
        }
    }
}