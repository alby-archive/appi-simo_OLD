namespace AppiSimo.Shared.Model
{
    using System;

    public class CourtRate : Entity
    {
        public Guid RateId { get; set; }
        public Rate Rate { get; set; }
        
        public Guid CourtId { get; set; }
        public Court Court { get; set; }
    }
}