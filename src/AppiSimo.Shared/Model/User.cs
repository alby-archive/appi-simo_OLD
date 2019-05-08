namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;

    public class User : Entity
    {
        public Profile Profile { get; set; } = new Profile();
        public Fit Fit { get; set; } = new Fit();
        public bool Enabled { get; set; }

        public ICollection<UserEvent> UsersEvents { get; set; }

    }
}