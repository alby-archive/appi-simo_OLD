namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;
    
    public class Light : Entity
    {
        public string LightType { get; set; }
        public decimal Price { get; set; }
        public ICollection<Court> Courts { get; set; }
    }
}