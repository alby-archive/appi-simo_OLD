namespace AppiSimo.Shared.Model
{
    using System;

    public class Fit : Entity
    {
        public string CardNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}