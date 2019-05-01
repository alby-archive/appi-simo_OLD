namespace AppiSimo.Client.Pages.CurrentUserBadge
{
    using Shared.Services;

    public class CurrentUserBadgeViewModel
    {
        readonly AuthService _service;
        
        public CurrentUserBadgeViewModel(AuthService service)
        {
            _service = service;
        }

        public string CurrentUser = "User";
        public bool IsLogged() => _service?.IsLogged ?? false;
    }
}