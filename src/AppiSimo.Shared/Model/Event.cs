namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;

    public class Event : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CourtId { get; set; }
        public Court Court { get; set; }
        public Light Light { get; set; } = new Light();
        public Heat Heat { get; set; } = new Heat();
        public int Users { get; set; }
        public ICollection<UserEvent> UsersEvents { get; set; }
    }
}