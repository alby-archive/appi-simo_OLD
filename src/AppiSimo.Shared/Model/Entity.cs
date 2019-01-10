namespace AppiSimo.Shared.Model
{
    using System;
    using Abstract;
    using Microsoft.OData.Client;

    [EntityType]
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}