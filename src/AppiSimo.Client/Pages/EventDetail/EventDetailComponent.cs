namespace AppiSimo.Client.Pages.EventDetail
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using AppiSimo.Shared.Validators;
    using AppiSimo.Shared.Validators.Abstract;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
    using Shared.Model;
    using Shared.Pages.Abstract;

    public class EventDetailComponent : BaseDetailComponent<Event>
    {
        [Inject]
        IUriHelper UriHelper { get; set; }

        [Inject]
        EndPoint<Court> CourtEndPoint { get; set; }

        [Inject]
        EndPoint<Light> LightEndPoint { get; set; }

        [Inject]
        EndPoint<Heat> HeatEndPoint { get; set; }

        [Inject]
        EndPoint<User> UserEndPoint { get; set; }
        
        [Inject]
        IValidator<Event> Validator { get; set; }

        protected override DataServiceQuery<Event> Selector(DataServiceQuery<Event> @event) => @event.Expand("UsersEvents($expand=User)");

        protected string Filter { get; set; } = string.Empty;

        protected EventDetailView ViewModel { get; private set; } = new EventDetailView();

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();

            var users = (await UserEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            var lights = (await LightEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            var heats = (await HeatEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            var courts = (await CourtEndPoint.Entities.IncludeTotalCount().Expand("CourtsRates($expand=Rate)").ToListAsync(CourtEndPoint.Client)).Value;
            
            ViewModel = new EventDetailView(Entity, users, courts, lights, heats, Validator);
            StateHasChanged();
        }

        protected void AddUser(UserEvent userEvent)
        {
            // TODO: implement equality comparer in UserEvent
            ViewModel.SelectedUserEvents.Add(userEvent);
            ViewModel.FilteredUserEvents = ViewModel.FilteredUserEvents.Where(fue => fue.UserId != userEvent.UserId).ToList();

            StateHasChanged();
        }

        protected void RemoveUser(UserEvent userEvent)
        {
            ViewModel.SelectedUserEvents = ViewModel.SelectedUserEvents.Where(sue => sue.UserId != userEvent.UserId).ToList();
            ViewModel.FilteredUserEvents.Add(userEvent);
            StateHasChanged();
        }

        protected override async Task Save()
        {
            Entity = ViewModel.Event;
            Entity.UsersEvents = ViewModel.SelectedUserEvents;
            Entity.Users = Entity.UsersEvents.Count;

            await base.Save();
            GoToEvents();
        }

        protected override async Task Delete()
        {
            await base.Delete();
            GoToEvents();
        }

        void GoToEvents()
        {
            UriHelper.NavigateTo("/events");
        }
    }
}