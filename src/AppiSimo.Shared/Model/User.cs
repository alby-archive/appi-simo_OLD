namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;

    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public CivicAddress CivicAddress { get; set; } = new CivicAddress();
        public string PhoneNumber { get; set; }
        public Fit Fit { get; set; } = new Fit();
        public ICollection<UserEvent> UsersEvents { get; set; }
        public ICollection<MedicalCertificate> MedicalCertificates { get; set; }
    }
}