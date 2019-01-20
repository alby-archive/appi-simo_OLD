namespace AppiSimo.Shared.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CourtRate")]
    public class CourtRate : Entity
    {
        public Guid RateId { get; set; }
        public Rate Rate { get; set; }
        
        public Guid CourtId { get; set; }
        public Court Court { get; set; }
    }
}