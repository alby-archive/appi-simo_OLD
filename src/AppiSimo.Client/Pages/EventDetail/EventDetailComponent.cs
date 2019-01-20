namespace AppiSimo.Client.Pages.EventDetail
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        protected ICollection<User> SelectedUsers { get; private set; } = new List<User>();
        protected ICollection<User> FilteredUsers { get; private set; } = new List<User>();

        protected string Filter { get; set; } = string.Empty;

        protected override DataServiceQuery<Event> Selector(DataServiceQuery<Event> @event) => @event
            .Expand("UsersEvents($expand=User)");

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();

            Courts = (await CourtEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Lights = (await LightEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Heats = (await HeatEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;
            Users = (await UserEndPoint.Entities.IncludeTotalCount().ToListAsync(CourtEndPoint.Client)).Value;

            if (Entity.UsersEvents == null)
            {
                Entity.UsersEvents = new List<UserEvent>();
            }
            
            SelectedUsers = Entity.UsersEvents.Select(eu => eu.User).ToList();
            FilteredUsers = Users.Except(SelectedUsers).ToList();
            Console.WriteLine("COURT: " + Entity.CourtId);
            
            Entity.CourtId = Entity.CourtId == Guid.Empty ? Courts.OrderBy(court => court.Name).First().Id : Entity.CourtId;
        }

        protected string StartDate { get => Entity.StartDate == DateTime.MinValue ? string.Empty : Entity.StartDate.ToLocalTime().ToString("g"); set => Entity.StartDate = DateTime.Parse(value).ToUniversalTime(); }

        protected string EndDate { get => Entity.EndDate == DateTime.MinValue ? string.Empty : Entity.EndDate.ToLocalTime().ToString("g"); set => Entity.EndDate = DateTime.Parse(value).ToUniversalTime(); }

        protected string SelectedCourt
        {
            get => Entity.CourtId.ToString();
            set => Entity.CourtId = Guid.Parse(value);
        }

        // TODO: https://github.com/aspnet/Blazor/issues/576
        protected string SelectedLight
        {
            get => Entity.LightId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Entity.LightId = null;
                }
                else
                {
                    Entity.LightId = Guid.Parse(value);
                }
            }
        }

        protected string SelectedHeat
        {
            get => Entity.HeatId?.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Entity.HeatId = null;
                }
                else
                {
                    Entity.HeatId = Guid.Parse(value);
                }
            }
        }

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
            Entity.UsersEvents = SelectedUsers.Select(su => new UserEvent
            {
                UserId = su.Id,
                EventId = Entity.Id
            }).ToList();

            Entity.Users = SelectedUsers.Count;

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