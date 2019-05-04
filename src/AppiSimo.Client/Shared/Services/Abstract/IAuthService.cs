namespace AppiSimo.Client.Shared.Services.Abstract
{
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Model;

    public interface IAuthService
    {
        BehaviorSubject<RootObject> User { get; }
        RootObject CurrentUser { get; }

        Task TryLoadUser();

        Task SignIn();

        Task SignedIn();

        void SignOut();
    }
}