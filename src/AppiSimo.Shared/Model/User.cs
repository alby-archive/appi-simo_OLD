namespace AppiSimo.Shared.Model
{
    using System.Collections.Generic;

    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<UserEvent> UsersEvents { get; set; }
    }
}