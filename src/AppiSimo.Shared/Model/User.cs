namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;

    public class User : Entity
    {
        public Guid Sub { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public Address Address { get; set; } = new Address();
        public string Username { get; set; }      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Fit Fit { get; set; } = new Fit();
        public bool Enabled { get; set; }

        public ICollection<UserEvent> UsersEvents { get; set; }

    }
}