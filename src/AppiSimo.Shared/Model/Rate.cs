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
        
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public ICollection<CourtRate> CourtsRates { get; set; }
    }
}