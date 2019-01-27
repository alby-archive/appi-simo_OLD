namespace AppiSimo.Shared.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.ExceptionServices;

    [Table("UserEvent")]
    public class UserEvent : Entity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Event Event { get; set; }
        public Guid EventId { get; set; }
        public decimal Cost { get; private set; }
        
        [NotMapped]
        public string FormattedCost
        {
            get => Cost.ToString("0.00");
            set
            {
                var isValid = decimal.TryParse(value, out var cost);
                Cost = isValid ? cost : throw new InvalidCastException("Cannot parse cost value.");
            }
        }
    }
}