namespace AppiSimo.Client.Pages.PaymentsDetail
{
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;

    public class PaymentsDetailComponent : BaseDetailComponent<User>
    {
        protected override DataServiceQuery<User> Selector(DataServiceQuery<User> user) => user.Expand(u => u.UsersEvents);

    }
}