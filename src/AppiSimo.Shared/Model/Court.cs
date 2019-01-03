namespace AppiSimo.Shared.Model
{
    using System;
    using Abstract;

    public class Court : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}