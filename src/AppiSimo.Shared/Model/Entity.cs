namespace AppiSimo.Shared.Model
{
    using System;
    using Abstract;
    using Microsoft.OData.Client;

    [EntityType]
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        protected bool Equals(Entity other) => Id.Equals(other.Id);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: null, objB: obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && Equals((Entity) obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity left, Entity right) => Equals(left, right);

        public static bool operator !=(Entity left, Entity right) => !Equals(left, right);
    }
}