namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CourtId { get; set; }
        public Court Court { get; set; }
        public Guid? LightId { get; set; }
        public Light Light { get; set; }
        public double LightDuration { get; set; }
        public Guid? HeatId { get; set; }
        public Heat Heat { get; set; }
        public double HeatDuration { get; set; }
        public int Users { get; set; }
        public ICollection<UserEvent> UsersEvents { get; set; } = new List<UserEvent>();
        [NotMapped]
        public decimal Cost { get; set; }
        
        public bool Paid { get; set; }
    }
}