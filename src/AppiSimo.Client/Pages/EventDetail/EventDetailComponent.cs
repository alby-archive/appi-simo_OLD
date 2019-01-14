namespace AppiSimo.Client.Pages.EventDetail
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
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

        ICollection<User> Users { get; set; } = new List<User>();

        protected IEnumerable<Court> Courts { get; private set; } = new List<Court>();
        protected IEnumerable<Light> Lights { get; private set; } = new List<Light>();
        protected IEnumerable<Heat> Heats { get; private set; } = new List<Heat>();
        protected ICollection<User> SelectedUsers { get; } = new List<User>();
        protected ICollection<User> FilteredUsers { get; private set; } = new List<User>();

        protected string Filter { get; set; } = string.Empty;

        protected override DataServiceQuery<Event> Selector(DataServiceQuery<Event> @event) => @event
            .Expand(e => e.Light)
            .Expand(e => e.Heat);

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();

            Courts = (await CourtEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Lights = (await LightEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Heats = (await HeatEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Users = (await UserEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;

            FilteredUsers = Users;
        }

        protected string StartDate { get => Entity.StartDate == DateTime.MinValue ? string.Empty : Entity.StartDate.ToString("g"); set => Entity.StartDate = DateTime.Parse(value); }

        protected string EndDate { get => Entity.EndDate == DateTime.MinValue ? string.Empty : Entity.EndDate.ToString("g"); set => Entity.EndDate = DateTime.Parse(value); }

        protected string SelectedCourt { get => Entity.CourtId.ToString(); set => Entity.CourtId = Guid.Parse(value); }

        protected string SelectedLight { get => Entity.Light?.Id.ToString(); set => Entity.Light.Id = Guid.Parse(value); }

        protected string SelectedHeat { get => Entity.Heat?.Id.ToString(); set => Entity.Heat.Id = Guid.Parse(value); }

        protected void AddUser(User user)
        {
            SelectedUsers.Add(user);
            FilteredUsers.Remove(user);
            StateHasChanged();
        }

        protected void RemoveUser(User user)
        {
            SelectedUsers.Remove(user);
            FilteredUsers.Add(user);
            StateHasChanged();
        }

        protected override async Task Save()
        {
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