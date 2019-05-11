namespace AppiSimo.Client.Pages.UserDetail
{
    using System;
    using AppiSimo.Shared.Model;

    public class UserDetailViewModel
    {
        public User User { get; }

        public UserDetailViewModel()
        {
        }

        public UserDetailViewModel(User user)
        {
            User = user;
        }

        public string GivenName { get => User.Profile.GivenName; set => User.Profile.GivenName = value; }

        public string FamilyName { get => User.Profile.FamilyName; set => User.Profile.FamilyName = value; }

        public int Gender { get => (int) User.Profile.Gender; set => User.Profile.Gender = (Genders) value; }

        public bool BirthDateIsValid { get; set; }

        public string Birthdate
        {
            get => User.Profile.Birthdate == DateTime.MinValue ? string.Empty : $"{User.Profile.Birthdate:d}";
            set
            {
                BirthDateIsValid = DateTime.TryParse(value, out var birthDate);

                if (BirthDateIsValid)
                {
                    User.Profile.Birthdate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
                }
            }
        }

        public string Address { get => User.Profile.Address; set => User.Profile.Address = value; }

        public string Email { get => User.Profile.Email; set => User.Profile.Email = value; }

        public string PhoneNumber { get => User.Profile.PhoneNumber; set => User.Profile.PhoneNumber = value; }

        public string FitCard { get => User.Fit.CardNumber; set => User.Fit.CardNumber = value; }
    }
}