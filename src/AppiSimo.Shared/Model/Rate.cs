namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;

    public class Rate : Entity
    {
        public DateTime StartDate { get; set; }

        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }

        public decimal Price { get; set; }

        public ICollection<CourtRate> CourtsRates { get; set; }
    }
}