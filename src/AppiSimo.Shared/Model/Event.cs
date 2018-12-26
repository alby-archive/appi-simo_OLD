namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using Abstract;

    public class Event : IEntity
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}