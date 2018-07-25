namespace AppiSimo.Shared.Model
{
    using System;
    using Abstract;

    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}