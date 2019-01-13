namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;

    public class Court : Entity
    {
        public string Name { get; set; }
        public ICollection<CourtRate> CourtsRates { get; set; }
    }
}