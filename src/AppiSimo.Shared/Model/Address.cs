namespace AppiSimo.Shared.Model
{
    using System;

    public class Address : Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public override string ToString() => $"{Street},{Zip}, {City}";
    }
}