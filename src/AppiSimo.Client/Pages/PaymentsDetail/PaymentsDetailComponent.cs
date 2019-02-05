namespace AppiSimo.Client.Pages.PaymentsDetail
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;

    public class PaymentsDetailComponent : BaseDetailFilterComponent<UserEvent>
    {
        [Parameter]
        string Id { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected override IQueryable<UserEvent> Selector(DataServiceQuery<UserEvent> userEvents, Searcher searcher) => userEvents
            .Expand(userEvent => userEvent.User)
            .Expand(userEvent => userEvent.Event)
            .Where(userEvent => userEvent.User.Id == Guid.Parse(Id));

        protected async Task MarkAsPaid(UserEvent userEvent)
        {
            userEvent.Paid = !userEvent.Paid;
            await EndPoint.Save(userEvent);
        }

        protected decimal GetTotalNotPaid()
        {
            var eventsNotPaid = Entities.Where(userEvent => !userEvent.Paid);
            return eventsNotPaid.Sum(userEvent => userEvent.Cost);
        }

        protected void GoToEventDetail(Guid id)
        {
            UriHelper.NavigateTo($"/event/{id}");
        }
    }
}