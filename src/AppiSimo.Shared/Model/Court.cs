namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;

    public class Court : Entity
    {
        public string Name { get; set; }
        public Light Light { get; set; } = new Light();
        public Heat Heat { get; set; } = new Heat();      
        public ICollection<CourtRate> CourtsRates { get; set; }
    }
}