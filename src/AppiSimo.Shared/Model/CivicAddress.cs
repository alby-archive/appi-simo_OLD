namespace AppiSimo.Shared.Model
{
    using System;

    public class CivicAddress : Entity
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}