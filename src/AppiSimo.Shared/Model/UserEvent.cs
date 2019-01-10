namespace AppiSimo.Shared.Model
{
    using System;
    using Abstract;  
    
    public class UserEvent : Entity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Event Event { get; set; }
        public Guid EventId { get; set; }
    }
}