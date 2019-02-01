namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;

    public class Heat : Entity
    {
        public string HeatType { get; set; }
        public decimal Price { get; set; }
        ICollection<Court> Courts { get; set; }
    }
}