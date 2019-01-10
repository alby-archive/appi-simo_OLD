namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using Abstract;

    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<UserEvent> UsersEvents { get; set; }
    }
}