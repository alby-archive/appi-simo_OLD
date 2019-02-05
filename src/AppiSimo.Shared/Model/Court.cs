namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;

    public class Court : Entity
    {
        public string Name { get; set; }
        public Guid LightId { get; set; }
        public Light Light { get; set; } = new Light();
        public Guid HeatId { get; set; }
        public Heat Heat { get; set; } = new Heat();      
        public ICollection<CourtRate> CourtsRates { get; set; }
    }
}