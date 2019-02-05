namespace AppiSimo.Shared.Model
{
    using System;

    public class MedicalCertificate : Entity
    {
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}